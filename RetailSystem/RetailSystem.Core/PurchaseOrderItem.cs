using System.ComponentModel.DataAnnotations;

namespace RetailSystem.Core.Entities
{
    public class PurchaseOrderItem
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public Guid ItemId { get; set; }

        public Item Item { get; set; }

        [Required]
        public Guid PurchaseOrderId { get; set; }

        public PurchaseOrder PurchaseOrder { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        public decimal Price { get; set; }

        public ICollection<ReceiptItem> ReceiptItems { get; set; }
    }
}
