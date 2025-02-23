using LancamentosService.Data.Mappings.AutoMapper;
using LancamentosService.Data.Repositories;
using LancamentosService.Domain.Commands.Requests;
using LancamentosService.Domain.DTO;
using LancamentosService.Domain.Interfaces.Repositories;
using LancamentosService.Domain.Queries.Requests;
using LancamentosService.Domain.Queries.Responses;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace LancamentosService.IoC
{
    public static class NativeInjectorConfig
    {
        public static void RegisterServices(this IServiceCollection services, IConfiguration Configuration)
        {
            #region Mediator
            //services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(AppDomain.CurrentDomain.GetAssemblies()));
            
            #endregion

            #region Repository
            services.AddScoped<ILancamentoRepository, LancamentoRepository>();
            #endregion

            #region Mapper
            services.AddAutoMapper(typeof(CreateLancamentoDTOLancamentoMappingProfile));
            services.AddAutoMapper(typeof(GetLancamentoDTOLancamentoMappingProfile));

            #endregion

        }
    }
}
