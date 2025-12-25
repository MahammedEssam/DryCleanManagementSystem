using Microsoft.AspNetCore.Mvc;
using BAL;
using DryCleanShopApi.DAL.Models;

namespace DryCleanShopApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LogController : ControllerBase
    {
        // إضافة Log جديد
        [HttpPost]
        public async Task<IActionResult> AddLog(clsLog log)
        {
            try
            {
                await BALLog.AddLog(log);
                return Ok("Log added successfully");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error while adding log: {ex.Message}");
            }
        }

        // الحصول على كل الـ Logs
        [HttpGet]
        public IActionResult GetAllLogs()
        {
            try
            {
                var logs = BALLog.GetAllLogs();
                return Ok(logs);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error while fetching logs: {ex.Message}");
            }
        }
    }
}
