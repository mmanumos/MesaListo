using MesaListo.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MesaListoAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HealthController : ControllerBase
    {
        private readonly IDatabaseConnectionTester _databaseConnectionTester;

        public HealthController(IDatabaseConnectionTester databaseConnectionTester)
        {
            _databaseConnectionTester = databaseConnectionTester;
        }

        [HttpGet("database")]
        public async Task<IActionResult> CheckDatabaseConnection()
        {
            bool canConnect = await _databaseConnectionTester.CanConnectAsync();

            if (!canConnect)
            {
                return StatusCode(500, new
                {
                    success = false,
                    message = "No fue posible conectar con la base de datos."
                });
            }

            return Ok(new
            {
                success = true,
                message = "Conexión a la base de datos exitosa."
            });
        }
    }
}