using AutoMapper;
using LancamentosService.Domain.DTO;
using LancamentosService.Domain.Entities;
using LancamentosService.Domain.Interfaces.Repositories;
using LancamentosService.Domain.Queries.Requests;
using LancamentosService.Domain.Queries.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LancamentosService.Business.Handlers.Queries
{
    public class GetLancamentosPeriodoQueryHandler : IRequestHandler<GetLancamentosPeriodoRequest, GetLancamentosPeriodoResponse>
    {
        private readonly ILancamentoRepository _lancamentoRepository;
        private readonly IMapper _mapper;

        public GetLancamentosPeriodoQueryHandler(ILancamentoRepository lancamentoRepository, IMapper mapper)
        {
            _lancamentoRepository = lancamentoRepository;
            _mapper = mapper;
        }

        public async Task<GetLancamentosPeriodoResponse> Handle(GetLancamentosPeriodoRequest request, CancellationToken cancellationToken)
        {
            var (lancamentos, totalRecords) = await _lancamentoRepository.GetLancamentos(request.InitialDate, request.FinalDate, request.PageSize, request.TipoLancamentoEnum, request.Page);
            GetLancamentosPeriodoResponse response = new GetLancamentosPeriodoResponse();
            response.TotalRecords = totalRecords;
            response.Lancamentos = _mapper.Map<List<GetLancamentoDTO>>(lancamentos);
            return response;         
           }
    }
}
