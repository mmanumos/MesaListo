using MesaListo.Domain.Entities;

namespace MesaListo.Application.Interfaces
{
    public interface IJuegoRepository
    {
        Task<List<Juego>> ListarActivosAsync();
    }
}