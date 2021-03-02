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
    public class CompetitionsController : ApiControllerBase
    {
        private readonly IMediator _mediator;

        public CompetitionsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost, ActionName("AddCompetitions")]
        [ResponseMessage("Competitions successfully added/updated")]
        public async Task<IActionResult> AddCompetitionsAsync(CancellationToken cancellationToken)
        {
            await _mediator.Send(new AddCompetitionsCommandRequest(), cancellationToken);
            return OkResponse();
        }

        [HttpPost, ActionName("AddCompetitionTeams")]
        [ResponseMessage("Competition/Teams successfully added/updated")]
        public async Task<IActionResult> AddCompetitionTeamsAsync([FromBody]AddCompetitionTeamsCommandRequest request, CancellationToken cancellationToken)
        {
            await _mediator.Send(request, cancellationToken);
            return OkResponse();
        }

        [HttpPost, ActionName("AddCompetitionSeasons")]
        [ResponseMessage("Competition/Seasons successfully added/updated")]
        public async Task<IActionResult> AddCompetitionSeasonsAsync([FromBody]AddCompetitionSeasonsCommandRequest request, CancellationToken cancellationToken)
        {
            await _mediator.Send(request, cancellationToken);
            return OkResponse();
        }

        [HttpGet, ActionName("GetCompetitions")]
        public async Task<IActionResult> GetCompetitionsAsync(CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(new GetCompetitionsQueryRequest(), cancellationToken);
            return OkResponse(response);
        }

        [HttpGet, ActionName("GetCompetition")]
        public async Task<IActionResult> GetCompetitionAsync([FromQuery]int competitionId, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(new GetCompetitionQueryRequest(competitionId), cancellationToken);
            return OkResponse(response);
        }
    }
}
