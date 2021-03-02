using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Vegas.AspNetCore.Common.Exceptions;
using Vegas.FootballDatApp.Entities;
using Vegas.FootballDatApp.Models.Response;
using Vegas.FootballDatApp.Settings;

namespace Vegas.FootballDatApp.CQRS.Query.External
{
    public class FootballDataHttpClient : IFootballDataHttpClient
    {
        private readonly HttpClient _httpClient;
        private readonly IFootballDataApiSettings _footballDataApiSettings;

        private readonly string dateFormat = "yyyy-MM-dd";

        public FootballDataHttpClient(HttpClient httpClient, IFootballDataApiSettings footballDataApiSettings)
        {
            _httpClient = httpClient;
            _footballDataApiSettings = footballDataApiSettings;

            _httpClient.DefaultRequestHeaders.Add("X-Auth-Token", _footballDataApiSettings.Token);
            _httpClient.BaseAddress = new Uri(_footballDataApiSettings.Endpoint);
        }

        private async Task<TResponse> FetchAsync<TResponse>(string requestUri, CancellationToken cancellationToken = default)
        {
            try
            {
                var fullRequestUri = $"/{_footballDataApiSettings.Version}/{requestUri}";

                var jResponse = await _httpClient.GetStringAsync(fullRequestUri, cancellationToken);
                var response = JsonSerializer.Deserialize<TResponse>(jResponse, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
                return response;
            }
            catch (TaskCanceledException)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<Area>> FetchAreasAsync()
        {
            var requestUri = "areas";
            var result = await FetchAsync<FetchAreasResponse>(requestUri);
            return result.Areas;
        }

        public async Task<Area> FetchAreaAsync(int areaId)
        {
            var requestUri = $"areas/{areaId}";
            var result = await FetchAsync<Area>(requestUri);
            return result;
        }

        public async Task<List<Competition>> FetchCompetitionsAsync(string plan = "TIER_ONE")
        {
            var requestUri = $"competitions?plan={plan}";
            var result = await FetchAsync<FetchCompetitionsResponse>(requestUri);
            return result.Competitions;
        }

        public async Task<Competition> FetchCompetitionAsync(int competitionId)
        {
            var requestUri = $"competitions/{competitionId}";
            var result = await FetchAsync<Competition>(requestUri);
            return result;
        }

        /// <summary>
        /// CompetitionId'ye ait takımları getirir.
        /// </summary>
        /// <param name="competitionId"></param>
        /// <param name="season"> YYYY </param>
        /// <param name="stage"></param>
        /// <returns></returns>
        public async Task<List<Team>> FetchCompetitionTeamsAsync(int competitionId, string season, string stage = default)
        {
            var requestUri = $"competitions/{competitionId}/teams";
            if (string.IsNullOrWhiteSpace(season))
            {
                throw new BusinessException(HttpStatusCode.BadRequest, "Season cannot be null");
            }
            requestUri += $"?season={season}";

            if (!string.IsNullOrWhiteSpace(stage))
            {
                requestUri += $"&stage={stage}";
            }

            var result = await FetchAsync<FetchCompetitionTeamsResponse>(requestUri);
            return result.Teams;
        }

        /// <summary>
        /// CompetitionId'ye ait lig sıralamasını getirir.
        /// </summary>
        /// <param name="competitionId"></param>
        /// <param name="standingType"> TOTAL(default) | HOME | AWAY </param>
        /// <returns></returns>
        public async Task<List<Standing>> FetchCompetitionStandingsAsync(int competitionId, string standingType = "TOTAL")
        {
            var requestUri = $"competitions/{competitionId}/standings?standingType={standingType}";
            var result = await FetchAsync<FetchCompetitionStandingsResponse>(requestUri);
            return result.Standings;
        }

        /// <summary>
        /// Competition'a ait maçları getirir.
        /// </summary>
        /// <param name="competitionId"></param>
        /// <param name="season"> YYYY </param>
        /// <param name="matchday"> 1-49 </param>
        /// <param name="status"> SCHEDULED | LIVE | IN_PLAY | PAUSED | FINISHED | POSTPONED | SUSPENDED | CANCELED </param>
        /// <param name="stage"></param>
        /// <param name="group"></param>
        /// <param name="dateFrom"> YYYY-MM-DD </param>
        /// <param name="dateTo"> YYYY-MM-DD </param>
        /// <returns></returns>
        public async Task<List<Match>> FetchCompetitionMatchesAsync(int competitionId, string season, int? matchday, MatchStatus? status,
            string stage, string group, DateTime? dateFrom, DateTime? dateTo)
        {
            var requestUri = $"competitions/{competitionId}/matches";
            if (string.IsNullOrWhiteSpace(season))
            {
                throw new BusinessException(HttpStatusCode.BadRequest, "Season cannot be null");
            }
            requestUri += $"?season={season}";

            if (matchday.HasValue)
            {
                requestUri += $"&matchday={matchday.Value}";
            }
            if (status.HasValue)
            {
                requestUri += $"&status={status.Value}";
            }
            if (!string.IsNullOrWhiteSpace(stage))
            {
                requestUri += $"&stage={stage}";
            }
            if (!string.IsNullOrWhiteSpace(group))
            {
                requestUri += $"&group={group}";
            }
            if (dateFrom.HasValue)
            {
                requestUri += $"&dateFrom={dateFrom.Value.ToString(dateFormat)}";
            }
            if (dateTo.HasValue)
            {
                requestUri += $"&dateTo={dateTo.Value.ToString(dateFormat)}";
            }

            var result = await FetchAsync<FetchCompetitionMatchesResponse>(requestUri);
            return result.Matches;
        }

        public async Task<List<Scorer>> FetchCompetitionScorersAsync(int competitionId)
        {
            var requestUri = $"competitions/{competitionId}/scorers";
            var result = await FetchAsync<FetchCompetitionScorersResponse>(requestUri);
            return result.Scorers;
        }

        /// <summary>
        /// Parametrelere göre match'ları getirir. Parametre verilmezse bugüne air maçlar getirilir.
        /// </summary>
        /// <param name="dateFrom"></param>
        /// <param name="dateTo"></param>
        /// <param name="status"> SCHEDULED | LIVE | IN_PLAY | PAUSED | FINISHED | POSTPONED | SUSPENDED | CANCELED </param>
        /// <returns></returns>
        public async Task<List<Match>> FetchMatchesAsync(DateTime? dateFrom, DateTime? dateTo, MatchStatus? status)
        {
            var requestUri = "matches";
            if (!dateFrom.HasValue)
            {
                dateFrom = DateTime.Today;
            }
            if (!dateTo.HasValue)
            {
                dateTo = DateTime.Today;
            }
            requestUri += $"?dateFrom={dateFrom.Value.ToString(dateFormat)}&dateTo={dateTo.Value.ToString(dateFormat)}";

            if (status.HasValue)
            {
                requestUri += $"&status={status.Value}";
            }

            var result = await FetchAsync<FetchMatchesResponse>(requestUri);
            return result.Matches;
        }

        public async Task<Match> FetchMatchAsync(int matchId)
        {
            var requestUri = $"matches/{matchId}";
            var result = await FetchAsync<FetchMatchResponse>(requestUri);
            return result.MatchDetail.Match;
        }

        public async Task<Team> FetchTeamAsync(int teamId)
        {
            var requestUri = $"teams/{teamId}";
            var result = await FetchAsync<Team>(requestUri);
            return result;
        }

        /// <summary>
        /// Bir takıma ait şu anki sezonun maçlarını getirir.
        /// </summary>
        /// <param name="teamId"></param>
        /// <param name="dateFrom"></param>
        /// <param name="dateTo"></param>
        /// <param name="status"> SCHEDULED | LIVE | IN_PLAY | PAUSED | FINISHED | POSTPONED | SUSPENDED | CANCELED </param>
        /// <param name="venue"> HOME | AWAY </param>
        /// <returns></returns>
        public async Task<List<Match>> FetchTeamMatchesAsync(int teamId, DateTime? dateFrom, DateTime? dateTo, MatchStatus? status, string venue)
        {
            var requestUri = $"teams/{teamId}/matches";
            if (dateFrom.HasValue || dateTo.HasValue || status.HasValue || !string.IsNullOrWhiteSpace(venue))
            {
                requestUri += "?";
            }

            if (dateFrom.HasValue)
            {
                requestUri += $"&dateFrom={dateFrom.Value.ToString(dateFormat)}";
            }
            if (dateTo.HasValue)
            {
                requestUri += $"&dateTo={dateTo.Value.ToString(dateFormat)}";
            }
            if (status.HasValue)
            {
                requestUri += $"&status={status.Value}";
            }
            if (!string.IsNullOrWhiteSpace(venue))
            {
                requestUri += $"&venue={venue}";
            }

            var index = requestUri.IndexOf('&');
            if (index != -1)
            {
                requestUri = requestUri.Remove(index, 1);
            }

            var result = await FetchAsync<FetchTeamMatchesResponse>(requestUri);
            return result.Matches;
        }

        public async Task<Player> FetchPlayerAsync(int playerId)
        {
            var requestUri = $"players/{playerId}";
            var result = await FetchAsync<Player>(requestUri);
            return result;
        }

        /// <summary>
        /// Oyuncuya ait maçları getirir.
        /// </summary>
        /// <param name="playerId"></param>
        /// <param name="dateFrom"></param>
        /// <param name="dateTo"></param>
        /// <param name="status"></param>
        /// <param name="competitionIds"></param>
        /// <returns></returns>
        public async Task<List<Match>> FetchPlayerMatchesAsync(int playerId, DateTime? dateFrom, DateTime? dateTo,
            MatchStatus? status, string competitionIds)
        {
            var requestUri = $"players/{playerId}/matches";
            if (dateFrom.HasValue || dateTo.HasValue || status.HasValue || !string.IsNullOrWhiteSpace(competitionIds))
            {
                requestUri += "?";
            }

            if (dateFrom.HasValue)
            {
                requestUri += $"&dateFrom={dateFrom.Value.ToString(dateFormat)}";
            }
            if (dateTo.HasValue)
            {
                requestUri += $"&dateTo={dateTo.Value.ToString(dateFormat)}";
            }
            if (status.HasValue)
            {
                requestUri += $"&status={status.Value}";
            }
            if (!string.IsNullOrWhiteSpace(competitionIds))
            {
                requestUri += $"&competitionIds={competitionIds}";
            }

            var index = requestUri.IndexOf('&');
            if (index != -1)
            {
                requestUri = requestUri.Remove(index, 1);
            }

            var result = await FetchAsync<FetchPlayerMatchesResponse>(requestUri);
            return result.Matches;
        }
    }
}
