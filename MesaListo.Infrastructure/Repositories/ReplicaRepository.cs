using MesaListo.Application.DTOs.Replicas;
using MesaListo.Application.Interfaces;
using MesaListo.Infrastructure.Persistence;
using Microsoft.Data.SqlClient;

namespace MesaListo.Infrastructure.Repositories
{
    public class ReplicaRepository : IReplicaRepository
    {
        private readonly SqlStoredProcedureExecutor _executor;

        public ReplicaRepository(SqlStoredProcedureExecutor executor)
        {
            _executor = executor;
        }

        public async Task<List<ReplicaResumenDto>> ListarReplicasPorNoticiaAsync(int usuarioId, int noticiaId)
        {
            try
            {
                List<SqlParameter> parameters = new List<SqlParameter>
                {
                    new SqlParameter("@UsuarioId", usuarioId),
                    new SqlParameter("@NoticiaId", noticiaId)
                };

                List<ReplicaResumenDto> replicas = await _executor.ExecuteQueryAsync(
                    "dbo.paListarReplicasPorNoticia",
                    parameters,
                    reader => new ReplicaResumenDto
                    {
                        ReplicaId = Convert.ToInt32(reader["ReplicaId"]),
                        NoticiaId = Convert.ToInt32(reader["NoticiaId"]),
                        UsuarioId = Convert.ToInt32(reader["UsuarioId"]),
                        Nombres = reader["Nombres"].ToString() ?? string.Empty,
                        Apellidos = reader["Apellidos"].ToString() ?? string.Empty,
                        Alias = reader["Alias"] == DBNull.Value ? null : reader["Alias"].ToString(),
                        Contenido = reader["Contenido"].ToString() ?? string.Empty,
                        FechaCreacion = Convert.ToDateTime(reader["FechaCreacion"]),
                        Estado = reader["Estado"].ToString() ?? string.Empty,
                        UsuarioEsMiembro = Convert.ToBoolean(reader["UsuarioEsMiembro"]),
                        CantidadReportes = Convert.ToInt32(reader["CantidadReportes"])
                    }
                );

                return replicas;
            }
            catch (Exception ex)
            {
                throw new Exception("Error consultando las réplicas de la noticia.", ex);
            }
        }



        public async Task<ResultadoCrearReplicaDbDto> CrearReplicaAsync(CrearReplicaRequestDto request)
        {
            try
            {
                List<SqlParameter> parameters = new List<SqlParameter>
        {
            new SqlParameter("@UsuarioId", request.UsuarioId),
            new SqlParameter("@NoticiaId", request.NoticiaId),
            new SqlParameter("@Contenido", request.Contenido)
        };

                List<ResultadoCrearReplicaDbDto> resultado = await _executor.ExecuteQueryAsync(
                    "dbo.paCrearReplicaNoticia",
                    parameters,
                    reader => new ResultadoCrearReplicaDbDto
                    {
                        Success = Convert.ToBoolean(reader["Success"]),
                        Message = reader["Message"].ToString() ?? string.Empty,
                        ReplicaId = reader["ReplicaId"] == DBNull.Value ? null : Convert.ToInt32(reader["ReplicaId"]),
                        NoticiaId = reader["NoticiaId"] == DBNull.Value ? null : Convert.ToInt32(reader["NoticiaId"]),
                        UsuarioId = reader["UsuarioId"] == DBNull.Value ? null : Convert.ToInt32(reader["UsuarioId"]),
                        Nombres = reader["Nombres"] == DBNull.Value ? null : reader["Nombres"].ToString(),
                        Apellidos = reader["Apellidos"] == DBNull.Value ? null : reader["Apellidos"].ToString(),
                        Alias = reader["Alias"] == DBNull.Value ? null : reader["Alias"].ToString(),
                        Contenido = reader["Contenido"] == DBNull.Value ? null : reader["Contenido"].ToString(),
                        FechaCreacion = reader["FechaCreacion"] == DBNull.Value ? null : Convert.ToDateTime(reader["FechaCreacion"]),
                        Estado = reader["Estado"] == DBNull.Value ? null : reader["Estado"].ToString(),
                        UsuarioEsMiembro = Convert.ToBoolean(reader["UsuarioEsMiembro"]),
                        CantidadReportes = Convert.ToInt32(reader["CantidadReportes"])
                    }
                );

                ResultadoCrearReplicaDbDto? replicaCreada = resultado.FirstOrDefault();

                if (replicaCreada == null)
                {
                    return new ResultadoCrearReplicaDbDto
                    {
                        Success = false,
                        Message = "No se obtuvo respuesta al crear la réplica."
                    };
                }

                return replicaCreada;
            }
            catch (Exception ex)
            {
                return new ResultadoCrearReplicaDbDto
                {
                    Success = false,
                    Message = $"Error creando réplica: {ex.Message}"
                };
            }
        }





    }
}