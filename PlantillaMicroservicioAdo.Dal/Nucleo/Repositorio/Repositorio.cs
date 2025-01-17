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
        public async Task<T> ObtenerAsync(string query, CommandType commandType, params SqlParameter[] parametros)
        {
            using var connection = new SqlConnection(_connectionString);
            using var command = new SqlCommand(query, connection)
            {
                CommandType = commandType
            };

            if (parametros != null)
            {
                command.Parameters.AddRange(parametros);
            }

            connection.Open();
            using var reader = await command.ExecuteReaderAsync();
            return  reader.Read() ? Mapear(reader) : default;
        }
        public async Task<bool> ExisteAsync(string query, CommandType commandType, params SqlParameter[]? parametros)
        {
            // Abre la conexión dentro de un bloque using
            using var connection = new SqlConnection(_connectionString);
            using var command = new SqlCommand(query, connection)
            {
                CommandType = commandType
            };

            if (parametros != null)
            {
                command.Parameters.AddRange(parametros); // Agrega los parámetros
            }

            await connection.OpenAsync(); //
            var resultado = await command.ExecuteScalarAsync(); 

            
            return resultado != null && Convert.ToInt32(resultado) > 0;
        }

        private T Mapear(IDataReader reader)
        {
            
            T entidad = Activator.CreateInstance<T>();

           
            var propiedades = typeof(T).GetProperties();

            
            for (int i = 0; i < reader.FieldCount; i++)
            {
                string nombreColumna = reader.GetName(i);

                var propiedad = propiedades.FirstOrDefault(p =>
                    string.Equals(p.Name, nombreColumna, StringComparison.OrdinalIgnoreCase));

                if (propiedad != null && !reader.IsDBNull(i))
                {
                    
                    object valor = Convert.ChangeType(reader.GetValue(i), propiedad.PropertyType);
                    propiedad.SetValue(entidad, valor);
                }
            }

            return entidad;
        }

    }
}
