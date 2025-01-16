using Microsoft.Data.SqlClient;
using System.Data;

namespace PlantillaMicroservicioAdo.Dal.Nucleo.Interfaces
{
    public interface IRepositorio<T> where T : class
    {
        IEnumerable<T> Buscar(string query, CommandType commandType, params IDataParameter[] parametros);
        Task<T> ObtenerAsync(string query, CommandType commandType, params SqlParameter[] parametros);
        Task<bool> ExisteAsync(string query, CommandType commandType, params SqlParameter[] parametros);
        T BuscarPorId(string query, CommandType commandType, params IDataParameter[] parametros);
        void Agregar(string query, CommandType commandType, params IDataParameter[] parametros);
        void Actualizar(string query, CommandType commandType, params IDataParameter[] parametros);
        void Eliminar(string query, CommandType commandType, params IDataParameter[] parametros);
    }
}
