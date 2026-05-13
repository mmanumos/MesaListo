namespace MesaListo.Application.DTOs.Auth
{
    public class UsuarioSesionDto
    {
        public int UsuarioId { get; set; }
        public string Nombres { get; set; } = string.Empty;
        public string Apellidos { get; set; } = string.Empty;
        public string Correo { get; set; } = string.Empty;
        public string? Alias { get; set; }
    }
}