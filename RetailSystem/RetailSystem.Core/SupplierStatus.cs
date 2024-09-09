using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace RetailSystem.Core.Entities
{
    public class SupplierStatus
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string MachineName { get; set; }

        [Required]
        [MaxLength(100)]
        public string DisplayName { get; set; }

        // JsonIgnore to prevent circular reference
        [JsonIgnore]
        public ICollection<Supplier>? Suppliers { get; set; }
    }
}
