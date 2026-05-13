namespace MesaListo.Application.DTOs.Comunidades
{
    public class ResultadoCrearComunidadDbDto
    {
        public bool Success { get; set; }

        public string Message { get; set; } = string.Empty;

        public int? ComunidadId { get; set; }

        public string? Nombre { get; set; }

        public string? Descripcion { get; set; }

        public string? LineamientosConvivencia { get; set; }

        public int? PropietarioUsuarioId { get; set; }

        public DateTime? FechaCreacion { get; set; }

        public string? Estado { get; set; }
    }
}