using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Vegas.FootballDatApp.Contexts;
using Vegas.FootballDatApp.CQRS.Query.External;
using Vegas.FootballDatApp.Entities;

namespace Vegas.FootballDatApp.CQRS.Command
{
    public class AddCompetitionsCommandRequest : IRequest
    { }


    public class AddCompetitionsCommandHandler : IRequestHandler<AddCompetitionsCommandRequest, Unit>
    {
        private readonly FootballDbContext _dbContext;
        private readonly IFootballDataHttpClient _footballDataHttpClient;
        private readonly List<string> CupCodes = new List<string> { "CL", "WC", "EC" };

        public AddCompetitionsCommandHandler(FootballDbContext dbContext, IFootballDataHttpClient footballDataHttpClient)
        {
            _dbContext = dbContext;
            _footballDataHttpClient = footballDataHttpClient;
        }

        public async Task<Unit> Handle(AddCompetitionsCommandRequest request, CancellationToken cancellationToken)
        {
            var competitions = await _footballDataHttpClient.FetchCompetitionsAsync();
            foreach (var competition in competitions)
            {
                if (competition.Id == 2013 || competition.Id == 2016)
                {
                    continue;
                }
                competition.Type = CupCodes.Contains(competition.Code) ? CompetitionType.Cup : CompetitionType.League;
                competition.AreaId = competition.Area.Id;
                competition.Area = null;

                _dbContext.Competitions.AddOrUpdate(competition);
            }
            await _dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
