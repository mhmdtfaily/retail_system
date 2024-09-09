using RetailSystem.Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetailSystem.Core.Models;

public class SupplierModel
{
        public Guid? Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public Guid supplier_status_id { get; set; }

}
public class SupplierDto : SupplierModel
{
    public bool IsActive { get; set; }
}
