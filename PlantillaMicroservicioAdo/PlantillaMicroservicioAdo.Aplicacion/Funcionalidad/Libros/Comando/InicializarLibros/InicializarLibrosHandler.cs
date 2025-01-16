using Bogus;
using MediatR;
using Microsoft.Data.SqlClient;
using PlantillaMicroservicioAdo.Dal.Nucleo.Interfaces;
using PlantillaMicroservicioAdo.Modelo;
using System.Data;


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
                .RuleFor(l => l.FechaPublicacion, f => f.Date.Past(10));

            var libros = faker.Generate(100);

            try
            {
                const string consulta = "SELECT TOP 1 * FROM Libros";
                
                bool resultado = await _libroRepositorio.Libros.ExisteAsync(
                     consulta,
                    CommandType.Text,
                    null
                    );
                if (!resultado)
                {
                    foreach (var libro in libros)
                    {
                        var parametros = new[]
                        {
                        new SqlParameter("@Titulo", DbType.String) { Value = libro.Titulo },
                        new SqlParameter("@Autor", DbType.String) { Value = libro.Autor },
                        new SqlParameter("@FechaPublicacion", DbType.Date) { Value = libro.FechaPublicacion }
                    };

                        _libroRepositorio.Libros.Agregar(
                            query: "INSERT INTO Libros (Titulo, Autor, FechaPublicacion) VALUES (@Titulo, @Autor, @FechaPublicacion)",
                            commandType: CommandType.Text,
                            parametros: parametros
                        );
                    }
                }
              

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al inicializar los libros", ex);
            }
        }
    }
}
