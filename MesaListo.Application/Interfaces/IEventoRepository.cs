using MesaListo.Application.DTOs.Eventos;

namespace MesaListo.Application.Interfaces
{
    public interface IEventoRepository
    {
        Task<ResultadoCrearEventoDbDto> CrearEventoAsync(CrearEventoRequestDto request);
        Task<List<EventoResumenDto>> ListarEventosProximosAsync(int usuarioId);
        Task<ResultadoEventoAccionDbDto> AsistirEventoAsync(AsistirEventoRequestDto request);
        Task<ResultadoEventoAccionDbDto> CancelarAsistenciaEventoAsync(CancelarAsistenciaEventoRequestDto request);
        Task<EventoResumenDto?> ObtenerDetalleEventoAsync(int usuarioId, int eventoId);
        Task<List<EventoResumenDto>> ListarMisEventosAgendadosAsync(int usuarioId);
    }
}