using System;
using System.Data;
using Microsoft.Data.SqlClient;

namespace PlantillaMicroservicioAdo.Dal.Datos
{
    public class SqlServerInMemoryDatabase
    {
        private readonly string _connectionString;

        public SqlServerInMemoryDatabase()
        {
            _connectionString = "Data Source=s462-Intradb02;Database=tempdb;User ID=intranet;Password=05equipo;TrustServerCertificate=True;";
            CrearBaseDeDatos();
        }

        public string GetConnectionString() => _connectionString;

        private void CrearBaseDeDatos()
        {
            using var connection = new SqlConnection(_connectionString);
            connection.Open();

            // Crear la tabla 'Libros' si no existe
            var crearTablaLibros = @"
            IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Libros')
            BEGIN
                CREATE TABLE Libros (
                    Id INT IDENTITY PRIMARY KEY,
                    Titulo NVARCHAR(MAX) NOT NULL,
                    Editorial NVARCHAR(MAX) NOT NULL,
                    Autor NVARCHAR(MAX) NOT NULL,
                    FechaPublicacion DATE NOT NULL
                );
            END;";

            using var command = new SqlCommand(crearTablaLibros, connection);
            command.ExecuteNonQuery();
        }
    }
}
