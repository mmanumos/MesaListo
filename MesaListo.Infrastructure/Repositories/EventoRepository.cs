using MesaListo.Application.DTOs.Eventos;
using MesaListo.Application.Interfaces;
using MesaListo.Infrastructure.Persistence;
using Microsoft.Data.SqlClient;

namespace MesaListo.Infrastructure.Repositories
{
    public class EventoRepository : IEventoRepository
    {
        private readonly SqlStoredProcedureExecutor _executor;

        public EventoRepository(SqlStoredProcedureExecutor executor)
        {
            _executor = executor;
        }

        public async Task<ResultadoCrearEventoDbDto> CrearEventoAsync(CrearEventoRequestDto request)
        {
            try
            {
                string juegosIds = string.Join(",", request.JuegosIds);
                string comunidadesIds = string.Join(",", request.ComunidadesIds);

                List<SqlParameter> parameters = new List<SqlParameter>
                {
                    new SqlParameter("@UsuarioId", request.UsuarioId),
                    new SqlParameter("@Titulo", request.Titulo),
                    new SqlParameter("@Descripcion", request.Descripcion),
                    new SqlParameter("@FechaHoraInicio", request.FechaHoraInicio),
                    new SqlParameter("@Lugar", request.Lugar),
                    new SqlParameter("@AforoMaximo", request.AforoMaximo),
                    new SqlParameter("@LineamientosConvivencia", request.LineamientosConvivencia),
                    new SqlParameter("@JuegosIds", juegosIds),
                    new SqlParameter("@ComunidadesIds", comunidadesIds)
                };

                List<ResultadoCrearEventoDbDto> resultado = await _executor.ExecuteQueryAsync(
                    "dbo.paCrearEvento",
                    parameters,
                    reader => new ResultadoCrearEventoDbDto
                    {
                        Success = Convert.ToBoolean(reader["Success"]),
                        Message = reader["Message"].ToString() ?? string.Empty,
                        EventoId = reader["EventoId"] == DBNull.Value ? null : Convert.ToInt32(reader["EventoId"])
                    }
                );

                ResultadoCrearEventoDbDto? eventoCreado = resultado.FirstOrDefault();

                if (eventoCreado == null)
                {
                    return new ResultadoCrearEventoDbDto
                    {
                        Success = false,
                        Message = "No se obtuvo respuesta al crear el evento."
                    };
                }

                return eventoCreado;
            }
            catch (Exception ex)
            {
                return new ResultadoCrearEventoDbDto
                {
                    Success = false,
                    Message = $"Error creando evento: {ex.Message}"
                };
            }
        }


        public async Task<List<EventoResumenDto>> ListarEventosProximosAsync(int usuarioId)
        {
            try
            {
                List<SqlParameter> parameters = new List<SqlParameter>
        {
            new SqlParameter("@UsuarioId", usuarioId)
        };

                List<EventoResumenDto> eventos = await _executor.ExecuteQueryAsync(
                    "dbo.paListarEventosProximos",
                    parameters,
                    reader => new EventoResumenDto
                    {
                        EventoId = Convert.ToInt32(reader["EventoId"]),
                        AnfitrionUsuarioId = Convert.ToInt32(reader["AnfitrionUsuarioId"]),
                        NombresAnfitrion = reader["NombresAnfitrion"].ToString() ?? string.Empty,
                        ApellidosAnfitrion = reader["ApellidosAnfitrion"].ToString() ?? string.Empty,
                        AliasAnfitrion = reader["AliasAnfitrion"] == DBNull.Value ? null : reader["AliasAnfitrion"].ToString(),
                        Titulo = reader["Titulo"].ToString() ?? string.Empty,
                        Descripcion = reader["Descripcion"].ToString() ?? string.Empty,
                        FechaHoraInicio = Convert.ToDateTime(reader["FechaHoraInicio"]),
                        Lugar = reader["Lugar"].ToString() ?? string.Empty,
                        AforoMaximo = Convert.ToInt32(reader["AforoMaximo"]),
                        LineamientosConvivencia = reader["LineamientosConvivencia"].ToString() ?? string.Empty,
                        FechaCreacion = Convert.ToDateTime(reader["FechaCreacion"]),
                        Estado = reader["Estado"].ToString() ?? string.Empty,
                        CantidadAsistentes = Convert.ToInt32(reader["CantidadAsistentes"]),
                        CuposDisponibles = Convert.ToInt32(reader["CuposDisponibles"]),
                        EstaAgendado = Convert.ToBoolean(reader["EstaAgendado"]),
                        Juegos = reader["Juegos"] == DBNull.Value ? string.Empty : reader["Juegos"].ToString() ?? string.Empty
                    }
                );

                return eventos;
            }
            catch (Exception ex)
            {
                throw new Exception("Error consultando los eventos próximos.", ex);
            }
        }




        public async Task<ResultadoEventoAccionDbDto> AsistirEventoAsync(AsistirEventoRequestDto request)
        {
            try
            {
                List<SqlParameter> parameters = new List<SqlParameter>
        {
            new SqlParameter("@UsuarioId", request.UsuarioId),
            new SqlParameter("@EventoId", request.EventoId)
        };

                List<ResultadoEventoAccionDbDto> resultado = await _executor.ExecuteQueryAsync(
                    "dbo.paAsistirEvento",
                    parameters,
                    reader => new ResultadoEventoAccionDbDto
                    {
                        Success = Convert.ToBoolean(reader["Success"]),
                        Message = reader["Message"].ToString() ?? string.Empty,
                        EventoId = reader["EventoId"] == DBNull.Value ? null : Convert.ToInt32(reader["EventoId"])
                    }
                );

                ResultadoEventoAccionDbDto? accion = resultado.FirstOrDefault();

                if (accion == null)
                {
                    return new ResultadoEventoAccionDbDto
                    {
                        Success = false,
                        Message = "No se obtuvo respuesta al confirmar asistencia."
                    };
                }

                return accion;
            }
            catch (Exception ex)
            {
                return new ResultadoEventoAccionDbDto
                {
                    Success = false,
                    Message = $"Error confirmando asistencia al evento: {ex.Message}"
                };
            }
        }


        public async Task<ResultadoEventoAccionDbDto> CancelarAsistenciaEventoAsync(CancelarAsistenciaEventoRequestDto request)
        {
            try
            {
                List<SqlParameter> parameters = new List<SqlParameter>
        {
            new SqlParameter("@UsuarioId", request.UsuarioId),
            new SqlParameter("@EventoId", request.EventoId)
        };

                List<ResultadoEventoAccionDbDto> resultado = await _executor.ExecuteQueryAsync(
                    "dbo.paCancelarAsistenciaEvento",
                    parameters,
                    reader => new ResultadoEventoAccionDbDto
                    {
                        Success = Convert.ToBoolean(reader["Success"]),
                        Message = reader["Message"].ToString() ?? string.Empty,
                        EventoId = reader["EventoId"] == DBNull.Value ? null : Convert.ToInt32(reader["EventoId"])
                    }
                );

                ResultadoEventoAccionDbDto? accion = resultado.FirstOrDefault();

                if (accion == null)
                {
                    return new ResultadoEventoAccionDbDto
                    {
                        Success = false,
                        Message = "No se obtuvo respuesta al cancelar la asistencia."
                    };
                }

                return accion;
            }
            catch (Exception ex)
            {
                return new ResultadoEventoAccionDbDto
                {
                    Success = false,
                    Message = $"Error cancelando asistencia al evento: {ex.Message}"
                };
            }
        }


        public async Task<EventoResumenDto?> ObtenerDetalleEventoAsync(int usuarioId, int eventoId)
        {
            try
            {
                List<SqlParameter> parameters = new List<SqlParameter>
        {
            new SqlParameter("@UsuarioId", usuarioId),
            new SqlParameter("@EventoId", eventoId)
        };

                List<EventoResumenDto> eventos = await _executor.ExecuteQueryAsync(
                    "dbo.paObtenerDetalleEvento",
                    parameters,
                    reader => new EventoResumenDto
                    {
                        EventoId = Convert.ToInt32(reader["EventoId"]),
                        AnfitrionUsuarioId = Convert.ToInt32(reader["AnfitrionUsuarioId"]),
                        NombresAnfitrion = reader["NombresAnfitrion"].ToString() ?? string.Empty,
                        ApellidosAnfitrion = reader["ApellidosAnfitrion"].ToString() ?? string.Empty,
                        AliasAnfitrion = reader["AliasAnfitrion"] == DBNull.Value ? null : reader["AliasAnfitrion"].ToString(),
                        Titulo = reader["Titulo"].ToString() ?? string.Empty,
                        Descripcion = reader["Descripcion"].ToString() ?? string.Empty,
                        FechaHoraInicio = Convert.ToDateTime(reader["FechaHoraInicio"]),
                        Lugar = reader["Lugar"].ToString() ?? string.Empty,
                        AforoMaximo = Convert.ToInt32(reader["AforoMaximo"]),
                        LineamientosConvivencia = reader["LineamientosConvivencia"].ToString() ?? string.Empty,
                        FechaCreacion = Convert.ToDateTime(reader["FechaCreacion"]),
                        Estado = reader["Estado"].ToString() ?? string.Empty,
                        CantidadAsistentes = Convert.ToInt32(reader["CantidadAsistentes"]),
                        CuposDisponibles = Convert.ToInt32(reader["CuposDisponibles"]),
                        EstaAgendado = Convert.ToBoolean(reader["EstaAgendado"]),
                        Juegos = reader["Juegos"] == DBNull.Value ? string.Empty : reader["Juegos"].ToString() ?? string.Empty
                    }
                );

                return eventos.FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw new Exception("Error obteniendo el detalle del evento.", ex);
            }
        }


        public async Task<List<EventoResumenDto>> ListarMisEventosAgendadosAsync(int usuarioId)
        {
            try
            {
                List<SqlParameter> parameters = new List<SqlParameter>
        {
            new SqlParameter("@UsuarioId", usuarioId)
        };

                List<EventoResumenDto> eventos = await _executor.ExecuteQueryAsync(
                    "dbo.paListarMisEventosAgendados",
                    parameters,
                    reader => new EventoResumenDto
                    {
                        EventoId = Convert.ToInt32(reader["EventoId"]),
                        AnfitrionUsuarioId = Convert.ToInt32(reader["AnfitrionUsuarioId"]),
                        NombresAnfitrion = reader["NombresAnfitrion"].ToString() ?? string.Empty,
                        ApellidosAnfitrion = reader["ApellidosAnfitrion"].ToString() ?? string.Empty,
                        AliasAnfitrion = reader["AliasAnfitrion"] == DBNull.Value ? null : reader["AliasAnfitrion"].ToString(),
                        Titulo = reader["Titulo"].ToString() ?? string.Empty,
                        Descripcion = reader["Descripcion"].ToString() ?? string.Empty,
                        FechaHoraInicio = Convert.ToDateTime(reader["FechaHoraInicio"]),
                        Lugar = reader["Lugar"].ToString() ?? string.Empty,
                        AforoMaximo = Convert.ToInt32(reader["AforoMaximo"]),
                        LineamientosConvivencia = reader["LineamientosConvivencia"].ToString() ?? string.Empty,
                        FechaCreacion = Convert.ToDateTime(reader["FechaCreacion"]),
                        Estado = reader["Estado"].ToString() ?? string.Empty,
                        CantidadAsistentes = Convert.ToInt32(reader["CantidadAsistentes"]),
                        CuposDisponibles = Convert.ToInt32(reader["CuposDisponibles"]),
                        EstaAgendado = Convert.ToBoolean(reader["EstaAgendado"]),
                        Juegos = reader["Juegos"] == DBNull.Value ? string.Empty : reader["Juegos"].ToString() ?? string.Empty
                    }
                );

                return eventos;
            }
            catch (Exception ex)
            {
                throw new Exception("Error consultando los eventos agendados del usuario.", ex);
            }
        }



    }
}