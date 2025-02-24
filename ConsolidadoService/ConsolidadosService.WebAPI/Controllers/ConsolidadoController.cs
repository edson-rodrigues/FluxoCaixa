using ConsolidadoService.Business;
using Microsoft.AspNetCore.Mvc;

namespace ConsolidadosService.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ConsolidadoController : ControllerBase
    {
        private readonly ClassConsolidadoService _consolidadoService;

        public ConsolidadoController(ClassConsolidadoService consolidadoService)
        {
            _consolidadoService = consolidadoService;
        }

        [HttpGet]
        public IActionResult GetSaldoConsolidado()
        {
            var saldo = _consolidadoService.ObterSaldoConsolidado();
            return Ok(saldo);
        }
    }
}
