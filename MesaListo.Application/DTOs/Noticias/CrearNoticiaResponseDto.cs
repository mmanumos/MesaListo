namespace MesaListo.Application.DTOs.Noticias
{
    public class CrearNoticiaResponseDto
    {
        public bool Success { get; set; }

        public string Message { get; set; } = string.Empty;

        public NoticiaResumenDto? Noticia { get; set; }
    }
}