namespace MesaListo.Application.DTOs.Replicas
{
    public class CrearReplicaRequestDto
    {
        public int UsuarioId { get; set; }

        public int NoticiaId { get; set; }

        public string Contenido { get; set; } = string.Empty;
    }
}