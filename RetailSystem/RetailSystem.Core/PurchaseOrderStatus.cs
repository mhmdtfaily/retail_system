using System.ComponentModel.DataAnnotations;

namespace RetailSystem.Core.Entities;

public class PurchaseOrderStatus
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    public string MachineName { get; set; }

    [Required]
    public string DisplayName { get; set; }

    public ICollection<PurchaseOrder> PurchaseOrders { get; set; }
}
