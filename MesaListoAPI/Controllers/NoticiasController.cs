using MesaListo.Application.DTOs.Noticias;
using MesaListo.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace MesaListoAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NoticiasController : ControllerBase
    {
        private readonly NoticiaService _noticiaService;

        public NoticiasController(NoticiaService noticiaService)
        {
            _noticiaService = noticiaService;
        }

        [HttpGet("getNoticiasPorComunidad/{usuarioId}/{comunidadId}")]
        public async Task<IActionResult> GetNoticiasPorComunidad(int usuarioId, int comunidadId)
        {
            try
            {
                List<NoticiaResumenDto> noticias = await _noticiaService.ListarNoticiasPorComunidadAsync(usuarioId, comunidadId);

                return Ok(new
                {
                    success = true,
                    message = "Noticias de la comunidad consultadas correctamente.",
                    data = noticias
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    success = false,
                    message = "Error consultando las noticias de la comunidad.",
                    error = ex.Message
                });
            }
        }


        [HttpPost("crearNoticia")]
        public async Task<IActionResult> CrearNoticia([FromBody] CrearNoticiaRequestDto request)
        {
            try
            {
                CrearNoticiaResponseDto response = await _noticiaService.CrearNoticiaAsync(request);

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
                    message = "Error inesperado al crear la noticia.",
                    error = ex.Message
                });
            }
        }






    }
}