using System;
using System.Collections.Generic;
using System.Text;

namespace OrderManagement.Application.Orders.Dtos
{
    public class OrderDetailDto
    {
        public string Producto { get; set; }
        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
        public decimal Subtotal { get; set; }
    }

}
