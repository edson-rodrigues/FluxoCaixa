using AutoMapper;
using LancamentosService.Data.Context;
using LancamentosService.Domain.DTO;
using LancamentosService.Domain.Entities;
using LancamentosService.Domain.Enums;
using LancamentosService.Domain.Interfaces.Repositories;
using LancamentosService.Domain.Interfaces.Services;
using LancamentosService.MessageBroker.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LancamentosService.Data.Repositories
{
    public class LancamentoRepository : EFRepositoryBase<Lancamento>, ILancamentoRepository
    {
        private readonly IMapper _mapper;
        private readonly IRabbitMQPublisher<Lancamento> _rabbitMQPublisher;
        public LancamentoRepository(AppDbContext context, IMapper mapper, IRabbitMQPublisher<Lancamento> rabbitMQPublisher) : base(context)
        {
            _mapper = mapper;
            _rabbitMQPublisher = rabbitMQPublisher;
        }

        public async Task<int> CreateLancamento(CreateLancamentoDTO lancamentoDTO)
        {
            var lancamento = _mapper.Map<Lancamento>(lancamentoDTO);
            lancamento.DataCriacao = DateTime.Now;
            await db.Lancamentos.AddAsync(lancamento);
            await db.SaveChangesAsync();
            await _rabbitMQPublisher.PublishMessageAsync(lancamento, "lancamentos");
            return lancamento.Id;
        }

        public async Task<Lancamento> GetLancamentoById(int id)
        {
            return await db.Lancamentos.FirstOrDefaultAsync(l => l.Id == id);
        }

        public async Task<(List<Lancamento>, int totalRecords)> GetLancamentos(DateTime? initialDate, DateTime? finalDate, int pageSize = 100, TipoLancamentoEnum? tipoLancamento = null, int page = 1)
        {
            var query = db.Lancamentos.AsQueryable();

            if(tipoLancamento != null)
            {
                if(tipoLancamento == TipoLancamentoEnum.DEBITO)
                {
                    query.Where(l => l.Tipo.Equals("Débito"));
                }
                else
                {
                    query.Where(l => l.Tipo.Equals("Crédito"));
                }
            }

            if(initialDate.HasValue && finalDate.HasValue)
            {
                query = query.Where(l => l.DataLancamento >= initialDate.Value && l.DataLancamento <= finalDate.Value);
            }

            query = query.OrderByDescending(l => l.DataLancamento);

            int totalRecords = await query.CountAsync();

            var lancamentos = await query
           .Skip((page - 1) * pageSize)
           .Take(pageSize)
           .ToListAsync();

            return (lancamentos, totalRecords);
        }
    }
}
