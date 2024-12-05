using Microsoft.Extensions.DependencyInjection;
using Repository.Implements;
using Repository.Interfaces;
using Services.Implements;
using Services.Interfaces;

namespace Api.Extention
{
    public static class DIContainer
    {
        public static void ConfigDI(this IServiceCollection service, IConfiguration configuration)
        {
            service.AddTransient<IEmpolyeeService, EmpolyeeService>();
            service.AddTransient<IEmpolyeeRepository, EmpolyeeRepository>();
           
        }
    }
}
