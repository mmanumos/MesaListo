using MesaListo.Application.DTOs.Replicas;
using MesaListo.Application.Interfaces;

namespace MesaListo.Application.Services
{
    public class ReplicaService
    {
        private readonly IReplicaRepository _replicaRepository;

        public ReplicaService(IReplicaRepository replicaRepository)
        {
            _replicaRepository = replicaRepository;
        }

        public async Task<List<ReplicaResumenDto>> ListarReplicasPorNoticiaAsync(int usuarioId, int noticiaId)
        {
            try
            {
                if (usuarioId <= 0)
                {
                    return new List<ReplicaResumenDto>();
                }

                if (noticiaId <= 0)
                {
                    return new List<ReplicaResumenDto>();
                }

                List<ReplicaResumenDto> replicas = await _replicaRepository.ListarReplicasPorNoticiaAsync(usuarioId, noticiaId);

                return replicas;
            }
            catch (Exception ex)
            {
                throw new Exception("Error en el servicio al consultar las réplicas de la noticia.", ex);
            }
        }



        public async Task<CrearReplicaResponseDto> CrearReplicaAsync(CrearReplicaRequestDto request)
        {
            try
            {
                if (request.UsuarioId <= 0)
                {
                    return new CrearReplicaResponseDto
                    {
                        Success = false,
                        Message = "El usuario es obligatorio para crear la réplica."
                    };
                }

                if (request.NoticiaId <= 0)
                {
                    return new CrearReplicaResponseDto
                    {
                        Success = false,
                        Message = "La noticia es obligatoria para crear la réplica."
                    };
                }

                if (string.IsNullOrWhiteSpace(request.Contenido))
                {
                    return new CrearReplicaResponseDto
                    {
                        Success = false,
                        Message = "El contenido de la réplica es obligatorio."
                    };
                }

                CrearReplicaRequestDto requestNormalizado = new CrearReplicaRequestDto
                {
                    UsuarioId = request.UsuarioId,
                    NoticiaId = request.NoticiaId,
                    Contenido = request.Contenido.Trim()
                };

                ResultadoCrearReplicaDbDto resultado = await _replicaRepository.CrearReplicaAsync(requestNormalizado);

                if (!resultado.Success ||
                    resultado.ReplicaId == null ||
                    resultado.NoticiaId == null ||
                    resultado.UsuarioId == null ||
                    resultado.FechaCreacion == null)
                {
                    return new CrearReplicaResponseDto
                    {
                        Success = false,
                        Message = resultado.Message
                    };
                }

                ReplicaResumenDto replica = new ReplicaResumenDto
                {
                    ReplicaId = resultado.ReplicaId.Value,
                    NoticiaId = resultado.NoticiaId.Value,
                    UsuarioId = resultado.UsuarioId.Value,
                    Nombres = resultado.Nombres ?? string.Empty,
                    Apellidos = resultado.Apellidos ?? string.Empty,
                    Alias = resultado.Alias,
                    Contenido = resultado.Contenido ?? string.Empty,
                    FechaCreacion = resultado.FechaCreacion.Value,
                    Estado = resultado.Estado ?? string.Empty,
                    UsuarioEsMiembro = resultado.UsuarioEsMiembro,
                    CantidadReportes = resultado.CantidadReportes
                };

                return new CrearReplicaResponseDto
                {
                    Success = true,
                    Message = resultado.Message,
                    Replica = replica
                };
            }
            catch (Exception ex)
            {
                return new CrearReplicaResponseDto
                {
                    Success = false,
                    Message = $"Error en el servicio al crear la réplica: {ex.Message}"
                };
            }
        }








    }
}