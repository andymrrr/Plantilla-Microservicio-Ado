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
            servicio.AddHttpContextAccessor();

            //servicio.AddDbContext<ContextPlantillaMicroServicio>(options =>
            //    options.UseSqlServer(configuracion.GetConnectionString("PlantillaMicroServicio"),
            //        sqlOptions => sqlOptions.MigrationsAssembly(typeof(ContextPlantillaMicroServicio).Assembly.FullName)));

            servicio.AddDbContext<ContextPlantillaMicroServicio>(options =>
                    options.UseInMemoryDatabase("PlantillaMicroServicio"));

            servicio.AddScoped<IPlantillaMicroServicioUoW, PlantillaMicroServicioUoW>();

            servicio.AddScoped(typeof(IRepositorio<>), typeof(Repositorio<>));
            servicio.AddScoped<IAutenticacion, Autenticacion>();

            servicio.Configure<ConfiguracionJWT>(configuracion.GetSection("ConfiguracionJwt"));
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuracion["ConfiguracionJwt:Llave"]!));
            servicio.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(opcion =>
            {
                opcion.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = key,
                    ValidateAudience = false,
                    ValidateIssuer = false
                };
            });

            return servicio;
        }
    }
}
