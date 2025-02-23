using LancamentosService.Domain.Commands.Requests;
using LancamentosService.Domain.DTO;
using LancamentosService.Domain.Interfaces.Repositories;
using MediatR;

namespace LancamentosService.Business.Handlers.Commands
{
    public class CreateLancamentoCommandHandler : IRequestHandler<CreateLancamentoRequest, int>
    {
        private readonly ILancamentoRepository _lancamentoRepository;

        public CreateLancamentoCommandHandler(ILancamentoRepository lancamentoRepository)
        {
            _lancamentoRepository = lancamentoRepository;
        }

        async Task<int> IRequestHandler<CreateLancamentoRequest, int>.Handle(CreateLancamentoRequest request, CancellationToken cancellationToken)
        {
            var lancamento = new CreateLancamentoDTO
            {
                Tipo = request.Tipo,
                Valor = request.Valor,
                Descricao = request.Descricao,
                DataLancamento = request.DataLancamento
            };

            return await _lancamentoRepository.CreateLancamento(lancamento);
        }
    }
}
