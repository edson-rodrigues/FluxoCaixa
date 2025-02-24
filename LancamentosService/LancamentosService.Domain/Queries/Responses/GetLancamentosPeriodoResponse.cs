using LancamentosService.Domain.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LancamentosService.Domain.Queries.Responses
{
    public class GetLancamentosPeriodoResponse
    {
        public int TotalRecords { get; set; }
        public List<GetLancamentoDTO>? Lancamentos { get; set; }
    }
}
