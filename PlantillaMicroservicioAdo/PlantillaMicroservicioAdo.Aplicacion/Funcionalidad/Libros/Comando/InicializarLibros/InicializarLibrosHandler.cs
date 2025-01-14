using Bogus;
using MediatR;
using Microsoft.Data.SqlClient;
using PlantillaMicroservicioAdo.Dal.Nucleo.Interfaces;
using PlantillaMicroservicioAdo.Modelo;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlantillaMicroservicioAdo.Aplicacion.Funcionalidad.Libros.Comando.InicializarLibros
{
    public class InicializarLibrosHandler : IRequestHandler<InicializarLibrosComando, bool>
    {

        private readonly IPlantillaMicroservicioAdoUoW _libroRepositorio;

        public InicializarLibrosHandler(IPlantillaMicroservicioAdoUoW libroRepositorio)
        {
            _libroRepositorio = libroRepositorio;
        }
        public async Task<bool> Handle(InicializarLibrosComando request, CancellationToken cancellationToken)
        {
          
            var faker = new Faker<Libro>()
                .RuleFor(l => l.Titulo, f => f.Lorem.Sentence(3))
                .RuleFor(l => l.Autor, f => f.Name.FullName())
                .RuleFor(l => l.Editorial, f => f.Company.CompanyName())
                .RuleFor(l => l.FechaPublicacion, f => f.Date.Past(10));

            var libros = faker.Generate(100);

          
            foreach (var libro in libros)
            {
                var parametros = new[]
                {
                new SqlParameter("@Titulo", SqlDbType.NVarChar) { Value = libro.Titulo },
                new SqlParameter("@Autor", SqlDbType.NVarChar) { Value = libro.Autor },
                new SqlParameter("@Editorial", SqlDbType.NVarChar) { Value = libro.Editorial },
                new SqlParameter("@FechaPublicacion", SqlDbType.Date) { Value = libro.FechaPublicacion }
            };

                _libroRepositorio.Libros.Agregar(
                    query: "INSERT INTO Libros (Titulo, Autor, Editorial, FechaPublicacion) VALUES (@Titulo, @Autor, @Editorial, @FechaPublicacion)",
                    commandType: CommandType.Text,
                    parametros: parametros
                );
            }

            
            await Task.CompletedTask;

            return true;
        }
    }
}
