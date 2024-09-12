using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetailSystem.Core.RequestModel;

    public class CreatePurchaseOrderRequest
    {
        public Guid SupplierId { get; set; }
        public List<GetPurchaseOrderItemModel> OrderItems { get; set; }
    }

    public class PurchaseOrderItemModel
    {
        public Guid ItemId { get; set; }
        public string ItemName { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
    public class GetPurchaseOrderItemModel
    {
        public Guid ItemId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }

public class PurchaseOrderModel
    {
        public Guid Id { get; set; }
        public Guid SupplierId { get; set; }
        public DateTime OrderDate { get; set; }
        public Guid PurchaseOrderStatusId { get; set; }
        public List<PurchaseOrderItemModel> PurchaseOrderItems { get; set; }
        public decimal TotalAmount { get; set; }
    }
    public class ChangePurchaseOrderStatusRequest
    {
        public Guid PurchaseOrderId { get; set; }
        public string PurchaseOrderStatus { get; set; }
    }

