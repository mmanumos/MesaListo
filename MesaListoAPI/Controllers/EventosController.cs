using MesaListo.Application.DTOs.Eventos;
using MesaListo.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace MesaListoAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventosController : ControllerBase
    {
        private readonly EventoService _eventoService;

        public EventosController(EventoService eventoService)
        {
            _eventoService = eventoService;
        }

        [HttpPost("crearEvento")]
        public async Task<IActionResult> CrearEvento([FromBody] CrearEventoRequestDto request)
        {
            try
            {
                CrearEventoResponseDto response = await _eventoService.CrearEventoAsync(request);

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
                    message = "Error inesperado al crear el evento.",
                    error = ex.Message
                });
            }
        }


        [HttpGet("getEventosProximos/{usuarioId}")]
        public async Task<IActionResult> GetEventosProximos(int usuarioId)
        {
            try
            {
                List<EventoResumenDto> eventos = await _eventoService.ListarEventosProximosAsync(usuarioId);

                return Ok(new
                {
                    success = true,
                    message = "Eventos próximos consultados correctamente.",
                    data = eventos
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    success = false,
                    message = "Error consultando los eventos próximos.",
                    error = ex.Message
                });
            }
        }



        [HttpPost("asistirEvento")]
        public async Task<IActionResult> AsistirEvento([FromBody] AsistirEventoRequestDto request)
        {
            try
            {
                EventoAccionResponseDto response = await _eventoService.AsistirEventoAsync(request);

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
                    message = "Error inesperado al confirmar asistencia al evento.",
                    error = ex.Message
                });
            }
        }


        [HttpPost("cancelarAsistenciaEvento")]
        public async Task<IActionResult> CancelarAsistenciaEvento([FromBody] CancelarAsistenciaEventoRequestDto request)
        {
            try
            {
                EventoAccionResponseDto response = await _eventoService.CancelarAsistenciaEventoAsync(request);

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
                    message = "Error inesperado al cancelar la asistencia al evento.",
                    error = ex.Message
                });
            }
        }



        [HttpGet("getDetalleEvento/{usuarioId}/{eventoId}")]
        public async Task<IActionResult> GetDetalleEvento(int usuarioId, int eventoId)
        {
            try
            {
                EventoResumenDto? evento = await _eventoService.ObtenerDetalleEventoAsync(usuarioId, eventoId);

                if (evento == null)
                {
                    return NotFound(new
                    {
                        success = false,
                        message = "El evento no existe, no está activo o ya no está vigente.",
                        data = (object?)null
                    });
                }

                return Ok(new
                {
                    success = true,
                    message = "Detalle del evento consultado correctamente.",
                    data = evento
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    success = false,
                    message = "Error consultando el detalle del evento.",
                    error = ex.Message
                });
            }
        }



        [HttpGet("getMisEventosAgendados/{usuarioId}")]
        public async Task<IActionResult> GetMisEventosAgendados(int usuarioId)
        {
            try
            {
                List<EventoResumenDto> eventos = await _eventoService.ListarMisEventosAgendadosAsync(usuarioId);

                return Ok(new
                {
                    success = true,
                    message = "Eventos agendados del usuario consultados correctamente.",
                    data = eventos
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    success = false,
                    message = "Error consultando los eventos agendados del usuario.",
                    error = ex.Message
                });
            }
        }



    }
}