using AutoMapper;
using LancamentosService.Domain.DTO;
using LancamentosService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LancamentosService.Data.Mappings.AutoMapper
{
    public class GetLancamentoDTOLancamentoMappingProfile : Profile
    {
        public GetLancamentoDTOLancamentoMappingProfile()
        {
            CreateMap<Lancamento, GetLancamentoDTO>().ReverseMap();
        }
    }
}
