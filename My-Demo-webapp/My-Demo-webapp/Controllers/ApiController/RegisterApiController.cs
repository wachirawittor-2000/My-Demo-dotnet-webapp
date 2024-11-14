using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using My_Demo_webapp.Data;
using My_Demo_webapp.Entites;
using My_Demo_webapp.Models;

namespace My_Demo_webapp.Controllers.ApiController
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegisterApiController : Controller
    {
        private readonly WebappDbContext _db;
        private readonly ILogger<RegisterApiController> _logger;  // Logger to log errors

        public RegisterApiController(WebappDbContext db, ILogger<RegisterApiController> logger)
        {
            _db = db;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromBody] RegisterReqModel model)
        {
            var username = model.Username;
            var password = model.Password;
            var firstname = model.FirstName;
            var lastname = model.LastName;
            var email = model.Email;

            var respond = new ResponseModel();


            if (model == null || string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password) 
                || string.IsNullOrEmpty(firstname) || string.IsNullOrEmpty(lastname) || string.IsNullOrEmpty(email))
            {
                respond = new ResponseModel
                {
                    ErrorCode = "ERR04",
                    ErrorMessage = "Incomplete information filled in",
                    Status = "Fail"
                };
                return BadRequest(respond);
            }

            // ตรวจสอบว่าชื่อผู้ใช้มีอยู่แล้วหรือไม่
            if (await _db.MasUsers.AnyAsync(u => u.Username == model.Username))
            {
                respond = new ResponseModel
                {
                    ErrorCode = "ERR05",
                    ErrorMessage = "Username is already taken.",
                    Status = "Fail"
                };
                return Conflict(respond);
            }

            try
            {
                // แฮชรหัสผ่านก่อนบันทึก
                var hashedPassword = PasswordHelper.HashPassword(model.Password);

                var userData = new MasUser
                {
                    Username = model.Username,
                    Password = hashedPassword,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Email = email
                    
                };

                _db.MasUsers.Add(userData);
                await _db.SaveChangesAsync();

                respond = new ResponseModel
                {
                    Status = "Success",
                    Result = userData
                };

                return Ok(respond);
            }
            catch (Exception ex)
            {
                // Log the exception and return a generic error message
                _logger.LogError(ex, "An error occurred during register.");

                respond = new ResponseModel
                {
                    ErrorCode = "ERR03",
                    ErrorMessage = "An error occurred while processing your request.",
                    Status = "Fail"
                };

                return StatusCode(500, respond);  // Return internal server error if an exception occurred
            }
        }
    }
}