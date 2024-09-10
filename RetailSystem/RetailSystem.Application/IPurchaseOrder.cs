using RetailSystem.Api.Models;
using RetailSystem.Core.RequestModel;
using System;
using System.Threading.Tasks;

namespace RetailSystem.Application.Interfaces
{
    public interface IPurchaseOrderRepository
    {
     Task<ResponseApi<PurchaseOrderModel>> CreatePurchaseOrder(Guid supplierId, List<PurchaseOrderItemModel> orderItems);
     Task<ResponseApi<PurchaseOrderModel>> GetPurchaseOrderById(Guid id);
     Task<ResponseApi<bool>> UpdatePurchaseOrderStatus(Guid purchaseOrderId, Guid purchaseOrderStatusId);
    }
}
