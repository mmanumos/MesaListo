namespace MesaListo.Application.DTOs.Auth
{
    public class IniciarSesionRequestDto
    {
        public string Correo { get; set; } = string.Empty;
        public string Contrasena { get; set; } = string.Empty;
    }
}