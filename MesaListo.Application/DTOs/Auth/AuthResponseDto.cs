namespace MesaListo.Application.DTOs.Auth
{
    public class AuthResponseDto
    {
        public bool Success { get; set; }

        public string Message { get; set; } = string.Empty;

        public UsuarioSesionDto? Usuario { get; set; }
    }
}