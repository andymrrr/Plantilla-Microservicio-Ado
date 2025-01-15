using System;
using System.Data;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using PlantillaMicroservicioAdo.Dal.Nucleo.Interfaces;
using PlantillaMicroservicioAdo.Modelo;

namespace PlantillaMicroservicioAdo.Dal.Nucleo.Repositorio
{
    public class PlantillaMicroServicioAdoUoW : IPlantillaMicroservicioAdoUoW
    {
        private readonly string _connectionString;
        private IDbTransaction _transaction;
        private IDbConnection _connection;

        public IRepositorio<Libro> Libros { get; }

        public PlantillaMicroServicioAdoUoW(string connectionString)
        {
            _connectionString = connectionString;
            _connection = new SqlConnection(_connectionString);
            Libros = new Repositorio<Libro>(_connectionString);
        }

        public async Task BeginAsync()
        {
            _connection.Open();
            _transaction = _connection.BeginTransaction();
        }

        public async Task CommitAsync()
        {
            _transaction.Commit();
            _transaction.Dispose();
            _transaction = null;
        }

        public async Task RollbackAsync()
        {
            _transaction.Rollback();
            _transaction.Dispose();
            _transaction = null;
        }

        public void Dispose()
        {
            _transaction?.Dispose();
            _connection?.Dispose();
        }

        public void GuardarCambios()
        {
            throw new NotImplementedException();
        }

        public Task GuardarCambiosAsync()
        {
            throw new NotImplementedException();
        }
    }
}
