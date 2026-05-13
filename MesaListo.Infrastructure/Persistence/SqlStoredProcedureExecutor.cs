using Microsoft.Data.SqlClient;
using System.Data;

namespace MesaListo.Infrastructure.Persistence
{
    public class SqlStoredProcedureExecutor
    {
        private readonly SqlConnectionFactory _connectionFactory;

        public SqlStoredProcedureExecutor(SqlConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task<List<T>> ExecuteQueryAsync<T>(
            string storedProcedureName,
            IEnumerable<SqlParameter>? parameters,
            Func<SqlDataReader, T> mapFunction)
        {
            var result = new List<T>();

            await using var connection = _connectionFactory.CreateConnection();
            await using var command = new SqlCommand(storedProcedureName, connection);

            command.CommandType = CommandType.StoredProcedure;

            if (parameters != null)
            {
                command.Parameters.AddRange(parameters.ToArray());
            }

            await connection.OpenAsync();

            await using var reader = await command.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                result.Add(mapFunction(reader));
            }

            return result;
        }

        public async Task<int> ExecuteNonQueryAsync(
            string storedProcedureName,
            IEnumerable<SqlParameter>? parameters)
        {
            await using var connection = _connectionFactory.CreateConnection();
            await using var command = new SqlCommand(storedProcedureName, connection);

            command.CommandType = CommandType.StoredProcedure;

            if (parameters != null)
            {
                command.Parameters.AddRange(parameters.ToArray());
            }

            await connection.OpenAsync();

            return await command.ExecuteNonQueryAsync();
        }
    }
}