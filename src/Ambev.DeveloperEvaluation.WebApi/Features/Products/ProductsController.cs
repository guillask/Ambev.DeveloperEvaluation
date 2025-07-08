using Ambev.DeveloperEvaluation.Domain.DTOs;
using Ambev.DeveloperEvaluation.Domain.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController(IProductService service, ILogger<ProductsController> logger) : ControllerBase
    {
        private readonly IProductService _service = service;
        private readonly ILogger<ProductsController> _logger = logger;

        [HttpGet("GetById")]
        public async Task<ActionResult> GetById(int id)
        {
            try
            {
                _logger.LogInformation("Received request to get product by ID: {Id}", id);
                var product = await _service.GetById(id);
                _logger.LogInformation("Successfully retrieved product with ID: {Id}", id);
                return Ok(product);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving product by ID: {Id}", id);
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("Create")]
        public async Task<ActionResult> Create(ProductDTO productDTO)
        {
            try
            {
                _logger.LogInformation("Received request to create a new product: {Name}", productDTO?.Name);
                var product = await _service.Create(productDTO);
                _logger.LogInformation("Successfully created product with ID: {ProductId}", product?.Id);
                return Ok(product);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating product: {Name}", productDTO?.Name);
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("Update")]
        public async Task<ActionResult> Update(ProductDTO productDTO)
        {
            try
            {
                _logger.LogInformation("Received request to update product with ID: {Id}", productDTO?.Id);
                await _service.Update(productDTO);
                _logger.LogInformation("Successfully updated product with ID: {Id}", productDTO?.Id);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating product with ID: {Id}", productDTO?.Id);
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("Delete")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                _logger.LogInformation("Received request to delete product with ID: {Id}", id);
                await _service.Delete(id);
                _logger.LogInformation("Successfully deleted product with ID: {Id}", id);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting product with ID: {Id}", id);
                return BadRequest(ex.Message);
            }
        }
    }
}
