using MesaListo.Application.Interfaces;
using MesaListo.Domain.Entities;
using MesaListo.Infrastructure.Persistence;

namespace MesaListo.Infrastructure.Repositories
{
    public class JuegoRepository : IJuegoRepository
    {
        private readonly SqlStoredProcedureExecutor _executor;

        public JuegoRepository(SqlStoredProcedureExecutor executor)
        {
            _executor = executor;
        }

        public async Task<List<Juego>> ListarActivosAsync()
        {
            return await _executor.ExecuteQueryAsync(
                "dbo.paListarJuegosActivos",
                null,
                reader => new Juego
                {
                    JuegoId = Convert.ToInt32(reader["JuegoId"]),
                    Nombre = reader["Nombre"].ToString() ?? string.Empty,
                    Descripcion = reader["Descripcion"] == DBNull.Value ? null : reader["Descripcion"].ToString(),
                    MinJugadores = Convert.ToInt32(reader["MinJugadores"]),
                    MaxJugadores = Convert.ToInt32(reader["MaxJugadores"]),
                    DuracionMin = reader["DuracionMin"] == DBNull.Value ? null : Convert.ToInt32(reader["DuracionMin"]),
                    Estado = reader["Estado"].ToString() ?? string.Empty
                });
        }
    }
}