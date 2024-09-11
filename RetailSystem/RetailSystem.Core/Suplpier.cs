namespace RetailSystem.Core;

using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using RetailSystem.Core.Entities;


public class Supplier
{
    [Key]
    public Guid Id { get; set; }

    [Required(ErrorMessage = "Name is required")]
    public string Name { get; set; }

    [Required(ErrorMessage = "Address is required")]
    public string Address { get; set; }

    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Invalid email format")]
    public string Email { get; set; }

    [Required(ErrorMessage = "Phone is required")]
    [Phone(ErrorMessage = "Invalid phone format")]
    public string Phone { get; set; }

    [ForeignKey("Status")]
    [Required(ErrorMessage = "Supplier status is required")]
    public Guid supplier_status_id { get; set; }

    public SupplierStatus Status { get; set; }

}
