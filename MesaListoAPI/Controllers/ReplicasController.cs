using MesaListo.Application.DTOs.Replicas;
using MesaListo.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace MesaListoAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReplicasController : ControllerBase
    {
        private readonly ReplicaService _replicaService;

        public ReplicasController(ReplicaService replicaService)
        {
            _replicaService = replicaService;
        }

        [HttpGet("getReplicasPorNoticia/{usuarioId}/{noticiaId}")]
        public async Task<IActionResult> GetReplicasPorNoticia(int usuarioId, int noticiaId)
        {
            try
            {
                List<ReplicaResumenDto> replicas = await _replicaService.ListarReplicasPorNoticiaAsync(usuarioId, noticiaId);

                return Ok(new
                {
                    success = true,
                    message = "Réplicas de la noticia consultadas correctamente.",
                    data = replicas
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    success = false,
                    message = "Error consultando las réplicas de la noticia.",
                    error = ex.Message
                });
            }
        }


        [HttpPost("crearReplica")]
        public async Task<IActionResult> CrearReplica([FromBody] CrearReplicaRequestDto request)
        {
            try
            {
                CrearReplicaResponseDto response = await _replicaService.CrearReplicaAsync(request);

                if (!response.Success)
                {
                    return BadRequest(response);
                }

                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    success = false,
                    message = "Error inesperado al crear la réplica.",
                    error = ex.Message
                });
            }
        }



    }
}