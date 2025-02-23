using LancamentosService.Domain.DTO;
using LancamentosService.Domain.Enums;
using LancamentosService.Domain.Queries.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LancamentosService.Domain.Queries.Requests
{
    public class GetLancamentosPeriodoRequest : IRequest<GetLancamentosPeriodoResponse>
    {
        public DateTime? InitialDate { get; set; }
        public DateTime? FinalDate { get; set; }
        public TipoLancamentoEnum? TipoLancamentoEnum { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }

    }
}
