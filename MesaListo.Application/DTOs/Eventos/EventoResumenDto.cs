namespace MesaListo.Application.DTOs.Eventos
{
    public class EventoResumenDto
    {
        public int EventoId { get; set; }

        public int AnfitrionUsuarioId { get; set; }

        public string NombresAnfitrion { get; set; } = string.Empty;

        public string ApellidosAnfitrion { get; set; } = string.Empty;

        public string? AliasAnfitrion { get; set; }

        public string Titulo { get; set; } = string.Empty;

        public string Descripcion { get; set; } = string.Empty;

        public DateTime FechaHoraInicio { get; set; }

        public string Lugar { get; set; } = string.Empty;

        public int AforoMaximo { get; set; }

        public string LineamientosConvivencia { get; set; } = string.Empty;

        public DateTime FechaCreacion { get; set; }

        public string Estado { get; set; } = string.Empty;

        public int CantidadAsistentes { get; set; }

        public int CuposDisponibles { get; set; }

        public bool EstaAgendado { get; set; }

        public string Juegos { get; set; } = string.Empty;
    }
}