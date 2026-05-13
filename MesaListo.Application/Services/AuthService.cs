using MesaListo.Application.DTOs.Auth;
using MesaListo.Application.Interfaces;

namespace MesaListo.Application.Services
{
    public class AuthService
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IPasswordService _passwordService;

        public AuthService(IUsuarioRepository usuarioRepository, IPasswordService passwordService)
        {
            _usuarioRepository = usuarioRepository;
            _passwordService = passwordService;
        }

        public async Task<AuthResponseDto> CrearCuentaAsync(CrearCuentaRequestDto request)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(request.Nombres) ||
                    string.IsNullOrWhiteSpace(request.Apellidos) ||
                    string.IsNullOrWhiteSpace(request.Correo) ||
                    string.IsNullOrWhiteSpace(request.Contrasena))
                {
                    return new AuthResponseDto
                    {
                        Success = false,
                        Message = "Nombres, apellidos, correo y contraseña son obligatorios."
                    };
                }

                UsuarioLoginDbDto? usuarioExistente = await _usuarioRepository.ObtenerUsuarioPorCorreoAsync(request.Correo.Trim());

                if (usuarioExistente != null)
                {
                    return new AuthResponseDto
                    {
                        Success = false,
                        Message = "El correo ya se encuentra registrado."
                    };
                }

                string passwordHash = _passwordService.GenerarHash(request.Contrasena);

                CrearCuentaRequestDto requestNormalizado = new CrearCuentaRequestDto
                {
                    Nombres = request.Nombres.Trim(),
                    Apellidos = request.Apellidos.Trim(),
                    Correo = request.Correo.Trim(),
                    Contrasena = request.Contrasena,
                    Alias = string.IsNullOrWhiteSpace(request.Alias) ? null : request.Alias.Trim()
                };

                ResultadoCrearUsuarioDbDto resultado = await _usuarioRepository.CrearUsuarioAsync(requestNormalizado, passwordHash);

                if (!resultado.Success || resultado.UsuarioId == null)
                {
                    return new AuthResponseDto
                    {
                        Success = false,
                        Message = resultado.Message
                    };
                }

                UsuarioSesionDto usuarioSesion = new UsuarioSesionDto
                {
                    UsuarioId = resultado.UsuarioId.Value,
                    Nombres = resultado.Nombres ?? string.Empty,
                    Apellidos = resultado.Apellidos ?? string.Empty,
                    Correo = resultado.Correo ?? string.Empty,
                    Alias = resultado.Alias
                };

                return new AuthResponseDto
                {
                    Success = true,
                    Message = "Usuario creado correctamente.",
                    Usuario = usuarioSesion
                };
            }
            catch (Exception ex)
            {
                return new AuthResponseDto
                {
                    Success = false,
                    Message = $"Ocurrió un error al crear la cuenta: {ex.Message}"
                };
            }
        }

        public async Task<AuthResponseDto> IniciarSesionAsync(IniciarSesionRequestDto request)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(request.Correo) ||
                    string.IsNullOrWhiteSpace(request.Contrasena))
                {
                    return new AuthResponseDto
                    {
                        Success = false,
                        Message = "Correo y contraseña son obligatorios."
                    };
                }

                UsuarioLoginDbDto? usuario = await _usuarioRepository.ObtenerUsuarioPorCorreoAsync(request.Correo.Trim());

                if (usuario == null)
                {
                    return new AuthResponseDto
                    {
                        Success = false,
                        Message = "Correo o contraseña incorrectos."
                    };
                }

                bool contrasenaValida = _passwordService.ValidarContrasena(request.Contrasena, usuario.PasswordHash);

                if (!contrasenaValida)
                {
                    return new AuthResponseDto
                    {
                        Success = false,
                        Message = "Correo o contraseña incorrectos."
                    };
                }

                UsuarioSesionDto usuarioSesion = new UsuarioSesionDto
                {
                    UsuarioId = usuario.UsuarioId,
                    Nombres = usuario.Nombres,
                    Apellidos = usuario.Apellidos,
                    Correo = usuario.Correo,
                    Alias = usuario.Alias
                };

                return new AuthResponseDto
                {
                    Success = true,
                    Message = "Inicio de sesión exitoso.",
                    Usuario = usuarioSesion
                };
            }
            catch (Exception ex)
            {
                return new AuthResponseDto
                {
                    Success = false,
                    Message = $"Ocurrió un error al iniciar sesión: {ex.Message}"
                };
            }
        }
    }
}