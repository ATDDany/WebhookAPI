using System.ComponentModel.DataAnnotations;

namespace WebhookAPI.Models
{
    public class VehiculoWebhookQueue
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public required string IdExterno { get; set; }

        [Required]
        [MaxLength(20)]
        public required string Vin { get; set; }

        [Required]
        public required string Carga { get; set; }

        [MaxLength(50)]
        public string? Estado { get; set; }

        public int ContadorReintentos { get; set; } = 0;

        [MaxLength(1000)]
        public string? MensajeError { get; set; }
        public DateTime FechaRecepcion { get; set; }
        public DateTime? FechaProcesamiento { get; set; }
        public DateTime? FechaFinalizacion { get; set; }
    }
}
