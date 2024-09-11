using Microsoft.EntityFrameworkCore;
using RetailSystem.Api.Models;
using RetailSystem.Application.Interfaces;
using RetailSystem.Core;
using RetailSystem.Core.RequestModel;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace RetailSystem.Infrastructure.Repositories
{
    public class SupplierRepository : ISupplierRepository
    {
        private readonly RetailDbContext _context;

        public SupplierRepository(RetailDbContext context)
        {
            _context = context;
        }

        public async Task<ResponseApi<SupplierModel>> CreateSupplier(CreateSupplierModel supplierModel)
        {
            var response = new ResponseApi<SupplierModel>();
            try
            {
                // Fetch the status ID for the active state
                var activeStatus = await _context.SupplierStatuses
                                        .Where(s => s.MachineName == "active")
                                        .FirstOrDefaultAsync();
                if (activeStatus == null)
                {
                    response.IsSuccess = false;
                    response.Message = "Supplier status not found.";
                    response.StatusCode = 404;
                    return response;
                }

                // Check for duplicate suppliers based on email or phone
                var existingSupplier = await _context.Suppliers
                    .FirstOrDefaultAsync(s => s.Email == supplierModel.Email || s.Phone == supplierModel.Phone);

                if (existingSupplier != null)
                {
                    response.IsSuccess = false;
                    response.Message = "A supplier with the same email or phone already exists.";
                    response.StatusCode = 409; // Conflict
                    return response;
                }
                // Map the created Supplier entity to SupplierModel
                var supplier = new Supplier
                {
                    Name = supplierModel.Name,
                    Address = supplierModel.Address,
                    Email = supplierModel.Email,
                    Phone = supplierModel.Phone,
                    supplier_status_id = activeStatus.Id
                };
                _context.Suppliers.Add(supplier);
                await _context.SaveChangesAsync();

            

                response.Data = new SupplierModel
                    {
                        Id = supplier.Id,
                        Name = supplier.Name,
                        Address = supplier.Address,
                        Email = supplier.Email,
                        Phone = supplier.Phone,
                    };;
                response.Message = "Supplier created successfully.";
                response.StatusCode = 201;
                response.IsSuccess = true;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = $"An error occurred: {ex.Message}";
                response.StatusCode = 500;
            }
            return response;
        }

        public async Task<ResponseApi<SupplierModel>> GetSupplierById(Guid id)
        {
            var response = new ResponseApi<SupplierModel>();
            try
            {
                var supplier = await _context.Suppliers.Include(s => s.Status)
                                                       .FirstOrDefaultAsync(s => s.Id == id);
                if (supplier == null)
                {
                    response.IsSuccess = false;
                    response.Message = "Supplier not found.";
                    response.StatusCode = 404;
                }
                else
                {
                    // Map Supplier entity to SupplierModel
                    response.Data = new SupplierModel
                    {
                        Id = supplier.Id,
                        Name = supplier.Name,
                        Address = supplier.Address,
                        Email = supplier.Email,
                        Phone = supplier.Phone
                    };
                    response.IsSuccess = true;
                    response.StatusCode = 200;
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = $"An error occurred: {ex.Message}";
                response.StatusCode = 500;
            }
            return response;
        }
        public async Task<ResponseApi<SupplierModel>> UpdateSupplier(UpdateSupplierModel updateSupplierModel)
        {
            var response = new ResponseApi<SupplierModel>();

            try
            {
                // Fetch the existing supplier
                var existingSupplier = await _context.Suppliers.FindAsync(updateSupplierModel.Id);
                if (existingSupplier == null)
                {
                    return new ResponseApi<SupplierModel>
                    {
                        IsSuccess = false,
                        Message = "Supplier not found.",
                        StatusCode = 404
                    };
                }

                // Update existing supplier's properties
                existingSupplier.Name = updateSupplierModel.Name;
                existingSupplier.Address = updateSupplierModel.Address;
                existingSupplier.Email = updateSupplierModel.Email;
                existingSupplier.Phone = updateSupplierModel.Phone;

                // Save changes
                await _context.SaveChangesAsync();

                // Map the updated supplier to SupplierModel
                var updatedSupplierModel = new SupplierModel
                {
                    Id = existingSupplier.Id,
                    Name = existingSupplier.Name,
                    Address = existingSupplier.Address,
                    Email = existingSupplier.Email,
                    Phone = existingSupplier.Phone
                };

                response.IsSuccess = true;
                response.Data = updatedSupplierModel;
                response.Message = "Supplier updated successfully.";
                response.StatusCode = 200;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = $"An error occurred: {ex.Message}";
                response.StatusCode = 500;
            }

            return response;
        }

        public async Task<ResponseApi<object>> DeleteSupplier(Guid id)
        {
            var response = new ResponseApi<object>();
            try
            {
                // Retrieve the supplier by id
                var supplier = await _context.Suppliers.FindAsync(id);
                if (supplier != null)
                {
                    // Fetch the status ID for the inactive state
                    var inactiveStatus = await _context.SupplierStatuses
                                            .Where(s => s.MachineName == "in_active")
                                            .FirstOrDefaultAsync();

                    if (inactiveStatus != null)
                    {
                        // Update the supplier's status to inactive
                        supplier.supplier_status_id = inactiveStatus.Id;
                        _context.Suppliers.Update(supplier);
                        await _context.SaveChangesAsync();

                        response.Message = "Supplier marked as inactive successfully.";
                        response.Data = supplier;
                        response.StatusCode = 200;
                    }
                    else
                    {
                        response.IsSuccess = false;
                        response.Message = "Inactive status not found.";
                        response.StatusCode = 404;
                    }
                }
                else
                {
                    response.IsSuccess = false;
                    response.Message = "Supplier not found.";
                    response.StatusCode = 404;
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = $"An error occurred: {ex.Message}";
                response.StatusCode = 500;
            }
            return response;
        }

        public async Task<ResponseApi<IEnumerable<SupplierDto>>> GetAllSuppliers()
        {
            var response = new ResponseApi<IEnumerable<SupplierDto>>();
            try
            {
                // Retrieve the active status
                var activeStatus = await _context.SupplierStatuses
                                                 .Where(s => s.MachineName == "active")
                                                 .FirstOrDefaultAsync();

                // Fetch all suppliers
                var suppliers = await _context.Suppliers.Include(s => s.Status).ToListAsync();

                // Map suppliers to SupplierDto and set IsActive
                response.Data = suppliers.Select(s => new SupplierDto
                {
                    Id = s.Id,
                    Name = s.Name,
                    Address = s.Address,
                    Email = s.Email,
                    Phone = s.Phone,
                    IsActive = (s.Status != null && s.Status.MachineName == "active")
                }).ToList();

                response.IsSuccess = true;
                response.Message = "Suppliers retrieved successfully.";
                response.StatusCode = 200;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = $"An error occurred: {ex.Message}";
                response.StatusCode = 500;
            }

            return response;
        }
    }

}
