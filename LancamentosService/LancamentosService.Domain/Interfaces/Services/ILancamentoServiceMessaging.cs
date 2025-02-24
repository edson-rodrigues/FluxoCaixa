using LancamentosService.Domain.DTO;
using LancamentosService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LancamentosService.Domain.Interfaces.Services
{
    public interface ILancamentoServiceMessaging
    {
        Task PublishLancamento(Lancamento lancamento);
    }
}
