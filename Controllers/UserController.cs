using Microsoft.AspNetCore.Mvc;
using BAL;
using DryCleanShopApi.DAL.Models;

namespace DryCleanShopApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        // إضافة مستخدم جديد
        [HttpPost]
        public IActionResult AddUser(clsUser user)
        {
            try
            {
                if (BALUser.CheckUsernameExists(user.Username))
                    return BadRequest("Username already exists");

                var newId = BALUser.AddUser(user);
                return Ok(new { UserID = newId });
            }
            catch (Exception ex)
            {
                return BadRequest($"Error while adding user: {ex.Message}");
            }
        }

        // تعديل بيانات مستخدم
        [HttpPut("{id}")]
        public IActionResult UpdateUser(int id, clsUser user, int updatedBy)
        {
            try
            {
                user.UserID = id;
                var success = BALUser.UpdateUser(user, updatedBy);

                if (success)
                    return Ok("User updated successfully");
                else
                    return NotFound($"User with ID {id} not found");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error while updating user: {ex.Message}");
            }
        }

        // حذف مستخدم
        [HttpDelete("{id}")]
        public IActionResult DeleteUser(int id, int deletedBy)
        {
            try
            {
                var success = BALUser.DeleteUser(id, deletedBy);
                if (success)
                    return Ok("User deleted successfully");
                else
                    return NotFound($"User with ID {id} not found");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error while deleting user: {ex.Message}");
            }
        }

        // الحصول على كل المستخدمين
        [HttpGet]
        public IActionResult GetAllUsers()
        {
            try
            {
                var users = BALUser.GetAllUsers();
                return Ok(users);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error while fetching users: {ex.Message}");
            }
        }

        // الحصول على مستخدم واحد بالـ ID
        [HttpGet("{id}")]
        public IActionResult GetUserByID(int id)
        {
            try
            {
                var user = BALUser.GetUserByID(id);
                if (user == null)
                    return NotFound($"User with ID {id} not found");

                return Ok(user);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error while fetching user: {ex.Message}");
            }
        }

        // التحقق من وجود اسم مستخدم
        [HttpGet("exists")]
        public IActionResult CheckUsernameExists(string username)
        {
            try
            {
                var exists = BALUser.CheckUsernameExists(username);
                return Ok(new { Username = username, Exists = exists });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error while checking username: {ex.Message}");
            }
        }

        // تسجيل دخول مستخدم
        [HttpPost("login")]
        public IActionResult LoginUser(string username, string passwordHash)
        {
            try
            {
                var user = BALUser.LoginUser(username, passwordHash);
                if (user == null)
                    return Unauthorized("Invalid username or password");

                return Ok(user);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error while logging in: {ex.Message}");
            }
        }
    }
}
