using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Vegas.FootballDatApp.Contexts;
using Vegas.FootballDatApp.CQRS.Query.External;

namespace Vegas.FootballDatApp.CQRS.Command
{
    public class AddTeamPlayersCommandRequest : IRequest
    {
        public int TeamId { get; private set; }
        public AddTeamPlayersCommandRequest(int teamId)
        {
            TeamId = teamId;
        }
    }


    public class AddTeamPlayersCommandHandler : IRequestHandler<AddTeamPlayersCommandRequest, Unit>
    {
        private readonly FootballDbContext _dbContext;
        private readonly IFootballDataHttpClient _footballDataHttpClient;

        public AddTeamPlayersCommandHandler(FootballDbContext dbContext, IFootballDataHttpClient footballDataHttpClient)
        {
            _dbContext = dbContext;
            _footballDataHttpClient = footballDataHttpClient;
        }

        public async Task<Unit> Handle(AddTeamPlayersCommandRequest request, CancellationToken cancellationToken)
        {
            var team = await _footballDataHttpClient.FetchTeamAsync(request.TeamId);
            var existingTeamPlayers = await _dbContext.Players.Where(x => x.TeamId == team.Id).ToListAsync(cancellationToken);
            _dbContext.Players.RemoveRange(existingTeamPlayers);

            foreach (var player in team.Squad)
            {
                player.TeamId = request.TeamId;
                _dbContext.Players.Add(player);
            }
            await _dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
