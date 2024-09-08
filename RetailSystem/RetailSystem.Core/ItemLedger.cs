using System.ComponentModel.DataAnnotations;

namespace RetailSystem.Core.Entities
{
    public class ItemLedger
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public int Quantity { get; set; }

        public Guid? ReceiptItemId { get; set; }

        public ReceiptItem? ReceiptItem { get; set; }

        [Required]
        public DateTime Date { get; set; }
    }
}
