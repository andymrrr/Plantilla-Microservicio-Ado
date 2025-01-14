using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PlantillaMicroservicioAdo.Aplicacion.Mapeo;
using System.Reflection;

namespace PlantillaMicroservicioAdo.Aplicacion
{
    public static class Extension
    {
        public static void AddServicioAplicacion(this IServiceCollection servicio, IConfiguration config)
        {
            servicio.AddMediatR(opcion => opcion.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));
            var cofiguracionMapeo = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MapeoPerfil());
            });
            IMapper mapeo = cofiguracionMapeo.CreateMapper();
            servicio.AddSingleton(mapeo);
        }
    }
}
