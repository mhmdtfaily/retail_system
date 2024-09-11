using RetailSystem.Core.Models;
using RetailSystem.Core;
using RetailSystem.Core.RequestModel;

namespace RetailSystem.Application.Interfaces
{
    public interface ISupplierRepository
    {
        Task<ResponseApi<SupplierModel>> CreateSupplier(CreateSupplierModel supplierModel);
        Task<ResponseApi<SupplierModel>> GetSupplierById(Guid id);
        Task<ResponseApi<SupplierModel>> UpdateSupplier(UpdateSupplierModel supplierModel);
        Task<ResponseApi<object>> DeleteSupplier(Guid id);
        Task<ResponseApi<IEnumerable<SupplierDto>>> GetAllSuppliers();
    }
}
