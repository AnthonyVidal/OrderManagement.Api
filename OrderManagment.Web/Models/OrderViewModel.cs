using System.ComponentModel.DataAnnotations;

namespace OrderManagement.Web.Models
{
    public class OrderViewModel
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "El cliente es obligatorio")]
        public string Cliente { get; set; } = string.Empty;

        [Required(ErrorMessage = "El total es obligatorio")]
        [Range(0.01, 999999)]
        public decimal Total { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
