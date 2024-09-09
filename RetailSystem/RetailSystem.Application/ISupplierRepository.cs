using RetailSystem.Api.Models;
using RetailSystem.Core;
using RetailSystem.Core.Models;

namespace RetailSystem.Application.Interfaces
{
    public interface ISupplierRepository
    {
        Task<ResponseApi<SupplierModel>> CreateSupplier(SupplierModel supplierModel);
        Task<ResponseApi<SupplierModel>> GetSupplierById(Guid id);
        Task<ResponseApi<SupplierModel>> UpdateSupplier(SupplierModel supplierModel);
        Task<ResponseApi<object>> DeleteSupplier(Guid id);
        Task<ResponseApi<IEnumerable<SupplierDto>>> GetAllSuppliers();
    }
}
