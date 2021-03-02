using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Vegas.AspNetCore.Common.Attributes;
using Vegas.AspNetCore.Common.Controllers;
using Vegas.FootballDatApp.CQRS.Command;

namespace Vegas.FootballDatApp.Controllers
{
    public class TeamsController : ApiControllerBase
    {
        private readonly IMediator _mediator;

        public TeamsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost, ActionName("AddTeamPlayers")]
        [ResponseMessage("Team/Players successfully added/updated")]
        public async Task<IActionResult> AddTeamPlayersAsync([FromBody] AddTeamPlayersCommandRequest request, CancellationToken cancellationToken)
        {
            await _mediator.Send(request, cancellationToken);
            return OkResponse();
        }
    }
}
