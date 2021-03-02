using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Vegas.FootballDatApp.Contexts;
using Vegas.FootballDatApp.CQRS.Query.External;

namespace Vegas.FootballDatApp.CQRS.Command
{
    public class AddAreasCommandRequest : IRequest
    { }


    public class AddAreasCommandHandler : IRequestHandler<AddAreasCommandRequest, Unit>
    {
        private readonly FootballDbContext _dbContext;
        private readonly IFootballDataHttpClient _footballDataHttpClient;

        public AddAreasCommandHandler(FootballDbContext dbContext, IFootballDataHttpClient footballDataHttpClient)
        {
            _dbContext = dbContext;
            _footballDataHttpClient = footballDataHttpClient;
        }

        public async Task<Unit> Handle(AddAreasCommandRequest request, CancellationToken cancellationToken)
        {
            var areas = await _footballDataHttpClient.FetchAreasAsync();

            await _dbContext.Areas.AddRangeAsync(areas, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
