using MesaListo.Application.DTOs.Auth;
using MesaListo.Application.Interfaces;
using MesaListo.Infrastructure.Persistence;
using Microsoft.Data.SqlClient;

namespace MesaListo.Infrastructure.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly SqlStoredProcedureExecutor _executor;

        public UsuarioRepository(SqlStoredProcedureExecutor executor)
        {
            _executor = executor;
        }

        public async Task<ResultadoCrearUsuarioDbDto> CrearUsuarioAsync(CrearCuentaRequestDto request, string passwordHash)
        {
            try
            {
                List<SqlParameter> parameters = new List<SqlParameter>
                {
                    new SqlParameter("@Nombres", request.Nombres),
                    new SqlParameter("@Apellidos", request.Apellidos),
                    new SqlParameter("@Correo", request.Correo),
                    new SqlParameter("@PasswordHash", passwordHash),
                    new SqlParameter("@Alias", string.IsNullOrWhiteSpace(request.Alias) ? DBNull.Value : request.Alias)
                };

                List<ResultadoCrearUsuarioDbDto> resultado = await _executor.ExecuteQueryAsync(
                    "dbo.paCrearUsuario",
                    parameters,
                    reader => new ResultadoCrearUsuarioDbDto
                    {
                        Success = Convert.ToBoolean(reader["Success"]),
                        Message = reader["Message"].ToString() ?? string.Empty,
                        UsuarioId = reader["UsuarioId"] == DBNull.Value ? null : Convert.ToInt32(reader["UsuarioId"]),
                        Nombres = reader["Nombres"] == DBNull.Value ? null : reader["Nombres"].ToString(),
                        Apellidos = reader["Apellidos"] == DBNull.Value ? null : reader["Apellidos"].ToString(),
                        Correo = reader["Correo"] == DBNull.Value ? null : reader["Correo"].ToString(),
                        Alias = reader["Alias"] == DBNull.Value ? null : reader["Alias"].ToString()
                    });

                ResultadoCrearUsuarioDbDto? usuarioCreado = resultado.FirstOrDefault();

                if (usuarioCreado == null)
                {
                    return new ResultadoCrearUsuarioDbDto
                    {
                        Success = false,
                        Message = "No se obtuvo respuesta al crear el usuario."
                    };
                }

                return usuarioCreado;
            }
            catch (Exception ex)
            {
                return new ResultadoCrearUsuarioDbDto
                {
                    Success = false,
                    Message = $"Error creando usuario: {ex.Message}"
                };
            }
        }

        public async Task<UsuarioLoginDbDto?> ObtenerUsuarioPorCorreoAsync(string correo)
        {
            try
            {
                List<SqlParameter> parameters = new List<SqlParameter>
                {
                    new SqlParameter("@Correo", correo)
                };

                List<UsuarioLoginDbDto> usuarios = await _executor.ExecuteQueryAsync(
                    "dbo.paObtenerUsuarioPorCorreo",
                    parameters,
                    reader => new UsuarioLoginDbDto
                    {
                        UsuarioId = Convert.ToInt32(reader["UsuarioId"]),
                        Nombres = reader["Nombres"].ToString() ?? string.Empty,
                        Apellidos = reader["Apellidos"].ToString() ?? string.Empty,
                        Correo = reader["Correo"].ToString() ?? string.Empty,
                        PasswordHash = reader["PasswordHash"].ToString() ?? string.Empty,
                        Alias = reader["Alias"] == DBNull.Value ? null : reader["Alias"].ToString(),
                        FechaRegistro = Convert.ToDateTime(reader["FechaRegistro"]),
                        Estado = reader["Estado"].ToString() ?? string.Empty
                    });

                return usuarios.FirstOrDefault();
            }
            catch
            {
                return null;
            }
        }
    }
}