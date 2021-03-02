using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Vegas.AspNetCore.Common.Attributes;
using Vegas.AspNetCore.Common.Controllers;
using Vegas.FootballDatApp.CQRS.Command;
using Vegas.FootballDatApp.CQRS.Query.Internal;

namespace Vegas.FootballDatApp.Controllers
{
    public class AreasController : ApiControllerBase
    {
        private readonly IMediator _mediator;

        public AreasController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost, ActionName("AddAreas")]
        [ResponseMessage("Areas successfully added")]
        public async Task<IActionResult> AddAreasAsync(CancellationToken cancellationToken)
        {
            await _mediator.Send(new AddAreasCommandRequest(), cancellationToken);
            return OkResponse();
        }

        [HttpGet, ActionName("GetAreas")]
        public async Task<IActionResult> GetAreasAsync(CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(new GetAreasQueryRequest(), cancellationToken);
            return OkResponse(response);
        }
    }
}
