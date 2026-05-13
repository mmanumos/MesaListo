namespace MesaListo.Application.DTOs.Replicas
{
    public class ResultadoCrearReplicaDbDto
    {
        public bool Success { get; set; }

        public string Message { get; set; } = string.Empty;

        public int? ReplicaId { get; set; }

        public int? NoticiaId { get; set; }

        public int? UsuarioId { get; set; }

        public string? Nombres { get; set; }

        public string? Apellidos { get; set; }

        public string? Alias { get; set; }

        public string? Contenido { get; set; }

        public DateTime? FechaCreacion { get; set; }

        public string? Estado { get; set; }

        public bool UsuarioEsMiembro { get; set; }

        public int CantidadReportes { get; set; }
    }
}