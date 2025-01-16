using AutoMapper;
using MediatR;
using Microsoft.Data.SqlClient;
using PlantillaMicroservicioAdo.Aplicacion.Funcionalidad.Libros.Vm;
using PlantillaMicroservicioAdo.Dal.Nucleo.Interfaces;
using System.Data;

namespace PlantillaMicroservicioAdo.Aplicacion.Funcionalidad.Libros.Consulta.BuscarLibroId
{
    internal class BuscarLibroIdHandler : IRequestHandler<BuscarLibroIdConsulta, LibroVm>
    {
        private readonly IMapper _mapper;
        private readonly IPlantillaMicroservicioAdoUoW _context;

        public BuscarLibroIdHandler(IMapper mapper, IPlantillaMicroservicioAdoUoW context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<LibroVm> Handle(BuscarLibroIdConsulta request, CancellationToken cancellationToken)
        {

            if (request.id < 1)
            {
                throw new ArgumentException("El id proporcionado no es válido.");
            }


            const string query = "SELECT Id, Titulo, Autor, FechaPublicacion FROM Libros WHERE Id = @Id";

            var parametros = new[]
            {
            new SqlParameter("@Id", SqlDbType.Int) { Value = request.id }
        };


            var libro = await _context.Libros.ObtenerAsync(query, CommandType.Text, parametros);


            if (libro is null)
            {
                throw new KeyNotFoundException($"No se encontró un libro con el ID: {request.id}");
            }


            var libroVm = _mapper.Map<LibroVm>(libro);

            return libroVm;
        }
    }

}
