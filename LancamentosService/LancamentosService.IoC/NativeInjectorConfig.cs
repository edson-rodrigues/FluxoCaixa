using LancamentosService.Data.Mappings.AutoMapper;
using LancamentosService.Data.Repositories;
using LancamentosService.Domain.Interfaces.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

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
            #region Services
            #endregion
        }
    }
}
