using Ambev.DeveloperEvaluation.Domain.DTOs;
using Ambev.DeveloperEvaluation.Domain.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalesController(ISaleService service, ILogger<SalesController> logger) : ControllerBase
    {
        ISaleService _service = service;
        private readonly ILogger<SalesController> _logger = logger;

        [HttpGet("GetById")]
        public async Task<ActionResult> GetById(int id)
        {
            try
            {
                _logger.LogInformation("Received request to get sale by ID: {Id}", id);
                var sale = await _service.GetById(id);
                _logger.LogInformation("Successfully retrieved sale with ID: {Id}", id);
                return Ok(sale);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving sale by ID: {Id}", id);
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("Filter")]
        public async Task<ActionResult> Filter([FromQuery] DateTime? saleDate, [FromQuery] string? customer, [FromQuery] string? branch)
        {
            try
            {
                _logger.LogInformation("Filtering sales with parameters - Date: {Date}, Customer: {Customer}, Branch: {Branch}", saleDate, customer, branch);
                var sales = await _service.Filter(saleDate, customer, branch);
                _logger.LogInformation("Successfully retrieved filtered sales.");
                return Ok(sales);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error filtering sales.");
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("Create")]
        public async Task<ActionResult> Create(SaleDTO saleDTO)
        {
            try
            {
                _logger.LogInformation("Received request to create a new sale for customer: {Customer}", saleDTO.Customer);
                var sale = await _service.Create(saleDTO);
                _logger.LogInformation("Successfully created sale with ID: {SaleId}", sale?.Id);
                return Ok(sale);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating sale for customer: {Customer}", saleDTO.Customer);
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("AddSale")]
        public async Task<ActionResult> AddSale([FromQuery] List<int> productsIDs, [FromQuery] string customer, [FromQuery] bool onlineSale)
        {
            try
            {
                _logger.LogInformation("Received request to add sale. Customer: {Customer}, OnlineSale: {OnlineSale}, Product IDs: {ProductIds}", customer, onlineSale, string.Join(",", productsIDs));
                var sale = await _service.AddSale(productsIDs, customer, onlineSale);
                _logger.LogInformation("Successfully added sale with ID: {SaleId}", sale?.Id);
                return Ok(sale);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adding sale for customer: {Customer}", customer);
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("Update")]
        public async Task<ActionResult> Update(SaleDTO saleDTO)
        {
            try
            {
                _logger.LogInformation("Received request to update sale with ID: {SaleId}", saleDTO.Id);
                await _service.Update(saleDTO);
                _logger.LogInformation("Successfully updated sale with ID: {SaleId}", saleDTO.Id);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating sale with ID: {SaleId}", saleDTO.Id);
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("AlterSaleStatus")]
        public async Task<ActionResult> AlterSaleStatus(int id)
        {
            try
            {
                _logger.LogInformation("Received request to alter status of sale with ID: {Id}", id);
                var sale = await _service.AlterSaleStatus(id);
                _logger.LogInformation("Successfully changed status of sale with ID: {Id}", id);
                return Ok(sale);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error changing status of sale with ID: {Id}", id);
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("Delete")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                _logger.LogInformation("Received request to delete sale with ID: {Id}", id);
                await _service.Delete(id);
                _logger.LogInformation("Successfully deleted sale with ID: {Id}", id);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting sale with ID: {Id}", id);
                return BadRequest(ex.Message);
            }
        }
    }
}
