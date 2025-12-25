using Microsoft.AspNetCore.Mvc;
using BAL;
using DryCleanShopApi.DAL.Models;

namespace DryCleanShopApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        // إضافة عميل جديد
        [HttpPost]
        public IActionResult AddCustomer(clsCustomer customer, int createdBy)
        {
            try
            {
                var newId = BALCustomer.AddCustomer(customer, createdBy);
                return Ok(new { CustomerID = newId });
            }
            catch (Exception ex)
            {
                return BadRequest($"Error while adding customer: {ex.Message}");
            }
        }

        // تعديل بيانات عميل
        [HttpPut("{id}")]
        public IActionResult UpdateCustomer(int id, clsCustomer customer, int updatedBy)
        {
            try
            {
                customer.CustomerID = id;
                var success = BALCustomer.UpdateCustomer(customer, updatedBy);

                if (success)
                    return Ok("Customer updated successfully");
                else
                    return NotFound($"Customer with ID {id} not found");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error while updating customer: {ex.Message}");
            }
        }

        // الحصول على كل العملاء
        [HttpGet]
        public IActionResult GetAllCustomers()
        {
            try
            {
                var customers = BALCustomer.GetAllCustomers();
                return Ok(customers);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error while fetching customers: {ex.Message}");
            }
        }

        // الحصول على عميل واحد بالـ ID
        [HttpGet("{id}")]
        public IActionResult GetCustomerByID(int id)
        {
            try
            {
                var customer = BALCustomer.GetCustomerByID(id);
                if (customer == null)
                    return NotFound($"Customer with ID {id} not found");

                return Ok(customer);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error while fetching customer: {ex.Message}");
            }
        }

        // البحث عن عميل بالاسم
        [HttpGet("search")]
        public IActionResult GetCustomerByName( string name)
        {
            try
            {
                var customers = BALCustomer.GetCustomerByName(name);
                return Ok(customers);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error while searching customers: {ex.Message}");
            }
        }
    }
}
