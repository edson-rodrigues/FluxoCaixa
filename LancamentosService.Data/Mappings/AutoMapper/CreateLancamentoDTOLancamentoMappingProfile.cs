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
    public class CreateLancamentoDTOLancamentoMappingProfile : Profile
    {
        public CreateLancamentoDTOLancamentoMappingProfile()
        {
            CreateMap<Lancamento, CreateLancamentoDTO>().ReverseMap();
        }
    }
}
