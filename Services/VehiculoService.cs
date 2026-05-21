using WebhookAPI.Data;
using WebhookAPI.DTO;
using WebhookAPI.Models;

namespace WebhookAPI.Services
{
    public class VehiculoService: IVehiculoService
    {
        private readonly ILogger<VehiculoService> _logger;
        private static readonly List<VehiculoWebHookDto> _queue = new();

        //private readonly AppDbContext _context;

        public VehiculoService(ILogger<VehiculoService> logger)
        {
            _logger = logger;
            //_context = context;
        }

        public async Task<bool> ProcessWebhookVehiculosAsync(List<VehiculoWebHookDto> vehiculos)
        {
            try
            {
                _logger.LogInformation("Webhook recibido con {Count}", vehiculos.Count);
                foreach (var vehiculo in vehiculos)
                {
                    if (string.IsNullOrWhiteSpace(vehiculo.Vin))
                    {
                        _logger.LogWarning("Se salta vehículo, vin vacío");
                        continue;
                    }
                        
                    bool existe = _queue.Any(v =>
                    v.Id == vehiculo.Id);

                    if (existe)
                    {
                        _logger.LogWarning("Se salta vehículo, vin duplicado: '{Vin}'", vehiculo.Vin);
                        continue;
                    }
                        

                    _queue.Add(vehiculo);
                    _logger.LogInformation("Vehículo agregado correctamente al Queue: '{Vin}'", vehiculo.Vin);

                    //var queueRegistro = new VehiculoWebhookQueue
                    //{
                    //    Id = vehiculo.Id,
                    //    Vin = vehiculo.vin,
                    //    Placas = vehiculo.Placas,
                    //    Marca = vehiculo.Marca,
                    //    Modelo = vehiculo.Modelo,
                    //    Status = "PROCESANDO",
                    //    ContadorReintentos = 0,
                    //    FechaRecepcion = DateTime.UtcNow
                    //};

                    //_context.VehiculoWebhookQueue.Add(queueRegistro);
                    Console.WriteLine($"Vehiculo recibido: {vehiculo.Vin}");
                }

                //await _context.SaveChangesAsync();
                _logger.LogInformation("Webhook vehículos procesado correctamente");
                return true;
            }
            catch (Exception ex) {
                _logger.LogError(ex,"Error al procesar el webhook de vehículos");
                Console.WriteLine(ex.Message);

                return false;
            }
        }
    }
}
