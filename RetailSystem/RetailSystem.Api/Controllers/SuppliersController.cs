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
            if (!ModelState.IsValid)
            {
                return BadRequest(new ResponseApi<object>
                {
                    IsSuccess = false,
                    Message = "Invalid model state",
                    StatusCode = 400
                });
            }

            var response = await _supplierRepository.CreateSupplier(supplier);
            return Ok( response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSupplierById(Guid id)
        {
            var response = await _supplierRepository.GetSupplierById(id);
            if (!response.IsSuccess)
            {
                return StatusCode(response.StatusCode, response);
            }

            return Ok(response);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateSupplier([FromBody] UpdateSupplierModel supplier)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ResponseApi<object>
                {
                    IsSuccess = false,
                    Message = "Invalid model state",
                    StatusCode = 400
                });
            }
            var response = await _supplierRepository.UpdateSupplier(supplier);
            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSupplier(Guid id)
        {
            var response = await _supplierRepository.DeleteSupplier(id);
            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllSuppliers()
        {
            var response = await _supplierRepository.GetAllSuppliers();
            if (!response.IsSuccess)
            {
                return StatusCode(response.StatusCode, response.Message);
            }
            return Ok(response);
        }

    }
}
