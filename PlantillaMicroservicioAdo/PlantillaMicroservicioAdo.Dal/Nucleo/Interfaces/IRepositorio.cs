using System.Data;

namespace PlantillaMicroservicioAdo.Dal.Nucleo.Interfaces
{
    public interface IRepositorio<T> where T : class
    {
        IEnumerable<T> Buscar(string query, CommandType commandType, params IDataParameter[] parametros);
        T BuscarPorId(string query, CommandType commandType, params IDataParameter[] parametros);
        void Agregar(string query, CommandType commandType, params IDataParameter[] parametros);
        void Actualizar(string query, CommandType commandType, params IDataParameter[] parametros);
        void Eliminar(string query, CommandType commandType, params IDataParameter[] parametros);
    }
}
