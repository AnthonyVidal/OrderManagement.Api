using System;
using OrderManagement.WcfService.Contracts;
using OrderManagement.WcfService.DataContracts;

namespace OrderManagement.WcfService.Services
{
    public class OrderService : IOrderService
    {
        public RegisterOrderResponse RegisterOrder(OrderDto orden)
        {
            // Simulación de reglas básicas
            if (orden == null || orden.Detalles == null || orden.Detalles.Count == 0)
            {
                return new RegisterOrderResponse
                {
                    Exito = false,
                    Mensaje = "La orden debe tener al menos un detalle"
                };
            }

            // Simulación de registro exitoso
            return new RegisterOrderResponse
            {
                Exito = true,
                Id = Guid.NewGuid(),
                Mensaje = "Orden registrada correctamente (WCF simulado)"
            };
        }
    }
}
