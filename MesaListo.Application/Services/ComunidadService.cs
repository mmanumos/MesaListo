using MesaListo.Application.DTOs.Comunidades;
using MesaListo.Application.Interfaces;

namespace MesaListo.Application.Services
{
    public class ComunidadService
    {
        private readonly IComunidadRepository _comunidadRepository;

        public ComunidadService(IComunidadRepository comunidadRepository)
        {
            _comunidadRepository = comunidadRepository;
        }

        public async Task<List<ComunidadResumenDto>> ListarMisComunidadesAsync(int usuarioId)
        {
            try
            {
                List<ComunidadResumenDto> comunidades = await _comunidadRepository.ListarMisComunidadesAsync(usuarioId);

                return comunidades;
            }
            catch (Exception ex)
            {
                throw new Exception("Error en el servicio al consultar las comunidades del usuario.", ex);
            }
        }

        public async Task<List<ComunidadResumenDto>> ListarComunidadesPopularesParaExplorarAsync(int usuarioId)
        {
            try
            {
                List<ComunidadResumenDto> comunidades = await _comunidadRepository.ListarComunidadesPopularesParaExplorarAsync(usuarioId);

                return comunidades;
            }
            catch (Exception ex)
            {
                throw new Exception("Error en el servicio al consultar comunidades populares para explorar.", ex);
            }
        }

        public async Task<List<ComunidadResumenDto>> BuscarComunidadesPorNombreAsync(int usuarioId, string nombre)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(nombre))
                {
                    return new List<ComunidadResumenDto>();
                }

                List<ComunidadResumenDto> comunidades = await _comunidadRepository.BuscarComunidadesPorNombreAsync(usuarioId, nombre.Trim());

                return comunidades;
            }
            catch (Exception ex)
            {
                throw new Exception("Error en el servicio al buscar comunidades por nombre.", ex);
            }
        }


        public async Task<CrearComunidadResponseDto> CrearComunidadAsync(CrearComunidadRequestDto request)
        {
            try
            {
                if (request.UsuarioId <= 0)
                {
                    return new CrearComunidadResponseDto
                    {
                        Success = false,
                        Message = "El usuario es obligatorio para crear la comunidad."
                    };
                }

                if (string.IsNullOrWhiteSpace(request.Nombre))
                {
                    return new CrearComunidadResponseDto
                    {
                        Success = false,
                        Message = "El nombre de la comunidad es obligatorio."
                    };
                }

                if (string.IsNullOrWhiteSpace(request.Descripcion))
                {
                    return new CrearComunidadResponseDto
                    {
                        Success = false,
                        Message = "La descripción de la comunidad es obligatoria."
                    };
                }

                if (string.IsNullOrWhiteSpace(request.LineamientosConvivencia))
                {
                    return new CrearComunidadResponseDto
                    {
                        Success = false,
                        Message = "Los lineamientos de convivencia son obligatorios."
                    };
                }

                CrearComunidadRequestDto requestNormalizado = new CrearComunidadRequestDto
                {
                    UsuarioId = request.UsuarioId,
                    Nombre = request.Nombre.Trim(),
                    Descripcion = request.Descripcion.Trim(),
                    LineamientosConvivencia = request.LineamientosConvivencia.Trim()
                };

                ResultadoCrearComunidadDbDto resultado = await _comunidadRepository.CrearComunidadAsync(requestNormalizado);

                if (!resultado.Success || resultado.ComunidadId == null || resultado.PropietarioUsuarioId == null || resultado.FechaCreacion == null)
                {
                    return new CrearComunidadResponseDto
                    {
                        Success = false,
                        Message = resultado.Message
                    };
                }

                ComunidadResumenDto comunidad = new ComunidadResumenDto
                {
                    ComunidadId = resultado.ComunidadId.Value,
                    Nombre = resultado.Nombre ?? string.Empty,
                    Descripcion = resultado.Descripcion,
                    LineamientosConvivencia = resultado.LineamientosConvivencia,
                    PropietarioUsuarioId = resultado.PropietarioUsuarioId.Value,
                    FechaCreacion = resultado.FechaCreacion.Value,
                    Estado = resultado.Estado ?? string.Empty,
                    EsPropietario = true,
                    CantidadMiembros = 1
                };

                return new CrearComunidadResponseDto
                {
                    Success = true,
                    Message = "Comunidad creada correctamente.",
                    Comunidad = comunidad
                };
            }
            catch (Exception ex)
            {
                return new CrearComunidadResponseDto
                {
                    Success = false,
                    Message = $"Error en el servicio al crear la comunidad: {ex.Message}"
                };
            }
        }


        public async Task<ComunidadAccionResponseDto> UnirseComunidadAsync(UnirseComunidadRequestDto request)
        {
            try
            {
                if (request.UsuarioId <= 0)
                {
                    return new ComunidadAccionResponseDto
                    {
                        Success = false,
                        Message = "El usuario es obligatorio para unirse a la comunidad."
                    };
                }

                if (request.ComunidadId <= 0)
                {
                    return new ComunidadAccionResponseDto
                    {
                        Success = false,
                        Message = "La comunidad es obligatoria."
                    };
                }

                ResultadoComunidadAccionDbDto resultado = await _comunidadRepository.UnirseComunidadAsync(request);

                if (!resultado.Success || resultado.ComunidadId == null || resultado.PropietarioUsuarioId == null || resultado.FechaCreacion == null)
                {
                    return new ComunidadAccionResponseDto
                    {
                        Success = false,
                        Message = resultado.Message
                    };
                }

                ComunidadResumenDto comunidad = new ComunidadResumenDto
                {
                    ComunidadId = resultado.ComunidadId.Value,
                    Nombre = resultado.Nombre ?? string.Empty,
                    Descripcion = resultado.Descripcion,
                    LineamientosConvivencia = resultado.LineamientosConvivencia,
                    PropietarioUsuarioId = resultado.PropietarioUsuarioId.Value,
                    FechaCreacion = resultado.FechaCreacion.Value,
                    Estado = resultado.Estado ?? string.Empty,
                    EsPropietario = resultado.EsPropietario,
                    CantidadMiembros = resultado.CantidadMiembros
                };

                return new ComunidadAccionResponseDto
                {
                    Success = true,
                    Message = resultado.Message,
                    Comunidad = comunidad
                };
            }
            catch (Exception ex)
            {
                return new ComunidadAccionResponseDto
                {
                    Success = false,
                    Message = $"Error en el servicio al unirse a la comunidad: {ex.Message}"
                };
            }
        }


        public async Task<ComunidadAccionResponseDto> SalirComunidadAsync(SalirComunidadRequestDto request)
        {
            try
            {
                if (request.UsuarioId <= 0)
                {
                    return new ComunidadAccionResponseDto
                    {
                        Success = false,
                        Message = "El usuario es obligatorio para salir de la comunidad."
                    };
                }

                if (request.ComunidadId <= 0)
                {
                    return new ComunidadAccionResponseDto
                    {
                        Success = false,
                        Message = "La comunidad es obligatoria."
                    };
                }

                ResultadoComunidadAccionDbDto resultado = await _comunidadRepository.SalirComunidadAsync(request);

                if (!resultado.Success || resultado.ComunidadId == null || resultado.PropietarioUsuarioId == null || resultado.FechaCreacion == null)
                {
                    return new ComunidadAccionResponseDto
                    {
                        Success = false,
                        Message = resultado.Message
                    };
                }

                ComunidadResumenDto comunidad = new ComunidadResumenDto
                {
                    ComunidadId = resultado.ComunidadId.Value,
                    Nombre = resultado.Nombre ?? string.Empty,
                    Descripcion = resultado.Descripcion,
                    LineamientosConvivencia = resultado.LineamientosConvivencia,
                    PropietarioUsuarioId = resultado.PropietarioUsuarioId.Value,
                    FechaCreacion = resultado.FechaCreacion.Value,
                    Estado = resultado.Estado ?? string.Empty,
                    EsPropietario = resultado.EsPropietario,
                    CantidadMiembros = resultado.CantidadMiembros
                };

                return new ComunidadAccionResponseDto
                {
                    Success = true,
                    Message = resultado.Message,
                    Comunidad = comunidad
                };
            }
            catch (Exception ex)
            {
                return new ComunidadAccionResponseDto
                {
                    Success = false,
                    Message = $"Error en el servicio al salir de la comunidad: {ex.Message}"
                };
            }
        }







    }
}