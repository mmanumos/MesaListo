using MesaListo.Application.DTOs.Auth;
using MesaListo.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace MesaListoAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _authService;

        public AuthController(AuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("crearCuenta")]
        public async Task<IActionResult> CrearCuenta([FromBody] CrearCuentaRequestDto request)
        {
            try
            {
                AuthResponseDto response = await _authService.CrearCuentaAsync(request);

                if (!response.Success)
                {
                    return BadRequest(response);
                }

                return Ok(response);
            }
            catch (Exception ex)
            {
                AuthResponseDto response = new AuthResponseDto
                {
                    Success = false,
                    Message = $"Error inesperado al crear la cuenta: {ex.Message}"
                };

                return StatusCode(500, response);
            }
        }

        [HttpPost("iniciarSesion")]
        public async Task<IActionResult> IniciarSesion([FromBody] IniciarSesionRequestDto request)
        {
            try
            {
                AuthResponseDto response = await _authService.IniciarSesionAsync(request);

                if (!response.Success)
                {
                    return Unauthorized(response);
                }

                return Ok(response);
            }
            catch (Exception ex)
            {
                AuthResponseDto response = new AuthResponseDto
                {
                    Success = false,
                    Message = $"Error inesperado al iniciar sesión: {ex.Message}"
                };

                return StatusCode(500, response);
            }
        }
    }
}