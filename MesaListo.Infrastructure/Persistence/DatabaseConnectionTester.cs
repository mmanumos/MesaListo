using MesaListo.Application.Interfaces;

namespace MesaListo.Infrastructure.Persistence
{
    public class DatabaseConnectionTester : IDatabaseConnectionTester
    {
        private readonly SqlConnectionFactory _connectionFactory;

        public DatabaseConnectionTester(SqlConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task<bool> CanConnectAsync()
        {
            using var connection = _connectionFactory.CreateConnection();

            connection.Open();

            await Task.CompletedTask;

            return connection.State == System.Data.ConnectionState.Open;
        }
    }
}