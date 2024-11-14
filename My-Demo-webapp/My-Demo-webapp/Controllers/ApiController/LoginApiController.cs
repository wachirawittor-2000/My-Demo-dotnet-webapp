using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using My_Demo_webapp.Data;
using My_Demo_webapp.Models;

namespace My_Demo_webapp.Controllers.ApiController
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginApiController : Controller
    {
        private readonly WebappDbContext _db;
        private readonly ILogger<LoginApiController> _logger;  // Logger to log errors

        public LoginApiController(WebappDbContext db, ILogger<LoginApiController> logger)
        {
            _db = db;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginReqModel model)
        {
            var username = model.Username;
            var password = model.Password;

            var respond = new ResponseModel();

            // Check if username and password are not null
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                respond = new ResponseModel
                {
                    ErrorCode = "ERR01",
                    ErrorMessage = "Username or password cannot be empty.",
                    Status = "Fail"
                };

                return BadRequest(respond);  // Return BadRequest if username or password is missing
            }

            try
            {
                // Find user by username
                var userData = await _db.MasUsers.FirstOrDefaultAsync(u => u.Username == username);

                // Check if user exists and if password is correct
                if (userData != null && PasswordHelper.VerifyPassword(userData.Password, password))
                {
                    respond = new ResponseModel
                    {
                        Status = "Success",
                        Result = userData  // Return user data if login successful
                    };

                    return Ok(respond);  // Return Success response
                }
                else
                {
                    respond = new ResponseModel
                    {
                        ErrorCode = "ERR02",
                        ErrorMessage = "Username or password is incorrect.",
                        Status = "Fail"
                    };

                    return Unauthorized(respond);  // Return Unauthorized if login failed
                }
            }
            catch (Exception ex)
            {
                // Log the exception and return a generic error message
                _logger.LogError(ex, "An error occurred during login.");

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
