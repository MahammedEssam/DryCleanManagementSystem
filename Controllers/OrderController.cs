using Microsoft.AspNetCore.Mvc;
using BAL;
using DryCleanShopApi.DAL.Models;

namespace DryCleanShopApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        // إضافة أوردر جديد
        [HttpPost]
        public IActionResult AddOrder(clsOrder order)
        {
            try
            {
                var newId = BALOrder.AddOrder(order);
                return Ok(new { OrderID = newId });
            }
            catch (Exception ex)
            {
                return BadRequest($"Error while adding order: {ex.Message}");
            }
        }

        // تعديل حالة الأوردر
        [HttpPut("{id}/status")]
        public IActionResult UpdateOrderStatus(int id,  int status,  int updatedBy)
        {
            try
            {
                var success = BALOrder.UpdateOrderStatus(id, status, updatedBy);
                if (success)
                    return Ok("Order status updated successfully");
                else
                    return NotFound($"Order with ID {id} not found");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error while updating order status: {ex.Message}");
            }
        }

        // تعديل المبلغ الكلي للأوردر
        [HttpPut("{id}/amount")]
        public IActionResult UpdateOrderAmount(int id,  decimal totalAmount,  int updatedBy)
        {
            try
            {
                var success = BALOrder.UpdateOrderAmount(id, totalAmount, updatedBy);
                if (success)
                    return Ok("Order amount updated successfully");
                else
                    return NotFound($"Order with ID {id} not found");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error while updating order amount: {ex.Message}");
            }
        }

        // الحصول على أوردر واحد بالـ ID
        [HttpGet("{id}")]
        public IActionResult GetOrderByID(int id)
        {
            try
            {
                var order = BALOrder.GetOrderByID(id);
                if (order == null)
                    return NotFound($"Order with ID {id} not found");

                return Ok(order);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error while fetching order: {ex.Message}");
            }
        }

        // الحصول على كل الأوردرات
        [HttpGet]
        public IActionResult GetAllOrders()
        {
            try
            {
                var orders = BALOrder.GetAllOrders();
                return Ok(orders);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error while fetching orders: {ex.Message}");
            }
        }

        // الحصول على أوردرات عميل معين
        [HttpGet("customer/{customerId}")]
        public IActionResult GetOrdersByCustomerID(int customerId)
        {
            try
            {
                var orders = BALOrder.GetOrdersByCustomerID(customerId);
                return Ok(orders);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error while fetching orders for customer: {ex.Message}");
            }
        }
    }
}
