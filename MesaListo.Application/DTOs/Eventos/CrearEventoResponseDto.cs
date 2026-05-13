namespace MesaListo.Application.DTOs.Eventos
{
    public class CrearEventoResponseDto
    {
        public bool Success { get; set; }

        public string Message { get; set; } = string.Empty;

        public int? EventoId { get; set; }
    }
}