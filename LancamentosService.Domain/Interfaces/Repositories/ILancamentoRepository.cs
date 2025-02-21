using Azure;
using LancamentosService.Domain.DTO;
using LancamentosService.Domain.Entities;
using LancamentosService.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LancamentosService.Domain.Interfaces.Repositories
{
    public interface ILancamentoRepository : IRepositoryBase<Lancamento>
    {
        Task<(List<Lancamento>, int totalRecords)> GetLancamentos(DateTime? initialDate, DateTime? finalDate, int pageSize = 100, TipoLancamentoEnum? tipoLancamento = null, int page = 1);
        Task<Lancamento> GetLancamentoById(int id);
        Task CreateLancamento(CreateLancamentoDTO lancamentoDTO);

    }
}
