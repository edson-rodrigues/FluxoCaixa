using ConsolidadoService.Business;

namespace ConsolidadoService.Worker
{
    public class ConsolidadoBackgroundService : BackgroundService
    {
        private readonly ClassConsolidadoService _consolidadoService;

        public ConsolidadoBackgroundService(ClassConsolidadoService consolidadoService)
        {
            _consolidadoService = consolidadoService;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _consolidadoService.IniciarConsumo();
            return Task.CompletedTask;
        }
    }
}
