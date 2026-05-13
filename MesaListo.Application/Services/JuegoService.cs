using MesaListo.Application.Interfaces;
using MesaListo.Domain.Entities;

namespace MesaListo.Application.Services
{
    public class JuegoService
    {
        private readonly IJuegoRepository _juegoRepository;

        public JuegoService(IJuegoRepository juegoRepository)
        {
            _juegoRepository = juegoRepository;
        }

        public async Task<List<Juego>> ListarActivosAsync()
        {
            return await _juegoRepository.ListarActivosAsync();
        }
    }
}