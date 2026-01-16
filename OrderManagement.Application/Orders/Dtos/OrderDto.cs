using System;
using System.Collections.Generic;
using System.Text;

namespace OrderManagement.Application.Orders.Dtos
{
    public class OrderDto
    {
        public Guid Id { get; set; }
        public DateTime Fecha { get; set; }
        public string Cliente { get; set; }
        public decimal Total { get; set; }
        public List<OrderDetailDto> Details { get; set; }
    }

}
