using System.Runtime.Serialization;

namespace OrderManagement.WcfService.DataContracts
{
    [DataContract]
    public class OrderDetailDto
    {
        [DataMember(Order = 1)]
        public string Producto { get; set; }

        [DataMember(Order = 2)]
        public int Cantidad { get; set; }

        [DataMember(Order = 3)]
        public decimal PrecioUnitario { get; set; }
    }
}
