using System.ServiceModel;
using OrderManagement.WcfService.DataContracts;

namespace OrderManagement.WcfService.Contracts
{
    [ServiceContract]
    public interface IOrderService
    {
        [OperationContract]
        RegisterOrderResponse RegisterOrder(OrderDto orden);
    }
}
