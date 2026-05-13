using MesaListo.Application.DTOs.Comunidades;
using MesaListo.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace MesaListoAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ComunidadesController : ControllerBase
    {
        private readonly ComunidadService _comunidadService;

        public ComunidadesController(ComunidadService comunidadService)
        {
            _comunidadService = comunidadService;
        }

        [HttpGet("getMisComunidades/{usuarioId}")]
        public async Task<IActionResult> GetMisComunidades(int usuarioId)
        {
            try
            {
                List<ComunidadResumenDto> comunidades = await _comunidadService.ListarMisComunidadesAsync(usuarioId);

                return Ok(new
                {
                    success = true,
                    message = "Comunidades del usuario consultadas correctamente.",
                    data = comunidades
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    success = false,
                    message = "Error consultando las comunidades del usuario.",
                    error = ex.Message
                });
            }
        }

        [HttpGet("getComunidadesPopularesParaExplorar/{usuarioId}")]
        public async Task<IActionResult> GetComunidadesPopularesParaExplorar(int usuarioId)
        {
            try
            {
                List<ComunidadResumenDto> comunidades = await _comunidadService.ListarComunidadesPopularesParaExplorarAsync(usuarioId);

                return Ok(new
                {
                    success = true,
                    message = "Comunidades populares consultadas correctamente.",
                    data = comunidades
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    success = false,
                    message = "Error consultando comunidades populares para explorar.",
                    error = ex.Message
                });
            }
        }

        [HttpGet("buscarComunidadesPorNombre/{usuarioId}")]
        public async Task<IActionResult> BuscarComunidadesPorNombre(int usuarioId, [FromQuery] string nombre)
        {
            try
            {
                List<ComunidadResumenDto> comunidades = await _comunidadService.BuscarComunidadesPorNombreAsync(usuarioId, nombre);

                return Ok(new
                {
                    success = true,
                    message = "Búsqueda de comunidades realizada correctamente.",
                    data = comunidades
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    success = false,
                    message = "Error buscando comunidades por nombre.",
                    error = ex.Message
                });
            }
        }


        [HttpPost("crearComunidad")]
        public async Task<IActionResult> CrearComunidad([FromBody] CrearComunidadRequestDto request)
        {
            try
            {
                CrearComunidadResponseDto response = await _comunidadService.CrearComunidadAsync(request);

                if (!response.Success)
                {
                    return BadRequest(response);
                }

                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    success = false,
                    message = "Error inesperado al crear la comunidad.",
                    error = ex.Message
                });
            }
        }


        [HttpPost("unirseComunidad")]
        public async Task<IActionResult> UnirseComunidad([FromBody] UnirseComunidadRequestDto request)
        {
            try
            {
                ComunidadAccionResponseDto response = await _comunidadService.UnirseComunidadAsync(request);

                if (!response.Success)
                {
                    return BadRequest(response);
                }

                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    success = false,
                    message = "Error inesperado al unirse a la comunidad.",
                    error = ex.Message
                });
            }
        }



        [HttpPost("salirComunidad")]
        public async Task<IActionResult> SalirComunidad([FromBody] SalirComunidadRequestDto request)
        {
            try
            {
                ComunidadAccionResponseDto response = await _comunidadService.SalirComunidadAsync(request);

                if (!response.Success)
                {
                    return BadRequest(response);
                }

                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    success = false,
                    message = "Error inesperado al salir de la comunidad.",
                    error = ex.Message
                });
            }
        }



    }
}