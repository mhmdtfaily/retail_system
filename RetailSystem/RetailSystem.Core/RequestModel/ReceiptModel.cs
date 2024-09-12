using RetailSystem.Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RetailSystem.Core.Models;

public class CreatePurchaseReceiptRequest
{
    public Guid PurchaseOrderId { get; set; }
    public DateTime ReceivedDate { get; set; }
    public List<ReceiptItemModel> ReceiptItems { get; set; }
}

public class ReceiptItemModel
{
    public Guid PurchaseOrderItemId { get; set; }
    public int Quantity { get; set; }
}

public class ReceiptItemModelResponse : ReceiptItemModel
{
    public decimal Price { get; set; }
}
public class ReceiptModelResponse : PurchaseReceiptResponse
{
    public Guid Id { get; set; }
}

public class PurchaseReceiptResponse
{
    public Guid PurchaseOrderId { get; set; }
    public DateTime ReceivedDate { get; set; }
    public List<ReceiptItemModelResponse> ReceiptItems { get; set; }
    public decimal TotalAmount { get; set; }
}
