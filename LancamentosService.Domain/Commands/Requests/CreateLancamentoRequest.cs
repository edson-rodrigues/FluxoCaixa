using MediatR;

namespace LancamentosService.Domain.Commands.Requests
{
    public class CreateLancamentoRequest : IRequest<int>
    {
        public string Tipo { get; set; }

        public decimal Valor { get; set; }

        public string Descricao { get; set; }

        public DateTime DataLancamento { get; set; }
    }
}
