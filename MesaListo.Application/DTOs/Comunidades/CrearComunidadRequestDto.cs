namespace MesaListo.Application.DTOs.Comunidades
{
    public class CrearComunidadRequestDto
    {
        public int UsuarioId { get; set; }

        public string Nombre { get; set; } = string.Empty;

        public string Descripcion { get; set; } = string.Empty;

        public string LineamientosConvivencia { get; set; } = string.Empty;
    }
}