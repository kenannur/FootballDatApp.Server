using System.Net.Http;
using System.Threading.Tasks;

namespace Vegas.FootballDatApp.Console.Test
{
    class Program
    {
        private static readonly string apiKey = "54e6927f9a294659be40ac1693b76bb3";
        private static readonly string apiUri = "http://api.football-data.org/v2";
        private static readonly HttpClient client = new HttpClient();
        private static readonly int englandCompetitionId = 2021;
        //private static readonly int clCompetitionId = 2001;

        static async Task Main(string[] args)
        {
            client.DefaultRequestHeaders.Add("X-Auth-Token", apiKey);

            var areas = $"{apiUri}/areas";

            // random area Id = 2072
            var area = $"{apiUri}/areas/2267";

            var competitions = $"{apiUri}/competitions?plan=TIER_ONE";

            var competition = $"{apiUri}/competitions/{englandCompetitionId}";

            // season={STRING} YYYY
            // stage={STRING} A-Z
            var competitionTeams = $"{apiUri}/competitions/{englandCompetitionId}/teams?season=1994";

            // standingType={STRING} [TOTAL (default) | HOME | AWAY]
            var competitionStandings = $"{apiUri}/competitions/{englandCompetitionId}/standings";

            // dateFrom={DATE} YYYY-MM-dd
            // dateTo={DATE} YYYY-MM-dd
            // stage={STRING} A-Z
            // status={ENUM} [SCHEDULED | LIVE | IN_PLAY | PAUSED | FINISHED | POSTPONED | SUSPENDED | CANCELED]
            // matchday={INTEGER} 1-49
            // group={STRING}
            // season={STRING} YYYY
            var competitionMatches = $"{apiUri}/competitions/{englandCompetitionId}/matches?season=2019";

            var competitionScorers = $"{apiUri}/competitions/{englandCompetitionId}/scorers";

            // competitions={competitionIds} Comma seperated Id List
            // dateFrom={DATE} YYYY-MM-dd
            // dateTo={DATE} YYYY-MM-dd
            // status={ENUM} [SCHEDULED | LIVE | IN_PLAY | PAUSED | FINISHED | POSTPONED | SUSPENDED | CANCELED]
            var matches = $"{apiUri}/matches?dateFrom=2021-02-24&dateTo=2021-02-24";

            // random matchId = 308326;
            var match = $"{apiUri}/matches/308326";

            // random teamId = 57
            var team = $"{apiUri}/teams/59";

            // dateFrom={DATE} YYYY-MM-dd
            // dateTo={DATE} YYYY-MM-dd
            // status={ENUM} [SCHEDULED | LIVE | IN_PLAY | PAUSED | FINISHED | POSTPONED | SUSPENDED | CANCELED]
            // venue={ENUM} [HOME | AWAY]
            // limit={INTEGER}
            var teamMatches = $"{apiUri}/teams/57/matches";

            // random playerId = 7801
            var player = $"{apiUri}/players/7801";

            // dateFrom={DATE} YYYY-MM-dd
            // dateTo={DATE} YYYY-MM-dd
            // status={ENUM} [SCHEDULED | LIVE | IN_PLAY | PAUSED | FINISHED | POSTPONED | SUSPENDED | CANCELED]
            // competitions={competitionIds} Comma seperated Id List
            // limit={INTEGER}
            var playerMatches = $"{apiUri}/players/44/matches";


            var result = await client.GetStringAsync(team);

            System.Console.ReadLine();
        }
    }
}
