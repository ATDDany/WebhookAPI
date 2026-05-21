using WebhookAPI.DTO;

namespace WebhookAPI.Services
{
    public interface IVehiculoService
    {
        Task<bool> ProcessWebhookVehiculosAsync(List<VehiculoWebHookDto> vehiculos);
    }
}
