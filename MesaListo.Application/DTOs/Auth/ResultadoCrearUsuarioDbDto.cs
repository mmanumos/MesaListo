namespace MesaListo.Application.DTOs.Auth
{
    public class ResultadoCrearUsuarioDbDto
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
        public int? UsuarioId { get; set; }
        public string? Nombres { get; set; }
        public string? Apellidos { get; set; }
        public string? Correo { get; set; }
        public string? Alias { get; set; }
    }
}