using MesaListo.Application.DTOs.Noticias;

namespace MesaListo.Application.Interfaces
{
    public interface INoticiaRepository
    {
        Task<List<NoticiaResumenDto>> ListarNoticiasPorComunidadAsync(int usuarioId, int comunidadId);
        Task<ResultadoCrearNoticiaDbDto> CrearNoticiaAsync(CrearNoticiaRequestDto request);
    }
}