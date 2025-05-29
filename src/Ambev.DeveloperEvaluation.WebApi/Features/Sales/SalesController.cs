using Ambev.DeveloperEvaluation.Domain.DTOs;
using Ambev.DeveloperEvaluation.Domain.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalesController(ISaleService service) : ControllerBase
    {
        ISaleService _service = service;

        [HttpGet("GetById")]
        public async Task<ActionResult> GetById(int id)
        {
            try
            {
                var sale = await _service.GetById(id);
                return Ok(sale);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("Filter")]
        public async Task<ActionResult> Filter([FromQuery] DateTime? saleDate, [FromQuery] string? customer, [FromQuery] string? branch)
        {
            try
            {
                var sales = await _service.Filter(saleDate, customer, branch);
                return Ok(sales);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("Create")]
        public async Task<ActionResult> Create(SaleDTO saleDTO)
        {
            try
            {
                var sale = await _service.Create(saleDTO);
                return Ok(sale);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("AddSale")]
        public async Task<ActionResult> AddSale([FromQuery] List<int> productsIDs, [FromQuery] string customer, [FromQuery] bool onlineSale)
        {
            try
            {
                var sale = await _service.AddSale(productsIDs, customer, onlineSale);
                return Ok(sale);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("Update")]
        public async Task<ActionResult> Update(SaleDTO saleDTO)
        {
            try
            {
                await _service.Update(saleDTO);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("AlterSaleStatus")]
        public async Task<ActionResult> AlterSaleStatus(int id)
        {
            try
            {
                var sale = await _service.AlterSaleStatus(id);
                return Ok(sale);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("Delete")]
        public async Task<ActionResult> Delete(int id) 
        {
            try
            {
                await _service.Delete(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
