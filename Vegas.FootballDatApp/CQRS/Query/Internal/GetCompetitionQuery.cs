using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Vegas.FootballDatApp.Contexts;
using Vegas.FootballDatApp.Entities;

namespace Vegas.FootballDatApp.CQRS.Query.Internal
{
    public class GetCompetitionQueryRequest : IRequest<GetCompetitionQueryResponse>
    {
        public int CompetitionId { get; private set; }
        public GetCompetitionQueryRequest(int competitionId)
        {
            CompetitionId = competitionId;
        }
    }

    public class GetCompetitionQueryResponse
    {
        public Competition Competition { get; set; }
    }


    public class GetCompetitionQueryHandler : IRequestHandler<GetCompetitionQueryRequest, GetCompetitionQueryResponse>
    {
        private readonly FootballDbContext _dbContext;
        public GetCompetitionQueryHandler(FootballDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<GetCompetitionQueryResponse> Handle(GetCompetitionQueryRequest request, CancellationToken ct)
        {
            var competition = await _dbContext.Competitions.FirstOrDefaultAsync(x => x.Id == request.CompetitionId);
            return new GetCompetitionQueryResponse
            {
                Competition = competition
            };
        }
    }
}
