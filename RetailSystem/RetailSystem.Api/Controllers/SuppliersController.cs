using Microsoft.AspNetCore.Mvc;
using RetailSystem.Core.Models;
using RetailSystem.Application.Interfaces;
using RetailSystem.Core;
using RetailSystem.Core.RequestModel;
using System;
using System.Threading.Tasks;

namespace RetailSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuppliersController : ControllerBase
    {
        private readonly ISupplierRepository _supplierRepository;

        public SuppliersController(ISupplierRepository supplierRepository)
        {
            _supplierRepository = supplierRepository;
        }

        [HttpPost]
        public async Task<IActionResult> CreateSupplier([FromBody] CreateSupplierModel supplier)
        {
            var response = await _supplierRepository.CreateSupplier(supplier);
            return StatusCode(response.StatusCode,response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSupplierById(Guid id)
        {
            var response = await _supplierRepository.GetSupplierById(id);

            return StatusCode(response.StatusCode, response);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateSupplier([FromBody] UpdateSupplierModel supplier)
        {
            var response = await _supplierRepository.UpdateSupplier(supplier);
            return StatusCode(response.StatusCode, response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSupplier(Guid id)
        {
            var response = await _supplierRepository.DeleteSupplier(id);
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllSuppliers()
        {
            var response = await _supplierRepository.GetAllSuppliers();
            return StatusCode(response.StatusCode, response);
        }

    }
}
