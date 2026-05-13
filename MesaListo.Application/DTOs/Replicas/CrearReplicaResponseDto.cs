namespace MesaListo.Application.DTOs.Replicas
{
    public class CrearReplicaResponseDto
    {
        public bool Success { get; set; }

        public string Message { get; set; } = string.Empty;

        public ReplicaResumenDto? Replica { get; set; }
    }
}