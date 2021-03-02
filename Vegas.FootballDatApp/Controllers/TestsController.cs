using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Vegas.AspNetCore.Common.Attributes;
using Vegas.AspNetCore.Common.Controllers;
using Vegas.FootballDatApp.Contexts;
using Vegas.FootballDatApp.CQRS.Command;

namespace Vegas.FootballDatApp.Controllers
{
    public class TestsController : ApiControllerBase
    {
        private readonly FootballDbContext _dbContext;
        private readonly IMediator _mediator;

        public TestsController(FootballDbContext dbContext, IMediator mediator)
        {
            _dbContext = dbContext;
            _mediator = mediator;
        }

        [HttpGet, ActionName("GetTest")]
        public async Task<IActionResult> GetTestAsync()
        {
            var competition = _dbContext.Competitions.Include(x => x.Area).FirstOrDefault();
            var competition2 = _dbContext.Competitions.Include("Area").FirstOrDefault();

            await Task.Delay(100);

            return OkResponse(new
            {
                Key = "123"
            });
        }
        
    }
}
