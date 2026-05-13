using MesaListo.Application.DTOs.Comunidades;
using MesaListo.Application.Interfaces;
using MesaListo.Infrastructure.Persistence;
using Microsoft.Data.SqlClient;

namespace MesaListo.Infrastructure.Repositories
{
    public class ComunidadRepository : IComunidadRepository
    {
        private readonly SqlStoredProcedureExecutor _executor;

        public ComunidadRepository(SqlStoredProcedureExecutor executor)
        {
            _executor = executor;
        }

        public async Task<List<ComunidadResumenDto>> ListarMisComunidadesAsync(int usuarioId)
        {
            try
            {
                List<SqlParameter> parameters = new List<SqlParameter>
                {
                    new SqlParameter("@UsuarioId", usuarioId)
                };

                List<ComunidadResumenDto> comunidades = await _executor.ExecuteQueryAsync(
                    "dbo.paListarMisComunidades",
                    parameters,
                    MapearComunidadResumen
                );

                return comunidades;
            }
            catch (Exception ex)
            {
                throw new Exception("Error consultando las comunidades del usuario.", ex);
            }
        }

        public async Task<List<ComunidadResumenDto>> ListarComunidadesPopularesParaExplorarAsync(int usuarioId)
        {
            try
            {
                List<SqlParameter> parameters = new List<SqlParameter>
                {
                    new SqlParameter("@UsuarioId", usuarioId)
                };

                List<ComunidadResumenDto> comunidades = await _executor.ExecuteQueryAsync(
                    "dbo.paListarComunidadesPopularesParaExplorar",
                    parameters,
                    MapearComunidadResumen
                );

                return comunidades;
            }
            catch (Exception ex)
            {
                throw new Exception("Error consultando comunidades populares para explorar.", ex);
            }
        }

        public async Task<List<ComunidadResumenDto>> BuscarComunidadesPorNombreAsync(int usuarioId, string nombre)
        {
            try
            {
                List<SqlParameter> parameters = new List<SqlParameter>
                {
                    new SqlParameter("@UsuarioId", usuarioId),
                    new SqlParameter("@Nombre", nombre)
                };

                List<ComunidadResumenDto> comunidades = await _executor.ExecuteQueryAsync(
                    "dbo.paBuscarComunidadesPorNombre",
                    parameters,
                    MapearComunidadResumen
                );

                return comunidades;
            }
            catch (Exception ex)
            {
                throw new Exception("Error buscando comunidades por nombre.", ex);
            }
        }

        private ComunidadResumenDto MapearComunidadResumen(SqlDataReader reader)
        {
            ComunidadResumenDto comunidad = new ComunidadResumenDto
            {
                ComunidadId = Convert.ToInt32(reader["ComunidadId"]),
                Nombre = reader["Nombre"].ToString() ?? string.Empty,
                Descripcion = reader["Descripcion"] == DBNull.Value ? null : reader["Descripcion"].ToString(),
                LineamientosConvivencia = reader["LineamientosConvivencia"] == DBNull.Value ? null : reader["LineamientosConvivencia"].ToString(),
                PropietarioUsuarioId = Convert.ToInt32(reader["PropietarioUsuarioId"]),
                FechaCreacion = Convert.ToDateTime(reader["FechaCreacion"]),
                Estado = reader["Estado"].ToString() ?? string.Empty,
                EsPropietario = Convert.ToBoolean(reader["EsPropietario"]),
                CantidadMiembros = Convert.ToInt32(reader["CantidadMiembros"])
            };

            return comunidad;
        }

        public async Task<ResultadoCrearComunidadDbDto> CrearComunidadAsync(CrearComunidadRequestDto request)
        {
            try
            {
                List<SqlParameter> parameters = new List<SqlParameter>
        {
            new SqlParameter("@UsuarioId", request.UsuarioId),
            new SqlParameter("@Nombre", request.Nombre),
            new SqlParameter("@Descripcion", request.Descripcion),
            new SqlParameter("@LineamientosConvivencia", request.LineamientosConvivencia)
        };

                List<ResultadoCrearComunidadDbDto> resultado = await _executor.ExecuteQueryAsync(
                    "dbo.paCrearComunidad",
                    parameters,
                    reader => new ResultadoCrearComunidadDbDto
                    {
                        Success = Convert.ToBoolean(reader["Success"]),
                        Message = reader["Message"].ToString() ?? string.Empty,
                        ComunidadId = reader["ComunidadId"] == DBNull.Value ? null : Convert.ToInt32(reader["ComunidadId"]),
                        Nombre = reader["Nombre"] == DBNull.Value ? null : reader["Nombre"].ToString(),
                        Descripcion = reader["Descripcion"] == DBNull.Value ? null : reader["Descripcion"].ToString(),
                        LineamientosConvivencia = reader["LineamientosConvivencia"] == DBNull.Value ? null : reader["LineamientosConvivencia"].ToString(),
                        PropietarioUsuarioId = reader["PropietarioUsuarioId"] == DBNull.Value ? null : Convert.ToInt32(reader["PropietarioUsuarioId"]),
                        FechaCreacion = reader["FechaCreacion"] == DBNull.Value ? null : Convert.ToDateTime(reader["FechaCreacion"]),
                        Estado = reader["Estado"] == DBNull.Value ? null : reader["Estado"].ToString()
                    }
                );

                ResultadoCrearComunidadDbDto? comunidadCreada = resultado.FirstOrDefault();

                if (comunidadCreada == null)
                {
                    return new ResultadoCrearComunidadDbDto
                    {
                        Success = false,
                        Message = "No se obtuvo respuesta al crear la comunidad."
                    };
                }

                return comunidadCreada;
            }
            catch (Exception ex)
            {
                return new ResultadoCrearComunidadDbDto
                {
                    Success = false,
                    Message = $"Error creando comunidad: {ex.Message}"
                };
            }
        }


        public async Task<ResultadoComunidadAccionDbDto> UnirseComunidadAsync(UnirseComunidadRequestDto request)
        {
            try
            {
                List<SqlParameter> parameters = new List<SqlParameter>
        {
            new SqlParameter("@UsuarioId", request.UsuarioId),
            new SqlParameter("@ComunidadId", request.ComunidadId)
        };

                List<ResultadoComunidadAccionDbDto> resultado = await _executor.ExecuteQueryAsync(
                    "dbo.paUnirseComunidad",
                    parameters,
                    reader => new ResultadoComunidadAccionDbDto
                    {
                        Success = Convert.ToBoolean(reader["Success"]),
                        Message = reader["Message"].ToString() ?? string.Empty,
                        ComunidadId = reader["ComunidadId"] == DBNull.Value ? null : Convert.ToInt32(reader["ComunidadId"]),
                        Nombre = reader["Nombre"] == DBNull.Value ? null : reader["Nombre"].ToString(),
                        Descripcion = reader["Descripcion"] == DBNull.Value ? null : reader["Descripcion"].ToString(),
                        LineamientosConvivencia = reader["LineamientosConvivencia"] == DBNull.Value ? null : reader["LineamientosConvivencia"].ToString(),
                        PropietarioUsuarioId = reader["PropietarioUsuarioId"] == DBNull.Value ? null : Convert.ToInt32(reader["PropietarioUsuarioId"]),
                        FechaCreacion = reader["FechaCreacion"] == DBNull.Value ? null : Convert.ToDateTime(reader["FechaCreacion"]),
                        Estado = reader["Estado"] == DBNull.Value ? null : reader["Estado"].ToString(),
                        EsPropietario = Convert.ToBoolean(reader["EsPropietario"]),
                        CantidadMiembros = Convert.ToInt32(reader["CantidadMiembros"])
                    }
                );

                ResultadoComunidadAccionDbDto? comunidad = resultado.FirstOrDefault();

                if (comunidad == null)
                {
                    return new ResultadoComunidadAccionDbDto
                    {
                        Success = false,
                        Message = "No se obtuvo respuesta al unirse a la comunidad."
                    };
                }

                return comunidad;
            }
            catch (Exception ex)
            {
                return new ResultadoComunidadAccionDbDto
                {
                    Success = false,
                    Message = $"Error al unirse a la comunidad: {ex.Message}"
                };
            }
        }


        public async Task<ResultadoComunidadAccionDbDto> SalirComunidadAsync(SalirComunidadRequestDto request)
        {
            try
            {
                List<SqlParameter> parameters = new List<SqlParameter>
        {
            new SqlParameter("@UsuarioId", request.UsuarioId),
            new SqlParameter("@ComunidadId", request.ComunidadId)
        };

                List<ResultadoComunidadAccionDbDto> resultado = await _executor.ExecuteQueryAsync(
                    "dbo.paSalirComunidad",
                    parameters,
                    reader => new ResultadoComunidadAccionDbDto
                    {
                        Success = Convert.ToBoolean(reader["Success"]),
                        Message = reader["Message"].ToString() ?? string.Empty,
                        ComunidadId = reader["ComunidadId"] == DBNull.Value ? null : Convert.ToInt32(reader["ComunidadId"]),
                        Nombre = reader["Nombre"] == DBNull.Value ? null : reader["Nombre"].ToString(),
                        Descripcion = reader["Descripcion"] == DBNull.Value ? null : reader["Descripcion"].ToString(),
                        LineamientosConvivencia = reader["LineamientosConvivencia"] == DBNull.Value ? null : reader["LineamientosConvivencia"].ToString(),
                        PropietarioUsuarioId = reader["PropietarioUsuarioId"] == DBNull.Value ? null : Convert.ToInt32(reader["PropietarioUsuarioId"]),
                        FechaCreacion = reader["FechaCreacion"] == DBNull.Value ? null : Convert.ToDateTime(reader["FechaCreacion"]),
                        Estado = reader["Estado"] == DBNull.Value ? null : reader["Estado"].ToString(),
                        EsPropietario = Convert.ToBoolean(reader["EsPropietario"]),
                        CantidadMiembros = Convert.ToInt32(reader["CantidadMiembros"])
                    }
                );

                ResultadoComunidadAccionDbDto? comunidad = resultado.FirstOrDefault();

                if (comunidad == null)
                {
                    return new ResultadoComunidadAccionDbDto
                    {
                        Success = false,
                        Message = "No se obtuvo respuesta al salir de la comunidad."
                    };
                }

                return comunidad;
            }
            catch (Exception ex)
            {
                return new ResultadoComunidadAccionDbDto
                {
                    Success = false,
                    Message = $"Error al salir de la comunidad: {ex.Message}"
                };
            }
        }





    }
}