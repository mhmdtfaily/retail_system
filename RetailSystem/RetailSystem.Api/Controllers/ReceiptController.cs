using Microsoft.AspNetCore.Mvc;
using RetailSystem.Application.Interfaces;
using RetailSystem.Core.Models;
using System;
using System.Threading.Tasks;

namespace RetailSystem.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReceiptController : ControllerBase
    {
        private readonly IReceiptRepository _receiptService;

        public ReceiptController(IReceiptRepository receiptService)
        {
            _receiptService = receiptService;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateReceipt([FromBody] CreatePurchaseReceiptRequest request)
        {
            var result = await _receiptService.CreateReceipt(request);

            return StatusCode(result.StatusCode, result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetReceiptById(Guid id)
        {
            var result = await _receiptService.GetReceiptById(id);

            return StatusCode(result.StatusCode, result);
        }
    }
}
