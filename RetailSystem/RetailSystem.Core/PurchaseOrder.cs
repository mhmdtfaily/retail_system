using System.ComponentModel.DataAnnotations;

namespace RetailSystem.Core.Entities;

public class PurchaseOrder
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    public DateTime OrderDate { get; set; }

    [Required]
    public Guid PurchaseOrderStatusId { get; set; }

    public PurchaseOrderStatus PurchaseOrderStatus { get; set; }

    public ICollection<PurchaseOrderItem> PurchaseOrderItems { get; set; }
    public ICollection<Receipt> Receipts { get; set; }
}
