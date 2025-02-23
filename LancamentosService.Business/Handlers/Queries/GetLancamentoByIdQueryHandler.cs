using AutoMapper;
using LancamentosService.Domain.DTO;
using LancamentosService.Domain.Interfaces.Repositories;
using LancamentosService.Domain.Queries.Requests;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace LancamentosService.Business.Handlers.Queries
{
    public class GetLancamentoByIdQueryHandler : IRequestHandler<GetLancamentoByIdRequest, GetLancamentoDTO>
    {
        private readonly ILancamentoRepository _lancamentoRepository;
        private readonly IMapper _mapper;
        public GetLancamentoByIdQueryHandler(ILancamentoRepository lancamentoRepository, IMapper mapper)
        {
            _lancamentoRepository = lancamentoRepository;
            _mapper = mapper;
        }

        public async Task<GetLancamentoDTO> Handle(GetLancamentoByIdRequest request, CancellationToken cancellationToken)
        {
            var lancamento = await _lancamentoRepository.GetLancamentoById(request.Id);

            return _mapper.Map<GetLancamentoDTO>(lancamento);
        }
    }
}
