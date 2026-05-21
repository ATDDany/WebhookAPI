using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using WebhookAPI.DTO;
using WebhookAPI.Models;
using WebhookAPI.Services;

namespace WebhookAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VehiculosController : ControllerBase
    {
        private readonly IVehiculoService _vehiculoService;

        public VehiculosController(IVehiculoService service) { 
            _vehiculoService = service;
        }
        [HttpPost("vehiculos")]
        public async Task<IActionResult> RecibirVehiculos([FromBody] List<VehiculoWebHookDto> vehiculos)
        {
            if (vehiculos == null || vehiculos.Count == 0)
            {
                return BadRequest(new
                {
                    mensaje = "no se recibieron registros"
                });
            }

            var result = await _vehiculoService.ProcessWebhookVehiculosAsync(vehiculos);

            return Ok(new
            {
                mensaje = "webhook vehiculos recibido correctamente",
                totalRecibidos = vehiculos.Count,
                totalInsertados = result
            });
        }
    }
}
