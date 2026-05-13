namespace MesaListo.Application.DTOs.Noticias
{
    public class CrearNoticiaRequestDto
    {
        public int UsuarioId { get; set; }

        public int ComunidadId { get; set; }

        public int? EventoId { get; set; }

        public string Titulo { get; set; } = string.Empty;

        public string Contenido { get; set; } = string.Empty;
    }
}