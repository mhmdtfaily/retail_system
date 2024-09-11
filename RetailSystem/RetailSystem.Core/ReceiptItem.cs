using System.ComponentModel.DataAnnotations;

namespace RetailSystem.Core.Entities;

public class ReceiptItem
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    public Guid ReceiptId { get; set; }

    public Receipt Receipt { get; set; }

    public Guid PurchaseOrderItemId { get; set; }

    public PurchaseOrderItem? PurchaseOrderItem { get; set; }

    [Required]
    public int Quantity { get; set; }
}
