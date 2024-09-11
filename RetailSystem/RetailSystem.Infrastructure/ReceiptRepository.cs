using RetailSystem.Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using RetailSystem.Core.Entities;
using RetailSystem.Core.Models;
using System;
using System.Linq;
using System.Threading.Tasks;
using RetailSystem.Core.RequestModel;

namespace RetailSystem.Infrastructure.Repositories
{
    public class ReceiptRepository : IReceiptRepository
    {
        private readonly RetailDbContext _context;

        public ReceiptRepository(RetailDbContext context)
        {
            _context = context;
        }

        public async Task<ResponseApi<ReceiptModelResponse>> CreateReceipt(CreatePurchaseReceiptRequest request)
        {
            var response = new ResponseApi<ReceiptModelResponse>();

            try
            {
                // check if a receipt already exists for the given PurchaseOrderId
                var existingReceipt = await _context.Receipts
                    .FirstOrDefaultAsync(r => r.PurchaseOrderId == request.PurchaseOrderId);

                if (existingReceipt != null)
                {
                    // if receipt already exists, retrieve it and return the response
                    return await GetReceiptById(existingReceipt.Id);
                }
                var purchaseOrder = await _context.PurchaseOrders
                    .Include(p => p.PurchaseOrderItems)
                    .FirstOrDefaultAsync(p => p.Id == request.PurchaseOrderId);

                if (purchaseOrder == null)
                {
                    response.IsSuccess = false;
                    response.Message = "Purchase order not found";
                    response.StatusCode = 404;
                    return response;
                }

                var completedStatus = await _context.PurchaseOrderStatuses
                    .FirstOrDefaultAsync(s => s.MachineName == "completed");

                if (completedStatus == null)
                {
                    response.IsSuccess = false;
                    response.Message = "Completed status not found.";
                    response.StatusCode = 404;
                    return response;
                }

                if (purchaseOrder.PurchaseOrderStatusId != completedStatus.Id)
                {
                    response.IsSuccess = false;
                    response.Message = "Purchase order is not completed";
                    response.StatusCode = 400;
                    return response;
                }

                // validate that all PurchaseOrderItems are included in the request
                var requestedItemIds = request.ReceiptItems.Select(ri => ri.PurchaseOrderItemId).ToList();
                var purchaseOrderItemIds = purchaseOrder.PurchaseOrderItems.Select(poi => poi.Id).ToList();

                // check if there are any missing items in the request
                var missingItems = purchaseOrderItemIds.Except(requestedItemIds).ToList();
                if (missingItems.Any())
                {
                    response.IsSuccess = false;
                    response.Message = "Receipt must contain all PurchaseOrderItems.";
                    response.StatusCode = 400;
                    return response;
                }

                // create receipt
                var receipt = new Receipt
                {
                    Id = Guid.NewGuid(),
                    PurchaseOrderId = request.PurchaseOrderId,
                    ReceivedDate = request.ReceivedDate
                };

                foreach (var item in request.ReceiptItems)
                {
                    var purchaseOrderItem = await _context.PurchaseOrderItems.FindAsync(item.PurchaseOrderItemId);
                    if (purchaseOrderItem == null)
                    {
                        response.IsSuccess = false;
                        response.Message = "Invalid purchase order item";
                        response.StatusCode = 400;
                        return response;
                    }
                    if (item.Quantity <= 0)
                    {
                        response.IsSuccess = false;
                        response.Message = $"quantity must be greater than zero.";
                        response.StatusCode = 400;
                        return response;
                    }
                    if (item.Quantity > purchaseOrderItem.Quantity) 
                    {
                        response.IsSuccess = false;
                        response.Message = $"Received quantity {item.Quantity} exceeds ordered quantity {purchaseOrderItem.Quantity} for item {purchaseOrderItem.Item.Name}.";
                        response.StatusCode = 400;
                        return response;
                    }

                    var receiptItem = new ReceiptItem
                    {
                        Id = Guid.NewGuid(),
                        ReceiptId = receipt.Id,
                        PurchaseOrderItemId = item.PurchaseOrderItemId,
                        Quantity = item.Quantity
                    };

                    _context.ReceiptItems.Add(receiptItem);

                    // fetch the item entity to increae the quantity
                    var itemEntity = await _context.Items.FindAsync(purchaseOrderItem.ItemId);
                    if (itemEntity != null)
                    {
                        itemEntity.Quantity += item.Quantity;
                    }


                    // update item ledger
                    var itemLedger = new ItemLedger
                    {
                        Id = Guid.NewGuid(),
                        Quantity = item.Quantity,
                        ReceiptItemId = receiptItem.Id,
                        Date = DateTime.UtcNow
                    };

                    _context.ItemLedgers.Add(itemLedger);
                }

                _context.Receipts.Add(receipt);


                await _context.SaveChangesAsync();

                var receiptsResponse = new ReceiptModelResponse
                {
                    Id = receipt.Id,
                    PurchaseOrderId = receipt.PurchaseOrderId,
                    ReceivedDate = receipt.ReceivedDate,
                    ReceiptItems = receipt.ReceiptItems
            .Select(ri => new ReceiptItemModel
            {
                PurchaseOrderItemId = ri.Id,
                Quantity = ri.Quantity,
                // Map other necessary properties here
            }).ToList()
                };


                response.IsSuccess = true;
                response.Message = "Receipt created successfully";
                response.Data = receiptsResponse;
                response.StatusCode = 201;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = $"An error occurred while creating the receipt: {ex.Message}";
                response.StatusCode = 500;
            }

            return response;
        }

        public async Task<ResponseApi<ReceiptModelResponse>> GetReceiptById(Guid receiptId)
        {
            var response = new ResponseApi<ReceiptModelResponse>();

            try
            {
                var receipt = await _context.Receipts
                    .Include(r => r.ReceiptItems)
                    .FirstOrDefaultAsync(r => r.Id == receiptId);

                if (receipt == null)
                {
                    response.IsSuccess = false;
                    response.Message = "Receipt not found";
                    response.StatusCode = 404;
                    return response;
                }
                {
                    response.Data = new ReceiptModelResponse
                    {
                        Id = receipt.Id,
                        PurchaseOrderId = receipt.PurchaseOrderId,
                        ReceivedDate = receipt.ReceivedDate,
                        ReceiptItems = receipt.ReceiptItems.Select(ri => new ReceiptItemModel
                        {
                            PurchaseOrderItemId = ri.PurchaseOrderItemId,
                            Quantity = ri.Quantity
                        }).ToList()
                    };
                    response.Message = "Receipt retrieved successfully";
                    response.IsSuccess = true;
                    response.StatusCode = 200;
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = $"An error occurred while retrieving the receipt: {ex.Message}";
                response.StatusCode = 500;
            }

            return response;
        }
    }
}
