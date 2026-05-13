namespace MesaListo.Application.DTOs.Comunidades
{
    public class ComunidadAccionResponseDto
    {
        public bool Success { get; set; }

        public string Message { get; set; } = string.Empty;

        public ComunidadResumenDto? Comunidad { get; set; }
    }
}