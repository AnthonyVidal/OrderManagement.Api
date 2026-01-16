using System;
using System.Collections.Generic;
using System.Text;

namespace OrderManagement.Application.Orders.Commands.CreateOrder
{
    public class CreateOrderDetailDto
    {
        public string Producto { get; set; } = default!;
        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
    }
}
