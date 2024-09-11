using RetailSystem.Core.Entities;
using RetailSystem.Core.Models;

namespace RetailSystem.Application.Interfaces
{
    public interface IReceiptRepository
    {
        Task<ResponseApi<ReceiptModelResponse>> CreateReceipt(CreatePurchaseReceiptRequest request);
        Task<ResponseApi<ReceiptModelResponse>> GetReceiptById(Guid receiptId);
    }
}
