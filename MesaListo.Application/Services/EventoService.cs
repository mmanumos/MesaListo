using MesaListo.Application.DTOs.Eventos;
using MesaListo.Application.Interfaces;

namespace MesaListo.Application.Services
{
    public class EventoService
    {
        private readonly IEventoRepository _eventoRepository;

        public EventoService(IEventoRepository eventoRepository)
        {
            _eventoRepository = eventoRepository;
        }

        public async Task<CrearEventoResponseDto> CrearEventoAsync(CrearEventoRequestDto request)
        {
            try
            {
                if (request.UsuarioId <= 0)
                {
                    return new CrearEventoResponseDto
                    {
                        Success = false,
                        Message = "El usuario es obligatorio para crear el evento."
                    };
                }

                if (string.IsNullOrWhiteSpace(request.Titulo))
                {
                    return new CrearEventoResponseDto
                    {
                        Success = false,
                        Message = "El título del evento es obligatorio."
                    };
                }

                if (string.IsNullOrWhiteSpace(request.Descripcion))
                {
                    return new CrearEventoResponseDto
                    {
                        Success = false,
                        Message = "La descripción del evento es obligatoria."
                    };
                }

                if (request.FechaHoraInicio <= DateTime.Now)
                {
                    return new CrearEventoResponseDto
                    {
                        Success = false,
                        Message = "La fecha y hora del evento debe ser futura."
                    };
                }

                if (string.IsNullOrWhiteSpace(request.Lugar))
                {
                    return new CrearEventoResponseDto
                    {
                        Success = false,
                        Message = "El lugar del evento es obligatorio."
                    };
                }

                if (request.AforoMaximo <= 0)
                {
                    return new CrearEventoResponseDto
                    {
                        Success = false,
                        Message = "El aforo máximo debe ser mayor que cero."
                    };
                }

                if (string.IsNullOrWhiteSpace(request.LineamientosConvivencia))
                {
                    return new CrearEventoResponseDto
                    {
                        Success = false,
                        Message = "Los lineamientos de convivencia del evento son obligatorios."
                    };
                }

                if (request.JuegosIds == null || request.JuegosIds.Count == 0)
                {
                    return new CrearEventoResponseDto
                    {
                        Success = false,
                        Message = "Debe seleccionar al menos un juego para crear el evento."
                    };
                }

                if (request.ComunidadesIds == null || request.ComunidadesIds.Count == 0)
                {
                    return new CrearEventoResponseDto
                    {
                        Success = false,
                        Message = "Debe seleccionar al menos una comunidad para notificar el evento."
                    };
                }

                CrearEventoRequestDto requestNormalizado = new CrearEventoRequestDto
                {
                    UsuarioId = request.UsuarioId,
                    Titulo = request.Titulo.Trim(),
                    Descripcion = request.Descripcion.Trim(),
                    FechaHoraInicio = request.FechaHoraInicio,
                    Lugar = request.Lugar.Trim(),
                    AforoMaximo = request.AforoMaximo,
                    LineamientosConvivencia = request.LineamientosConvivencia.Trim(),
                    JuegosIds = request.JuegosIds.Distinct().ToList(),
                    ComunidadesIds = request.ComunidadesIds.Distinct().ToList()
                };

                ResultadoCrearEventoDbDto resultado = await _eventoRepository.CrearEventoAsync(requestNormalizado);

                return new CrearEventoResponseDto
                {
                    Success = resultado.Success,
                    Message = resultado.Message,
                    EventoId = resultado.EventoId
                };
            }
            catch (Exception ex)
            {
                return new CrearEventoResponseDto
                {
                    Success = false,
                    Message = $"Error en el servicio al crear el evento: {ex.Message}"
                };
            }
        }



        public async Task<List<EventoResumenDto>> ListarEventosProximosAsync(int usuarioId)
        {
            try
            {
                if (usuarioId <= 0)
                {
                    return new List<EventoResumenDto>();
                }

                List<EventoResumenDto> eventos = await _eventoRepository.ListarEventosProximosAsync(usuarioId);

                return eventos;
            }
            catch (Exception ex)
            {
                throw new Exception("Error en el servicio al consultar los eventos próximos.", ex);
            }
        }



        public async Task<EventoAccionResponseDto> AsistirEventoAsync(AsistirEventoRequestDto request)
        {
            try
            {
                if (request.UsuarioId <= 0)
                {
                    return new EventoAccionResponseDto
                    {
                        Success = false,
                        Message = "El usuario es obligatorio para confirmar asistencia."
                    };
                }

                if (request.EventoId <= 0)
                {
                    return new EventoAccionResponseDto
                    {
                        Success = false,
                        Message = "El evento es obligatorio para confirmar asistencia."
                    };
                }

                ResultadoEventoAccionDbDto resultado = await _eventoRepository.AsistirEventoAsync(request);

                return new EventoAccionResponseDto
                {
                    Success = resultado.Success,
                    Message = resultado.Message,
                    EventoId = resultado.EventoId
                };
            }
            catch (Exception ex)
            {
                return new EventoAccionResponseDto
                {
                    Success = false,
                    Message = $"Error en el servicio al confirmar asistencia al evento: {ex.Message}"
                };
            }
        }



        public async Task<EventoAccionResponseDto> CancelarAsistenciaEventoAsync(CancelarAsistenciaEventoRequestDto request)
        {
            try
            {
                if (request.UsuarioId <= 0)
                {
                    return new EventoAccionResponseDto
                    {
                        Success = false,
                        Message = "El usuario es obligatorio para cancelar la asistencia."
                    };
                }

                if (request.EventoId <= 0)
                {
                    return new EventoAccionResponseDto
                    {
                        Success = false,
                        Message = "El evento es obligatorio para cancelar la asistencia."
                    };
                }

                ResultadoEventoAccionDbDto resultado = await _eventoRepository.CancelarAsistenciaEventoAsync(request);

                return new EventoAccionResponseDto
                {
                    Success = resultado.Success,
                    Message = resultado.Message,
                    EventoId = resultado.EventoId
                };
            }
            catch (Exception ex)
            {
                return new EventoAccionResponseDto
                {
                    Success = false,
                    Message = $"Error en el servicio al cancelar la asistencia al evento: {ex.Message}"
                };
            }
        }



        public async Task<EventoResumenDto?> ObtenerDetalleEventoAsync(int usuarioId, int eventoId)
        {
            try
            {
                if (usuarioId <= 0)
                {
                    return null;
                }

                if (eventoId <= 0)
                {
                    return null;
                }

                EventoResumenDto? evento = await _eventoRepository.ObtenerDetalleEventoAsync(usuarioId, eventoId);

                return evento;
            }
            catch (Exception ex)
            {
                throw new Exception("Error en el servicio al obtener el detalle del evento.", ex);
            }
        }


        public async Task<List<EventoResumenDto>> ListarMisEventosAgendadosAsync(int usuarioId)
        {
            try
            {
                if (usuarioId <= 0)
                {
                    return new List<EventoResumenDto>();
                }

                List<EventoResumenDto> eventos = await _eventoRepository.ListarMisEventosAgendadosAsync(usuarioId);

                return eventos;
            }
            catch (Exception ex)
            {
                throw new Exception("Error en el servicio al consultar los eventos agendados del usuario.", ex);
            }
        }





    }
}