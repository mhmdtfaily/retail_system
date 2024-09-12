using RetailSystem.Core.Models;
using RetailSystem.Core.RequestModel;
using System;
using System.Threading.Tasks;

namespace RetailSystem.Application.Interfaces
{
    public interface IPurchaseOrderRepository
    {
     Task<ResponseApi<PurchaseOrderModel>> CreatePurchaseOrder(Guid supplierId, List<GetPurchaseOrderItemModel> orderItems);
     Task<ResponseApi<PurchaseOrderModel>> GetPurchaseOrderById(Guid id);
     Task<ResponseApi<bool>> UpdatePurchaseOrderStatus(Guid purchaseOrderId, string purchaseOrderStatus);
    }
}
