namespace WebhookAPI.Models
{
    public class Vehiculo
    {
        public int Id { get; set; }
        public string IdExterno { get; set; }
        public string Vin { get; set; }
        public string Placas { get; set; }
        public string Marca { get; set; }
        public string Modelo { get; set; }
        public string Anio { get; set; }
        public string Color { get; set; }
        public string Estado { get; set; }
        public DateTime FechaRegistro { get; set; }
        public DateTime FechaActualizacion { get; set; }
    }
}
