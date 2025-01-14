using AutoMapper;
using PlantillaMicroservicioAdo.Aplicacion.Funcionalidad.Libros.Vm;
using PlantillaMicroservicioAdo.Modelo;

namespace PlantillaMicroservicioAdo.Aplicacion.Mapeo
{
    public class MapeoPerfil : Profile
    {
        public MapeoPerfil()
        {
            CreateMap<Libro, LibroVm>().ReverseMap();
        }
    }
}
