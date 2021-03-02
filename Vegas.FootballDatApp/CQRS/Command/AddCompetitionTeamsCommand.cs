using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Vegas.FootballDatApp.Contexts;
using Vegas.FootballDatApp.CQRS.Query.External;
using Vegas.FootballDatApp.Entities;

namespace Vegas.FootballDatApp.CQRS.Command
{
    public class AddCompetitionTeamsCommandRequest : IRequest
    {
        public int CompetitionId { get; private set; }
        public string Season { get; private set; }

        public AddCompetitionTeamsCommandRequest(int competitionId, string season)
        {
            CompetitionId = competitionId;
            Season = season;
        }
    }


    public class AddCompetitionTeamsCommandHandler : IRequestHandler<AddCompetitionTeamsCommandRequest, Unit>
    {
        private readonly FootballDbContext _dbContext;
        private readonly IFootballDataHttpClient _footballDataHttpClient;

        public AddCompetitionTeamsCommandHandler(FootballDbContext dbContext, IFootballDataHttpClient footballDataHttpClient)
        {
            _dbContext = dbContext;
            _footballDataHttpClient = footballDataHttpClient;
        }

        public async Task<Unit> Handle(AddCompetitionTeamsCommandRequest request, CancellationToken cancellationToken)
        {
            var competitionId = request.CompetitionId;
            var teams = await _footballDataHttpClient.FetchCompetitionTeamsAsync(competitionId, request.Season);
            foreach (var team in teams)
            {
                team.AreaId = team.Area.Id;
                team.Area = null;
                _dbContext.Teams.AddOrUpdate(team);

                var teamCompetitionAssignments = await _dbContext.TeamCompetitionAssignments
                    .Where(x => x.TeamId == team.Id && x.CompetitionId == competitionId)
                    .ToListAsync(cancellationToken);

                _dbContext.RemoveRange(teamCompetitionAssignments);
                _dbContext.Add(new TeamCompetitionAssignment
                {
                    TeamId = team.Id,
                    CompetitionId = competitionId
                });
            }
            await _dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
