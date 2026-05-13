using MesaListo.Application.DTOs.Noticias;
using MesaListo.Application.Interfaces;
using MesaListo.Infrastructure.Persistence;
using Microsoft.Data.SqlClient;

namespace MesaListo.Infrastructure.Repositories
{
    public class NoticiaRepository : INoticiaRepository
    {
        private readonly SqlStoredProcedureExecutor _executor;

        public NoticiaRepository(SqlStoredProcedureExecutor executor)
        {
            _executor = executor;
        }

        public async Task<List<NoticiaResumenDto>> ListarNoticiasPorComunidadAsync(int usuarioId, int comunidadId)
        {
            try
            {
                List<SqlParameter> parameters = new List<SqlParameter>
                {
                    new SqlParameter("@UsuarioId", usuarioId),
                    new SqlParameter("@ComunidadId", comunidadId)
                };

                List<NoticiaResumenDto> noticias = await _executor.ExecuteQueryAsync(
                    "dbo.paListarNoticiasPorComunidad",
                    parameters,
                    reader => new NoticiaResumenDto
                    {
                        NoticiaId = Convert.ToInt32(reader["NoticiaId"]),
                        ComunidadId = Convert.ToInt32(reader["ComunidadId"]),
                        UsuarioId = Convert.ToInt32(reader["UsuarioId"]),
                        Nombres = reader["Nombres"].ToString() ?? string.Empty,
                        Apellidos = reader["Apellidos"].ToString() ?? string.Empty,
                        Alias = reader["Alias"] == DBNull.Value ? null : reader["Alias"].ToString(),
                        EventoId = reader["EventoId"] == DBNull.Value ? null : Convert.ToInt32(reader["EventoId"]),
                        Titulo = reader["Titulo"].ToString() ?? string.Empty,
                        Contenido = reader["Contenido"].ToString() ?? string.Empty,
                        FechaCreacion = Convert.ToDateTime(reader["FechaCreacion"]),
                        Estado = reader["Estado"].ToString() ?? string.Empty,
                        UsuarioEsMiembro = Convert.ToBoolean(reader["UsuarioEsMiembro"]),
                        CantidadReplicas = Convert.ToInt32(reader["CantidadReplicas"]),
                        CantidadReportes = Convert.ToInt32(reader["CantidadReportes"])
                    }
                );

                return noticias;
            }
            catch (Exception ex)
            {
                throw new Exception("Error consultando las noticias de la comunidad.", ex);
            }
        }



        public async Task<ResultadoCrearNoticiaDbDto> CrearNoticiaAsync(CrearNoticiaRequestDto request)
        {
            try
            {
                List<SqlParameter> parameters = new List<SqlParameter>
        {
            new SqlParameter("@UsuarioId", request.UsuarioId),
            new SqlParameter("@ComunidadId", request.ComunidadId),
            new SqlParameter("@EventoId", request.EventoId == null ? DBNull.Value : request.EventoId),
            new SqlParameter("@Titulo", request.Titulo),
            new SqlParameter("@Contenido", request.Contenido)
        };

                List<ResultadoCrearNoticiaDbDto> resultado = await _executor.ExecuteQueryAsync(
                    "dbo.paCrearNoticia",
                    parameters,
                    reader => new ResultadoCrearNoticiaDbDto
                    {
                        Success = Convert.ToBoolean(reader["Success"]),
                        Message = reader["Message"].ToString() ?? string.Empty,
                        NoticiaId = reader["NoticiaId"] == DBNull.Value ? null : Convert.ToInt32(reader["NoticiaId"]),
                        ComunidadId = reader["ComunidadId"] == DBNull.Value ? null : Convert.ToInt32(reader["ComunidadId"]),
                        UsuarioId = reader["UsuarioId"] == DBNull.Value ? null : Convert.ToInt32(reader["UsuarioId"]),
                        Nombres = reader["Nombres"] == DBNull.Value ? null : reader["Nombres"].ToString(),
                        Apellidos = reader["Apellidos"] == DBNull.Value ? null : reader["Apellidos"].ToString(),
                        Alias = reader["Alias"] == DBNull.Value ? null : reader["Alias"].ToString(),
                        EventoId = reader["EventoId"] == DBNull.Value ? null : Convert.ToInt32(reader["EventoId"]),
                        Titulo = reader["Titulo"] == DBNull.Value ? null : reader["Titulo"].ToString(),
                        Contenido = reader["Contenido"] == DBNull.Value ? null : reader["Contenido"].ToString(),
                        FechaCreacion = reader["FechaCreacion"] == DBNull.Value ? null : Convert.ToDateTime(reader["FechaCreacion"]),
                        Estado = reader["Estado"] == DBNull.Value ? null : reader["Estado"].ToString(),
                        UsuarioEsMiembro = Convert.ToBoolean(reader["UsuarioEsMiembro"]),
                        CantidadReplicas = Convert.ToInt32(reader["CantidadReplicas"]),
                        CantidadReportes = Convert.ToInt32(reader["CantidadReportes"])
                    }
                );

                ResultadoCrearNoticiaDbDto? noticiaCreada = resultado.FirstOrDefault();

                if (noticiaCreada == null)
                {
                    return new ResultadoCrearNoticiaDbDto
                    {
                        Success = false,
                        Message = "No se obtuvo respuesta al crear la noticia."
                    };
                }

                return noticiaCreada;
            }
            catch (Exception ex)
            {
                return new ResultadoCrearNoticiaDbDto
                {
                    Success = false,
                    Message = $"Error creando noticia: {ex.Message}"
                };
            }
        }






    }
}