using LancamentosService.Business.Handlers.Queries;
using LancamentosService.Domain.Commands.Requests;
using LancamentosService.Domain.Enums;
using LancamentosService.Domain.Queries.Requests;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace LancamentosService.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LancamentoController : ControllerBase
    {
        private readonly IMediator _mediator;

        public LancamentoController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("Get")]
        public async Task<IActionResult> GetAsync([FromQuery] int lancamentoId)
        {
            var response = await _mediator.Send(new GetLancamentoByIdRequest() { Id = lancamentoId} );
            if(response == null)
            {
                return NotFound();
            }
            return Ok(response);
        }

        [HttpGet("GetLancamentosPeriodo")]
        public async Task<IActionResult> GetLancamentosPeriodoAsync(
            [FromQuery]DateTime? initialDate = null,
            [FromQuery]DateTime? finalDate = null,
            [FromQuery]int pageSize = 100,
            [FromQuery]int page = 1,
            [FromQuery]TipoLancamentoEnum? tipoLancamento = null
            )
        {
            var response = await _mediator.Send(
                new GetLancamentosPeriodoRequest()
                {
                    InitialDate = initialDate,
                    FinalDate = finalDate,
                    PageSize = pageSize,
                    Page = page,
                    TipoLancamentoEnum = tipoLancamento
                }
                );
            if (response.TotalRecords == 0)
            {
                return NotFound();
            }
            return Ok(response);
        }

        [HttpPost("Create")]
        public async Task<IActionResult> CreateLancamentoAsync([FromBody] CreateLancamentoRequest createLancamento)
        {
            var response = await _mediator.Send(createLancamento);
            return Ok(response);
        }

    }
}
