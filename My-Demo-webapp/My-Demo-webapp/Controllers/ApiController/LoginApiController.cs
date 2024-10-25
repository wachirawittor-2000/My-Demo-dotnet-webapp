using Microsoft.AspNetCore.Mvc;
using My_Demo_webapp.Models;

namespace My_Demo_webapp.Controllers.ApiController
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginApiController : Controller
    {
        /*private readonly ApplicationDBContext _dbContext;*/

        /*public LoginApiController(ApplicationDBContext dbContext)
        {
            _dbContext = dbContext;
        }*/

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginReqModel model)
        {
            var test = model;
            var username = model.Username;
            var password = model.Password;

            var res = new ResponseModel();

            if (username != null && password != null)
                try
                {
                    var user = "WACHIRAWIT";
                    //var user = await _dbContext.MasUsers.FirstOrDefaultAsync(u => u.Username == username);
                    //var user = await _dbContext.MasUsers.FirstOrDefaultAsync(u => u.Username == username);

                    if (user != null)
                    {
                        res = new ResponseModel
                        {
                            Status = "Success",
                            Result = user
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
