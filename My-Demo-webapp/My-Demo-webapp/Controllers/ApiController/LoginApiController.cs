using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using My_Demo_webapp.Data;
using My_Demo_webapp.Models;
using My_Demo_webapp.Entites;

namespace My_Demo_webapp.Controllers.ApiController
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginApiController : Controller
    {
        private readonly WebappDbContext _db;
        
        public LoginApiController(WebappDbContext db)
        {
            _db = db;
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginReqModel model)
        {

            var username = model.Username;
            var password = model.Password;

            var res = new ResponseModel();

            if (username != null && password != null)
                try
                {
                    var checkUser = await _db.MasUsers.FirstOrDefaultAsync(u => u.Username == username);
                    

                    if (checkUser != null)
                    {
                        res = new ResponseModel
                        {
                            Status = "Success",
                            Result = checkUser
                        };
                        return Ok(res);
                    }
                    else
                    {
                        return Unauthorized(new { Message = "Login failed." });
                    }
                }
                catch (Exception ex)
                {

                }

            return BadRequest(ModelState);
        }
    }
}
