using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using PlantillaMicroservicioAdo.Dal.Datos;
using PlantillaMicroservicioAdo.Dal.Nucleo.Interfaces;
using PlantillaMicroservicioAdo.Dal.Nucleo.Repositorio;
using System.Text;

namespace PlantillaMicroservicioAdo.Dal
{
    public static class Extension
    {
        public static IServiceCollection AddServicioDatos(this IServiceCollection servicio, IConfiguration configuracion)
        {
            var sqliteDb = new SQLiteDatabase();
            string connectionString = sqliteDb.GetConnectionString();

            servicio.AddSingleton(sqliteDb); 
            servicio.AddScoped<IPlantillaMicroservicioAdoUoW>(provider =>
                new PlantillaMicroServicioAdoUoW(connectionString));

            servicio.AddScoped(typeof(IRepositorio<>), typeof(Repositorio<>));




            return servicio;
        }
    }
}
