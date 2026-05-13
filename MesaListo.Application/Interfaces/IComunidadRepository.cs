using MesaListo.Application.DTOs.Comunidades;

namespace MesaListo.Application.Interfaces
{
    public interface IComunidadRepository
    {
        Task<List<ComunidadResumenDto>> ListarMisComunidadesAsync(int usuarioId);

        Task<List<ComunidadResumenDto>> ListarComunidadesPopularesParaExplorarAsync(int usuarioId);

        Task<List<ComunidadResumenDto>> BuscarComunidadesPorNombreAsync(int usuarioId, string nombre);
        Task<ResultadoCrearComunidadDbDto> CrearComunidadAsync(CrearComunidadRequestDto request);
        Task<ResultadoComunidadAccionDbDto> UnirseComunidadAsync(UnirseComunidadRequestDto request);
        Task<ResultadoComunidadAccionDbDto> SalirComunidadAsync(SalirComunidadRequestDto request);
    }
}