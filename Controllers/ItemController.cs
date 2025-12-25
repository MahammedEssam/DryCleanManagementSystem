using Microsoft.AspNetCore.Mvc;
using BAL;
using DryCleanShopApi.DAL.Models;

namespace DryCleanShopApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemController : ControllerBase
    {
        // إضافة صنف جديد
        [HttpPost]
        public IActionResult AddItem(clsItem item)
        {
            try
            {
                // تحقق من أن الاسم مش موجود
                if (BALItem.IsItemNameExists(item.ItemName))
                    return BadRequest("Item name already exists");

                var newId = BALItem.AddItem(item);
                return Ok(new { ItemID = newId });
            }
            catch (Exception ex)
            {
                return BadRequest($"Error while adding item: {ex.Message}");
            }
        }

        // الحصول على كل الأصناف
        [HttpGet]
        public IActionResult GetAllItems()
        {
            try
            {
                var items = BALItem.GetAllItems();
                return Ok(items);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error while fetching items: {ex.Message}");
            }
        }

        // التحقق من وجود اسم صنف
        [HttpGet("exists")]
        public IActionResult IsItemNameExists(string itemName)
        {
            try
            {
                var exists = BALItem.IsItemNameExists(itemName);
                return Ok(new { ItemName = itemName, Exists = exists });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error while checking item name: {ex.Message}");
            }
        }
    }
}
