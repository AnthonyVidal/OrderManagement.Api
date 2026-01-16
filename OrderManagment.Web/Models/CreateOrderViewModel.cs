using System.ComponentModel.DataAnnotations;

namespace OrderManagement.Web.Models
{
    public class CreateOrderViewModel
    {
        [Required]
        public DateTime Fecha { get; set; } = DateTime.UtcNow;

        [Required]
        public string Cliente { get; set; } = string.Empty;

        [Required]
        public List<CreateOrderDetailViewModel> Details { get; set; }
            = new();
    }
}
