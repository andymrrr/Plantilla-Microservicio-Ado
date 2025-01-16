using MediatR;
using PlantillaMicroservicioAdo.Aplicacion.Funcionalidad.Libros.Vm;

namespace PlantillaMicroservicioAdo.Aplicacion.Funcionalidad.Libros.Consulta.BuscarLibroId
{
    public class BuscarLibroIdConsulta : IRequest<LibroVm>
    {
        public int? id { get; set; }
        public BuscarLibroIdConsulta(int? libroId)
        {

            if (libroId is null || libroId < 1)
            {
                throw new ArgumentException("El ID del libro no puede estar vacio", nameof(libroId));
            }

            id = libroId;
        }

    }
}
