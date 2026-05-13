namespace MesaListo.Application.DTOs.Noticias
{
    public class NoticiaResumenDto
    {
        public int NoticiaId { get; set; }

        public int ComunidadId { get; set; }

        public int UsuarioId { get; set; }

        public string Nombres { get; set; } = string.Empty;

        public string Apellidos { get; set; } = string.Empty;

        public string? Alias { get; set; }

        public int? EventoId { get; set; }

        public string Titulo { get; set; } = string.Empty;

        public string Contenido { get; set; } = string.Empty;

        public DateTime FechaCreacion { get; set; }

        public string Estado { get; set; } = string.Empty;

        public bool UsuarioEsMiembro { get; set; }

        public int CantidadReplicas { get; set; }

        public int CantidadReportes { get; set; }
    }
}