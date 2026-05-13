namespace MesaListo.Application.DTOs.Replicas
{
    public class ReplicaResumenDto
    {
        public int ReplicaId { get; set; }

        public int NoticiaId { get; set; }

        public int UsuarioId { get; set; }

        public string Nombres { get; set; } = string.Empty;

        public string Apellidos { get; set; } = string.Empty;

        public string? Alias { get; set; }

        public string Contenido { get; set; } = string.Empty;

        public DateTime FechaCreacion { get; set; }

        public string Estado { get; set; } = string.Empty;

        public bool UsuarioEsMiembro { get; set; }

        public int CantidadReportes { get; set; }
    }
}