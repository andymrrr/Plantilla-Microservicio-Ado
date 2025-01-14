using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using PlantillaMicroservicioAdo.Dal.Nucleo.Interfaces;

namespace PlantillaMicroservicioAdo.Dal.Nucleo.Repositorio
{
    public class Repositorio<T> : IRepositorio<T> where T : class
    {
        private readonly string _connectionString;

        public Repositorio(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IEnumerable<T> Buscar(string query, CommandType commandType, params IDataParameter[] parametros)
        {
            using var connection = new SqlConnection(_connectionString);
            using var command = new SqlCommand(query, connection) { CommandType = commandType };
            if (parametros != null)
                command.Parameters.AddRange(parametros);

            connection.Open();
            using var reader = command.ExecuteReader();
            var results = new List<T>();
            while (reader.Read())
            {
                var entity = Mapear(reader);
                results.Add(entity);
            }
            return results;
        }

        public T BuscarPorId(string query, CommandType commandType, params IDataParameter[] parametros)
        {
            using var connection = new SqlConnection(_connectionString);
            using var command = new SqlCommand(query, connection) { CommandType = commandType };
            if (parametros != null)
                command.Parameters.AddRange(parametros);

            connection.Open();
            using var reader = command.ExecuteReader();
            return reader.Read() ? Mapear(reader) : null!;
        }

        public void Agregar(string query, CommandType commandType, params IDataParameter[] parametros)
        {
            EjecutarNoQuery(query, commandType, parametros);
        }

        public void Actualizar(string query, CommandType commandType, params IDataParameter[] parametros)
        {
            EjecutarNoQuery(query, commandType, parametros);
        }

        public void Eliminar(string query, CommandType commandType, params IDataParameter[] parametros)
        {
            EjecutarNoQuery(query, commandType, parametros);
        }

        private void EjecutarNoQuery(string query, CommandType commandType, params IDataParameter[] parametros)
        {
            using var connection = new SqlConnection(_connectionString);
            using var command = new SqlCommand(query, connection) { CommandType = commandType };
            if (parametros != null)
                command.Parameters.AddRange(parametros);

            connection.Open();
            command.ExecuteNonQuery();
        }

        private T Mapear(IDataReader reader)
        {
            throw new NotImplementedException("Implementar el mapeo para la entidad.");
        }
    }
}
