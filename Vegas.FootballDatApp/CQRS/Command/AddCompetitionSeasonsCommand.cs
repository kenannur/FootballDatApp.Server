using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Vegas.FootballDatApp.Contexts;
using Vegas.FootballDatApp.CQRS.Query.External;

namespace Vegas.FootballDatApp.CQRS.Command
{
    public class AddCompetitionSeasonsCommandRequest : IRequest
    {
        public int CompetitionId { get; private set; }

        public AddCompetitionSeasonsCommandRequest(int competitionId)
        {
            CompetitionId = competitionId;
        }
    }


    public class AddCompetitionSeasonsCommandHandler : IRequestHandler<AddCompetitionSeasonsCommandRequest, Unit>
    {
        private readonly FootballDbContext _dbContext;
        private readonly IFootballDataHttpClient _footballDataHttpClient;

        public AddCompetitionSeasonsCommandHandler(FootballDbContext dbContext, IFootballDataHttpClient footballDataHttpClient)
        {
            _dbContext = dbContext;
            _footballDataHttpClient = footballDataHttpClient;
        }

        public async Task<Unit> Handle(AddCompetitionSeasonsCommandRequest request, CancellationToken cancellationToken)
        {
            var competition = await _footballDataHttpClient.FetchCompetitionAsync(request.CompetitionId);
            foreach (var season in competition.Seasons)
            {
                var winnerTeamId = season.Winner?.Id;
                if (winnerTeamId != null)
                {
                    var winnerTeamIsExist = _dbContext.Teams.Any(x => x.Id == winnerTeamId.Value);
                    if (!winnerTeamIsExist)
                    {
                        season.Winner.AreaId = competition.Area.Id;
                        _dbContext.Teams.Add(season.Winner);
                    }
                }

                season.WinnerId = winnerTeamId;
                season.Winner = null;
                season.CompetitionId = competition.Id;
                season.Competition = null;

                _dbContext.Seasons.AddOrUpdate(season);
            }
            await _dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
