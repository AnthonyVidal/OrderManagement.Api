using System.ComponentModel.DataAnnotations;

namespace OrderManagement.Web.Models
{
    public class CreateOrderDetailViewModel
    {
        [Required]
        public string Producto { get; set; } = string.Empty;

        [Required]
        [Range(1, 1000)]
        public int Cantidad { get; set; }

        [Required]
        [Range(0.01, 999999)]
        public decimal PrecioUnitario { get; set; }
    }
}
