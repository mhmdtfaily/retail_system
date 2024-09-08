using System.ComponentModel.DataAnnotations;

namespace RetailSystem.Core.Entities
{
    public class Receipt
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public Guid PurchaseOrderId { get; set; }

        public PurchaseOrder PurchaseOrder { get; set; }

        [Required]
        public DateTime ReceivedDate { get; set; }

        public ICollection<ReceiptItem> ReceiptItems { get; set; }
    }
}
