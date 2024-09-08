using System.ComponentModel.DataAnnotations;

namespace RetailSystem.Core.Entities;

public class Item
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    public string Name { get; set; }

    public string Description { get; set; }

    [Required]
    public int Quantity { get; set; }

    public ICollection<PurchaseOrderItem> PurchaseOrderItems { get; set; }
}
