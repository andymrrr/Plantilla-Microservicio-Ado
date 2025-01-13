
using PlantillaMicroservicioAdo.Modelo;

namespace PlantillaMicroservicioAdo.Dal.Nucleo.Interfaces
{
    public interface IPlantillaMicroservicioAdoUoW : IDisposable
    {
        IRepositorio<Libro> Libros { get; }
        Task BeginAsync();
        Task CommitAsync();
        Task RollbackAsync();
        void GuardarCambios();
        Task GuardarCambiosAsync();
    }
}
