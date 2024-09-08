using System.ComponentModel.DataAnnotations;

namespace RetailSystem.Core.Entities;

public class SupplierStatus
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    public string MachineName { get; set; }

    [Required]
    public string DisplayName { get; set; }

    public ICollection<Supplier> Suppliers { get; set; }

}
