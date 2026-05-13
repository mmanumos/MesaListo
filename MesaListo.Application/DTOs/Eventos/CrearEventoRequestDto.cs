namespace MesaListo.Application.DTOs.Eventos
{
    public class CrearEventoRequestDto
    {
        public int UsuarioId { get; set; }

        public string Titulo { get; set; } = string.Empty;

        public string Descripcion { get; set; } = string.Empty;

        public DateTime FechaHoraInicio { get; set; }

        public string Lugar { get; set; } = string.Empty;

        public int AforoMaximo { get; set; }

        public string LineamientosConvivencia { get; set; } = string.Empty;

        public List<int> JuegosIds { get; set; } = new List<int>();

        public List<int> ComunidadesIds { get; set; } = new List<int>();
    }
}