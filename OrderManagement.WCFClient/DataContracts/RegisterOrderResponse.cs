using System;
using System.Runtime.Serialization;

namespace OrderManagement.WcfService.DataContracts
{
    [DataContract]
    public class RegisterOrderResponse
    {
        [DataMember]
        public bool Exito { get; set; }

        [DataMember]
        public Guid Id { get; set; }

        [DataMember]
        public string Mensaje { get; set; }
    }
}
