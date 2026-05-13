using MesaListo.Application.DTOs.Noticias;
using MesaListo.Application.Interfaces;

namespace MesaListo.Application.Services
{
    public class NoticiaService
    {
        private readonly INoticiaRepository _noticiaRepository;

        public NoticiaService(INoticiaRepository noticiaRepository)
        {
            _noticiaRepository = noticiaRepository;
        }

        public async Task<List<NoticiaResumenDto>> ListarNoticiasPorComunidadAsync(int usuarioId, int comunidadId)
        {
            try
            {
                if (usuarioId <= 0)
                {
                    return new List<NoticiaResumenDto>();
                }

                if (comunidadId <= 0)
                {
                    return new List<NoticiaResumenDto>();
                }

                List<NoticiaResumenDto> noticias = await _noticiaRepository.ListarNoticiasPorComunidadAsync(usuarioId, comunidadId);

                return noticias;
            }
            catch (Exception ex)
            {
                throw new Exception("Error en el servicio al consultar las noticias de la comunidad.", ex);
            }
        }



        public async Task<CrearNoticiaResponseDto> CrearNoticiaAsync(CrearNoticiaRequestDto request)
        {
            try
            {
                if (request.UsuarioId <= 0)
                {
                    return new CrearNoticiaResponseDto
                    {
                        Success = false,
                        Message = "El usuario es obligatorio para crear la noticia."
                    };
                }

                if (request.ComunidadId <= 0)
                {
                    return new CrearNoticiaResponseDto
                    {
                        Success = false,
                        Message = "La comunidad es obligatoria para crear la noticia."
                    };
                }

                if (string.IsNullOrWhiteSpace(request.Titulo))
                {
                    return new CrearNoticiaResponseDto
                    {
                        Success = false,
                        Message = "El título de la noticia es obligatorio."
                    };
                }

                if (string.IsNullOrWhiteSpace(request.Contenido))
                {
                    return new CrearNoticiaResponseDto
                    {
                        Success = false,
                        Message = "El contenido de la noticia es obligatorio."
                    };
                }

                CrearNoticiaRequestDto requestNormalizado = new CrearNoticiaRequestDto
                {
                    UsuarioId = request.UsuarioId,
                    ComunidadId = request.ComunidadId,
                    EventoId = request.EventoId,
                    Titulo = request.Titulo.Trim(),
                    Contenido = request.Contenido.Trim()
                };

                ResultadoCrearNoticiaDbDto resultado = await _noticiaRepository.CrearNoticiaAsync(requestNormalizado);

                if (!resultado.Success ||
                    resultado.NoticiaId == null ||
                    resultado.ComunidadId == null ||
                    resultado.UsuarioId == null ||
                    resultado.FechaCreacion == null)
                {
                    return new CrearNoticiaResponseDto
                    {
                        Success = false,
                        Message = resultado.Message
                    };
                }

                NoticiaResumenDto noticia = new NoticiaResumenDto
                {
                    NoticiaId = resultado.NoticiaId.Value,
                    ComunidadId = resultado.ComunidadId.Value,
                    UsuarioId = resultado.UsuarioId.Value,
                    Nombres = resultado.Nombres ?? string.Empty,
                    Apellidos = resultado.Apellidos ?? string.Empty,
                    Alias = resultado.Alias,
                    EventoId = resultado.EventoId,
                    Titulo = resultado.Titulo ?? string.Empty,
                    Contenido = resultado.Contenido ?? string.Empty,
                    FechaCreacion = resultado.FechaCreacion.Value,
                    Estado = resultado.Estado ?? string.Empty,
                    UsuarioEsMiembro = resultado.UsuarioEsMiembro,
                    CantidadReplicas = resultado.CantidadReplicas,
                    CantidadReportes = resultado.CantidadReportes
                };

                return new CrearNoticiaResponseDto
                {
                    Success = true,
                    Message = resultado.Message,
                    Noticia = noticia
                };
            }
            catch (Exception ex)
            {
                return new CrearNoticiaResponseDto
                {
                    Success = false,
                    Message = $"Error en el servicio al crear la noticia: {ex.Message}"
                };
            }
        }






    }
}