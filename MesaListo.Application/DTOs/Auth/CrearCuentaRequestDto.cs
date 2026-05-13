namespace MesaListo.Application.DTOs.Auth
{
    public class CrearCuentaRequestDto
    {
        public string Nombres { get; set; } = string.Empty;
        public string Apellidos { get; set; } = string.Empty;
        public string Correo { get; set; } = string.Empty;
        public string Contrasena { get; set; } = string.Empty;
        public string? Alias { get; set; }
    }
}