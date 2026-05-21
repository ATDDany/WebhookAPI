using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using WebhookAPI.DTO;
using WebhookAPI.Models;
using WebhookAPI.Services;

namespace WebhookAPI.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class WebhookController : ControllerBase
    {
        private readonly IVehiculoService _vehiculoService;

        public WebhookController(IVehiculoService service) { 
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

            return Accepted(new
            {
                mensaje = "Webhook vehiculos recibido correctamente.",
                totalRecibidos = vehiculos.Count,
                totalInsertados = result
            });
        }
    }
}
