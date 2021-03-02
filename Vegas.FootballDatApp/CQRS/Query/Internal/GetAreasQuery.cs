using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Vegas.FootballDatApp.Contexts;
using Vegas.FootballDatApp.Entities;

namespace Vegas.FootballDatApp.CQRS.Query.Internal
{
    public class GetAreasQueryRequest : IRequest<GetAreasQueryResponse>
    { }

    public class GetAreasQueryResponse
    {
        public List<Area> Areas { get; set; }
    }


    public class GetAreasQueryHandler : IRequestHandler<GetAreasQueryRequest, GetAreasQueryResponse>
    {
        private readonly FootballDbContext _dbContext;

        public GetAreasQueryHandler(FootballDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<GetAreasQueryResponse> Handle(GetAreasQueryRequest request, CancellationToken cancellationToken)
        {
            var areas = await _dbContext.Areas.ToListAsync(cancellationToken: cancellationToken);
            return new GetAreasQueryResponse
            {
                Areas = areas
            };
        }
    }
}
