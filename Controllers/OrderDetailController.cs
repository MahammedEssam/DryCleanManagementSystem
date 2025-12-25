using Microsoft.AspNetCore.Mvc;
using BAL;
using DryCleanShopApi.DAL.Models;

namespace DryCleanShopApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderDetailController : ControllerBase
    {
        // إضافة تفاصيل أوردر جديد
        [HttpPost]
        public IActionResult AddOrderDetail(clsOrderDetail detail, int createdBy)
        {
            try
            {
                var newId = BALOrderDetail.AddOrderDetail(detail, createdBy);
                return Ok(new { DetailID = newId });
            }
            catch (Exception ex)
            {
                return BadRequest($"Error while adding order detail: {ex.Message}");
            }
        }

        // تعديل تفاصيل أوردر
        [HttpPut("{id}")]
        public IActionResult UpdateOrderDetail(int id, clsOrderDetail detail, int updatedBy)
        {
            try
            {
                detail.DetailID = id;
                var success = BALOrderDetail.UpdateOrderDetail(detail, updatedBy);

                if (success)
                    return Ok("Order detail updated successfully");
                else
                    return NotFound($"Order detail with ID {id} not found");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error while updating order detail: {ex.Message}");
            }
        }

        // حذف تفاصيل أوردر
        [HttpDelete("{id}")]
        public IActionResult DeleteOrderDetail(int id, int deletedBy)
        {
            try
            {
                var success = BALOrderDetail.DeleteOrderDetail(id, deletedBy);
                if (success)
                    return Ok("Order detail deleted successfully");
                else
                    return NotFound($"Order detail with ID {id} not found");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error while deleting order detail: {ex.Message}");
            }
        }

        // الحصول على تفاصيل أوردر واحد بالـ ID
        [HttpGet("{id}")]
        public IActionResult GetOrderDetailByID(int id)
        {
            try
            {
                var detail = BALOrderDetail.GetOrderDetailByID(id);
                if (detail == null)
                    return NotFound($"Order detail with ID {id} not found");

                return Ok(detail);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error while fetching order detail: {ex.Message}");
            }
        }

        // الحصول على كل تفاصيل أوردر معين
        [HttpGet("order/{orderId}")]
        public IActionResult GetOrderDetailsByOrderID(int orderId)
        {
            try
            {
                var details = BALOrderDetail.GetOrderDetailsByOrderID(orderId);
                if (details == null)
                    return NotFound($"Order with ID {orderId} not found");

                return Ok(details);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error while fetching order details: {ex.Message}");
            }
        }

        // الحصول على إجمالي المبلغ لأوردر معين
        [HttpGet("order/{orderId}/total")]
        public IActionResult GetOrderTotalByOrderID(int orderId)
        {
            try
            {
                var total = BALOrderDetail.GetOrderTotalByOrderID(orderId);
                return Ok(new { OrderID = orderId, TotalAmount = total });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error while calculating order total: {ex.Message}");
            }
        }
    }
}
