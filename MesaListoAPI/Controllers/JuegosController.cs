using MesaListo.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace MesaListoAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class JuegosController : ControllerBase
    {
        private readonly JuegoService _juegoService;

        public JuegosController(JuegoService juegoService)
        {
            _juegoService = juegoService;
        }

        [HttpGet("getJuegosActivos")]
        public async Task<IActionResult> ListarActivos()
        {
            var juegos = await _juegoService.ListarActivosAsync();

            return Ok(new
            {
                success = true,
                message = "Juegos activos consultados correctamente.",
                data = juegos
            });
        }
    }
}