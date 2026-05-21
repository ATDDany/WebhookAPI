using WebhookAPI.Data;
using WebhookAPI.DTO;
using WebhookAPI.Models;

namespace WebhookAPI.Services
{
    public class VehiculoService: IVehiculoService
    {
        private static readonly List<VehiculoWebHookDto> _queue = new();
        //private readonly AppDbContext _context;

        //public VehiculoService(AppDbContext context)
        //{
        //    _context = context;
        //}

        public async Task<bool> ProcessWebhookVehiculosAsync(List<VehiculoWebHookDto> vehiculos)
        {
            try
            {
                foreach (var vehiculo in vehiculos)
                {
                    if (string.IsNullOrWhiteSpace(vehiculo.vin))
                        continue;
                    bool existe = _queue.Any(v =>
                    v.Id == vehiculo.Id);

                    if(existe)
                        continue;

                    _queue.Add(vehiculo);

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
                    Console.WriteLine($"Vehiculo recibido: {vehiculo.vin}"
                        );
                }

                //await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex) {
                Console.WriteLine(ex.Message);

                return false;
            }
        }
    }
}
