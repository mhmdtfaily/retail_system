using Microsoft.EntityFrameworkCore;
using RetailSystem.Application.Interfaces;
using RetailSystem.Core.Models;
using RetailSystem.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RetailSystem.Core.RequestModel;

namespace RetailSystem.Infrastructure.Repositories
{
    public class PurchaseOrderRepository : IPurchaseOrderRepository
    {
        private readonly RetailDbContext _context;

        public PurchaseOrderRepository(RetailDbContext context)
        {
            _context = context;
        }

        public async Task<ResponseApi<PurchaseOrderModel>> CreatePurchaseOrder(Guid supplierId, List<GetPurchaseOrderItemModel> purchaseOrderitemList)
        {
            var response = new ResponseApi<PurchaseOrderModel>();
            try
            {
                // Check if the supplier exists
                var supplier = await _context.Suppliers.FindAsync(supplierId);
                if (supplier == null)
                {
                    response.IsSuccess = false;
                    response.Message = "Supplier not found.";
                    response.StatusCode = 404;
                    return response;
                }
                // Fetch the status for the inActive state
                var inactiveStatus = await _context.SupplierStatuses
                                        .Where(s => s.MachineName == "in_active")
                                        .FirstOrDefaultAsync();

                if (inactiveStatus==null)
                {
                    response.IsSuccess = false;
                    response.Message = "supplier status not found.";
                    response.StatusCode = 404;
                    return response;
                }
                if (supplier.supplier_status_id == inactiveStatus.Id)
                {
                    response.IsSuccess = false;
                    response.Message = "Supplier does not exists.";
                    response.StatusCode = 404;
                    return response;
                }
                // Fetch the status for "pending" Purchase Order
                var pendingStatus = await _context.PurchaseOrderStatuses
                    .FirstOrDefaultAsync(s => s.MachineName == "pending");
                if (pendingStatus == null)
                {
                    response.IsSuccess = false;
                    response.Message = "Pending status not found.";
                    response.StatusCode = 404;
                    return response;
                }

                // Create Purchase Order
                var purchaseOrder = new PurchaseOrder
                {
                    Id = Guid.NewGuid(),
                    SupplierId = supplierId,
                    OrderDate = DateTime.UtcNow,
                    PurchaseOrderStatusId = pendingStatus.Id,
                    PurchaseOrderItems = new List<PurchaseOrderItem>()
                };

                decimal totalAmount = 0;

                // Process each item
                foreach (var item in purchaseOrderitemList)
                {
                    var existingItem = await _context.Items.FindAsync(item.ItemId);

                    if (existingItem == null)
                    {
                        response.IsSuccess = false;
                        response.Message = $"Item with id :{item.ItemId} not found.";
                        response.StatusCode = 404;
                        return response;
                    }

                    if (item.Quantity <= 0)
                    {
                        response.IsSuccess = false;
                        response.Message = $"quantity must be greater than zero {existingItem.Name}.";
                        response.StatusCode = 400;
                        return response;
                    }


                    // add PurchaseOrderItem
                    var purchaseOrderItem = new PurchaseOrderItem
                    {
                        Id = Guid.NewGuid(),
                        ItemId = item.ItemId,
                        PurchaseOrderId = purchaseOrder.Id,
                        Quantity = item.Quantity,
                        Price = item.Price
                    };

                    purchaseOrder.PurchaseOrderItems.Add(purchaseOrderItem);

                    // Calculate total amount
                    totalAmount += item.Quantity * item.Price;
                }

                _context.PurchaseOrders.Add(purchaseOrder);
                await _context.SaveChangesAsync();

                response.Data = new PurchaseOrderModel
                {
                    Id = purchaseOrder.Id,
                    SupplierId = purchaseOrder.SupplierId,
                    OrderDate = purchaseOrder.OrderDate,
                    PurchaseOrderItems = purchaseOrder.PurchaseOrderItems.Select(
                        poi => new PurchaseOrderItemModel
                        {
                            ItemId= poi.ItemId,ItemName=poi.Item.Name,
                            Price=poi.Price,Quantity=poi.Quantity
                        }).ToList(),
                    PurchaseOrderStatusId = purchaseOrder.PurchaseOrderStatusId,
                    TotalAmount = totalAmount
                };
                response.Message = "Purchase Order created successfully.";
                response.StatusCode = 201;
                response.IsSuccess = true;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = $"An error occurred: {ex.Message}";
                response.StatusCode = 500;
            }

            return response;
        }

        public async Task<ResponseApi<PurchaseOrderModel>> GetPurchaseOrderById(Guid id)
        {
            var response = new ResponseApi<PurchaseOrderModel>();
            try
            {
                var purchaseOrder = await _context.PurchaseOrders
                    .Include(po => po.Supplier)
                    .Include(po => po.PurchaseOrderItems)
                        .ThenInclude(poi => poi.Item)
                    .Include(po => po.PurchaseOrderStatus)
                    .FirstOrDefaultAsync(po => po.Id == id);

                if (purchaseOrder == null)
                {
                    response.IsSuccess = false;
                    response.Message = "Purchase Order not found.";
                    response.StatusCode = 404;
                }
                else
                {
                    response.Data = new PurchaseOrderModel
                    {
                        Id = purchaseOrder.Id,
                        SupplierId = purchaseOrder.SupplierId,
                        OrderDate = purchaseOrder.OrderDate,
                        PurchaseOrderStatusId = purchaseOrder.PurchaseOrderStatusId,
                        TotalAmount = purchaseOrder.PurchaseOrderItems.Sum(poi => poi.Quantity * poi.Price),
                        PurchaseOrderItems = purchaseOrder.PurchaseOrderItems.Select(poi => new PurchaseOrderItemModel
                        {
                            ItemId = poi.ItemId,
                            ItemName = poi.Item.Name,
                            Quantity = poi.Quantity,
                            Price = poi.Price,
                        }).ToList()
                    };
                    response.Message = "Purchase order retrieve successfully";
                    response.IsSuccess = true;
                    response.StatusCode = 200;
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = $"An error occurred: {ex.Message}";
                response.StatusCode = 500;
            }

            return response;
        }

        public async Task<ResponseApi<bool>> UpdatePurchaseOrderStatus(Guid purchaseOrderId, string purchaseOrderStatus)
        {
            var response = new ResponseApi<bool>();
            try
            {
                // Check if the PurchaseOrderStatus exists
                var statusExists = await _context.PurchaseOrderStatuses.FirstOrDefaultAsync(s => s.MachineName == purchaseOrderStatus);

                if (statusExists==null)
                {
                    response.IsSuccess = false;
                    response.Message = "Invalid Purchase Order Status.";
                    response.StatusCode = 400;
                    return response;
                }

                // Find the PurchaseOrder
                var purchaseOrder = await _context.PurchaseOrders.FirstOrDefaultAsync(po => po.Id == purchaseOrderId);
                if (purchaseOrder == null)
                {
                    response.IsSuccess = false;
                    response.Message = "Purchase Order not found.";
                    response.StatusCode = 404;
                    return response;
                }

                // Update the status
                purchaseOrder.PurchaseOrderStatusId = statusExists.Id;
                _context.PurchaseOrders.Update(purchaseOrder);
                await _context.SaveChangesAsync();

                response.Data = true;
                response.IsSuccess = true;
                response.Message = "PurchaseOrder status change successfully";
                response.StatusCode = 200;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = $"An error occurred: {ex.Message}";
                response.StatusCode = 500;
            }

            return response;
        }
    }
}
