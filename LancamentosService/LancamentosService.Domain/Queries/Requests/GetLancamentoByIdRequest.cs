using LancamentosService.Domain.DTO;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LancamentosService.Domain.Queries.Requests
{
    public class GetLancamentoByIdRequest : IRequest<GetLancamentoDTO>
    {
        public int Id { get; set; }
    }
}
