using System.ComponentModel.DataAnnotations;

namespace WebhookAPI.Models
{
    public class VehiculoWebhookQueue
    {


        [Key]
        public string Id { get; set; }

        [Required]
        [MaxLength(20)]
        public required string Vin { get; set; }

        [Required]
        [MaxLength(20)]
        public required string Placas { get; set; }

        [Required]
        [MaxLength(20)]
        public required string Marca { get; set; }

        [Required]
        [MaxLength(20)]
        public required string Modelo { get; set; }

        [Required]
        [MaxLength(50)]
        public required string Status { get; set; }

        public int ContadorReintentos { get; set; } = 0;

        [MaxLength(1000)]
        public string? MensajeError { get; set; }
        public DateTime FechaRecepcion { get; set; }
        public DateTime? FechaProcesamiento { get; set; }
        public DateTime? FechaFinalizacion { get; set; }
    }
}
