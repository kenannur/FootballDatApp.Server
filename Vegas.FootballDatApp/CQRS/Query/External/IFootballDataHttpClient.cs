using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Vegas.FootballDatApp.Entities;

namespace Vegas.FootballDatApp.CQRS.Query.External
{
    public interface IFootballDataHttpClient
    {
        Task<List<Area>> FetchAreasAsync();

        Task<Area> FetchAreaAsync(int areaId);


        Task<List<Competition>> FetchCompetitionsAsync(string plan = "TIER_ONE");

        Task<Competition> FetchCompetitionAsync(int competitionId);

        Task<List<Team>> FetchCompetitionTeamsAsync(int competitionId, string season, string stage = default);

        Task<List<Standing>> FetchCompetitionStandingsAsync(int competitionId, string standingType = "TOTAL");

        Task<List<Match>> FetchCompetitionMatchesAsync(int competitionId, string season, int? matchday, MatchStatus? status, string stage, string group, DateTime? dateFrom, DateTime? dateTo);

        Task<List<Scorer>> FetchCompetitionScorersAsync(int competitionId);


        Task<List<Match>> FetchMatchesAsync(DateTime? dateFrom, DateTime? dateTo, MatchStatus? status);

        Task<Match> FetchMatchAsync(int matchId);


        Task<Team> FetchTeamAsync(int teamId);

        Task<List<Match>> FetchTeamMatchesAsync(int teamId, DateTime? dateFrom, DateTime? dateTo, MatchStatus? status, string venue);


        Task<Player> FetchPlayerAsync(int playerId);

        Task<List<Match>> FetchPlayerMatchesAsync(int playerId, DateTime? dateFrom, DateTime? dateTo, MatchStatus? status, string competitionIds);
    }
}
