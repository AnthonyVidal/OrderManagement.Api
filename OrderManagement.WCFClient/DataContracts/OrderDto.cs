using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace OrderManagement.WcfService.DataContracts
{
    [DataContract]
    public class OrderDto
    {
        [DataMember(Order = 1)]
        public DateTime Fecha { get; set; }

        [DataMember(Order = 2)]
        public string Cliente { get; set; }

        [DataMember(Order =3)]
        public List<OrderDetailDto> Detalles { get; set; }
    }
}
