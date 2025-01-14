using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using PlantillaMicroservicioAdo.Dal.Nucleo.Interfaces;
using PlantillaMicroservicioAdo.Dal.Nucleo.Repositorio;
using System.Text;

namespace PlantillaMicroservicioAdo.Dal
{
    public static class Extension
    {
        public static IServiceCollection AddServicioDatos(this IServiceCollection servicio, IConfiguration configuracion)
        {
            

            //servicio.AddDbContext<ContextPlantillaMicroServicio>(options =>
            //    options.UseSqlServer(configuracion.GetConnectionString("PlantillaMicroServicio"),
            //        sqlOptions => sqlOptions.MigrationsAssembly(typeof(ContextPlantillaMicroServicio).Assembly.FullName)));

      
            servicio.AddScoped<PlantillaMicroServicioUoW, PlantillaMicroServicioUoW>();

            servicio.AddScoped(typeof(IRepositorio<>), typeof(Repositorio<>));
          

          

            return servicio;
        }
    }
}
