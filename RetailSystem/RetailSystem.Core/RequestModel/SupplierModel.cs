using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetailSystem.Core.RequestModel;

public class SupplierModel
{
    public Guid? Id { get; set; }
    public string Name { get; set; }
    public string Address { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }

}
public class CreateSupplierModel
{
    public string Name { get; set; }
    public string Address { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }

}
public class UpdateSupplierModel
{
    public Guid? Id { get; set; }
    public string Name { get; set; }
    public string Address { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }

}
public class SupplierDto : SupplierModel
{
    public bool IsActive { get; set; }
}
