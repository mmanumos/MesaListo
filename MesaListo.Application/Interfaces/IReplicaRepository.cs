using MesaListo.Application.DTOs.Replicas;

namespace MesaListo.Application.Interfaces
{
    public interface IReplicaRepository
    {
        Task<List<ReplicaResumenDto>> ListarReplicasPorNoticiaAsync(int usuarioId, int noticiaId);
        Task<ResultadoCrearReplicaDbDto> CrearReplicaAsync(CrearReplicaRequestDto request);
    }
}