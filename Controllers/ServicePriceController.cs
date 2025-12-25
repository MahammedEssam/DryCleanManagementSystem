using Microsoft.AspNetCore.Mvc;
using BAL;
using DryCleanShopApi.DAL.Models;

namespace DryCleanShopApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServicePriceController : ControllerBase
    {
        // إضافة سعر خدمة جديدة
        [HttpPost]
        public IActionResult AddServicePrice(clsServicePrice price)
        {
            try
            {
                BALServicePrice.AddServicePrice(price);
                return Ok("Service price added successfully");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error while adding service price: {ex.Message}");
            }
        }

        // تعديل سعر خدمة
        [HttpPut("{id}")]
        public IActionResult UpdateServicePrice(int id, clsServicePrice price, int createdBy)
        {
            try
            {
                price.ServicePriceID = id;
                var success = BALServicePrice.UpdateServicePrice(price, createdBy);

                if (success)
                    return Ok("Service price updated successfully");
                else
                    return NotFound($"Service price with ID {id} not found");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error while updating service price: {ex.Message}");
            }
        }

        // الحصول على سعر خدمة معينة بناءً على Item و Service
        [HttpGet("get")]
        public IActionResult GetServicePrice(int itemId, int serviceId)
        {
            try
            {
                var price = BALServicePrice.GetServicePrice(itemId, serviceId);
                return Ok(new { ItemID = itemId, ServiceID = serviceId, Price = price });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error while fetching service price: {ex.Message}");
            }
        }
    }
}
