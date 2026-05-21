using Microsoft.EntityFrameworkCore;
using WebhookAPI.Data;
using WebhookAPI.Models;

namespace WebhookAPI.BackgroundServices
{
    public class VehiculoQueueProcessor : BackgroundService
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly ILogger<VehiculoQueueProcessor> _logger;

        public VehiculoQueueProcessor(IServiceScopeFactory scopeFactory, ILogger<VehiculoQueueProcessor> logger)
        {
            _scopeFactory = scopeFactory;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try {
                    using var scope = _scopeFactory.CreateScope();

                    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

                    var registrosPendientes = await context.VehiculoWebhookQueue
                        .Where(v => v.Estado == "PENDIENTE")
                        .Take(50)
                        .ToListAsync();

                    foreach (var queue in registrosPendientes)
                    {
                        try
                        {
                            queue.Estado = "PROCESANDO";

                            await context.SaveChangesAsync();

                            var vehiculo = new Vehiculo
                            {
                                IdExterno = queue.IdExterno,
                                Vin = queue.Vin,
                                FechaActualizacion = DateTime.UtcNow
                            };

                            context.Vehiculos.Add(vehiculo);

                            queue.Estado = "PROCESADO";

                            await context.SaveChangesAsync();

                            _logger.LogInformation("Vehículo Procesado: '{Vin}' ", queue.Vin);

                        }
                        catch (Exception ex)
                        {
                            queue.Estado = "ERROR";
                            queue.MensajeError = ex.Message;
                            queue.ContadorReintentos++;

                            await context.SaveChangesAsync();

                            _logger.LogError("Error procesando Vehículo: '{Vin}' ", queue.Vin);
                        }
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error en Background Service");
                }

                await Task.Delay(
                    TimeSpan.FromSeconds(10), stoppingToken);
            }

        }
    }
}
