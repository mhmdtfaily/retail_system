using Microsoft.AspNetCore.Mvc;
using RetailSystem.Core.Models;
using RetailSystem.Application.Interfaces;
using RetailSystem.Core.RequestModel;
namespace RetailSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PurchaseOrdersController : ControllerBase
    {
        private readonly IPurchaseOrderRepository _purchaseOrderRepository;

        public PurchaseOrdersController(IPurchaseOrderRepository purchaseOrderRepository)
        {
            _purchaseOrderRepository = purchaseOrderRepository;
        }

        [HttpPost]
        public async Task<IActionResult> CreatePurchaseOrder([FromBody] CreatePurchaseOrderRequest request)
        {
            if (request == null || request.OrderItems == null || request.OrderItems.Count == 0)
            {
                return BadRequest(new ResponseApi<object>
                {
                    IsSuccess = false,
                    Message = "Invalid request.",
                    StatusCode = 400
                });
            }

            var response = await _purchaseOrderRepository.CreatePurchaseOrder(request.SupplierId, request.OrderItems);
            if (!response.IsSuccess)
            {
                return StatusCode(response.StatusCode, response);
            }

            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPurchaseOrderById(Guid id)
        {
            var response = await _purchaseOrderRepository.GetPurchaseOrderById(id);
            if (!response.IsSuccess)
            {
                return StatusCode(response.StatusCode, response);
            }

            return Ok(response);
        }

        [HttpPut("change-status")]
        public async Task<IActionResult> ChangePurchaseOrderStatus([FromBody] ChangePurchaseOrderStatusRequest request)
        {
            var response = await _purchaseOrderRepository.UpdatePurchaseOrderStatus(request.PurchaseOrderId, request.PurchaseOrderStatusId);

            if (!response.IsSuccess)
            {
                return StatusCode(response.StatusCode, response);
            }

            return Ok(response);
        }
    }
}
