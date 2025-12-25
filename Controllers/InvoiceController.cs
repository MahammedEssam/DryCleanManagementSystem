using Microsoft.AspNetCore.Mvc;
using BAL;
using DryCleanShopApi.DAL.Models;

namespace DryCleanShopApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoiceController : ControllerBase
    {
        // إضافة فاتورة جديدة
        [HttpPost]
        public IActionResult AddInvoice(clsInvoice invoice,  int createdBy)
        {
            try
            {
                var newId = BALInvoice.AddInvoice(invoice, createdBy);
                return Ok(new { InvoiceID = newId });
            }
            catch (Exception ex)
            {
                return BadRequest($"Error while adding invoice: {ex.Message}");
            }
        }

        // الحصول على فاتورة واحدة بالـ ID
        [HttpGet("{id}")]
        public IActionResult GetInvoiceByID(int id)
        {
            try
            {
                var invoice = BALInvoice.GetInvoiceByID(id);
                if (invoice == null)
                    return NotFound($"Invoice with ID {id} not found");

                return Ok(invoice);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error while fetching invoice: {ex.Message}");
            }
        }

        // الحصول على إجمالي المدفوع في يوم معين
        [HttpGet("total/day")]
        public IActionResult GetTotalPaidByDay( DateTime targetDate)
        {
            try
            {
                var total = BALInvoice.GetTotalPaidByDay(targetDate);
                return Ok(new { Date = targetDate.ToString("dd-MM-yyyy"), TotalPaid = total });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error while calculating total paid: {ex.Message}");
            }
        }
    }
}
