using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LancamentosService.Domain.DTO
{
    public class GetLancamentoDTO
    {
        public int Id { get; set; }

        public string Tipo { get; set; }

        public decimal Valor { get; set; }

        public string Descricao { get; set; }

        public DateTime DataLancamento { get; set; }

        public DateTime DataCriacao { get; set; }
    }
}
