namespace MesaListo.Application.DTOs.Comunidades
{
    public class ComunidadResumenDto
    {
        public int ComunidadId { get; set; }

        public string Nombre { get; set; } = string.Empty;

        public string? Descripcion { get; set; }

        public string? LineamientosConvivencia { get; set; }

        public int PropietarioUsuarioId { get; set; }

        public DateTime FechaCreacion { get; set; }

        public string Estado { get; set; } = string.Empty;

        public bool EsPropietario { get; set; }

        public int CantidadMiembros { get; set; }
    }
}