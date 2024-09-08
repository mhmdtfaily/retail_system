namespace RetailSystem.Core;

using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using RetailSystem.Core.Entities;


public class Supplier
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    public string Name { get; set; }

    [Required]
    public string Address { get; set; }
    
    [Required]
    public string Email { get; set; }

    [Required]
    public string Phone { get; set; }

    [ForeignKey("Status")]
    public Guid supplier_status_id { get; set; }
    public SupplierStatus Status { get; set; }

    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public DateTime CreatedAt { get; set; }

    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    public DateTime UpdatedAt { get; set; }
}