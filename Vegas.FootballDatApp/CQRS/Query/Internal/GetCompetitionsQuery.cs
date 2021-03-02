using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Vegas.FootballDatApp.Contexts;
using Vegas.FootballDatApp.Entities;

namespace Vegas.FootballDatApp.CQRS.Query.Internal
{
    public class GetCompetitionsQueryRequest : IRequest<GetCompetitionsQueryResponse>
    { }

    public class GetCompetitionsQueryResponse
    {
        public List<Competition> Competitions { get; set; }
    }

    public class GetCompetitionsQueryHandler : IRequestHandler<GetCompetitionsQueryRequest, GetCompetitionsQueryResponse>
    {
        private readonly FootballDbContext _dbContext;

        public GetCompetitionsQueryHandler(FootballDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<GetCompetitionsQueryResponse> Handle(GetCompetitionsQueryRequest request, CancellationToken ct)
        {
            var competitions = await _dbContext.Competitions.ToListAsync();
            return new GetCompetitionsQueryResponse
            {
                Competitions = competitions
            };
        }
    }
}
