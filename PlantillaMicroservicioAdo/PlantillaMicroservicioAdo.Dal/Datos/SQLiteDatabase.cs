using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlantillaMicroservicioAdo.Dal.Datos
{
    public class SQLiteDatabase
    {
        private readonly string _connectionString;

        public SQLiteDatabase()
        {
            _connectionString = "Data Source=:memory:;Mode=Memory;Cache=Shared";
            CrearBaseDeDatos();
        }

        public string GetConnectionString() => _connectionString;

        private void CrearBaseDeDatos()
        {
            using var connection = new SqliteConnection(_connectionString);
            connection.Open();

            var crearTablaLibros = @"
            CREATE TABLE Libros (
                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                Titulo TEXT NOT NULL,
                Autor TEXT NOT NULL,
                FechaPublicacion TEXT NOT NULL
            );";

            using var command = new SqliteCommand(crearTablaLibros, connection);
            command.ExecuteNonQuery();
        }
    }
}