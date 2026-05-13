namespace MesaListo.Domain.Entities
{
    public class Juego
    {
        public int JuegoId { get; set; }

        public string Nombre { get; set; } = string.Empty;

        public string? Descripcion { get; set; }

        public int MinJugadores { get; set; }

        public int MaxJugadores { get; set; }

        public int? DuracionMin { get; set; }

        public string Estado { get; set; } = string.Empty;
    }
}