using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyDashboard.Model.Exceptions;

namespace MyDashboard.WebApi.Controllers
{
    [ApiController]
    [Route("api/home")]
    public class HomeController : ControllerBase
    {
        [Authorize]
        [HttpGet("checkAuth")]
        public IActionResult CheckAuthorization()
        {
            return Ok(new { message = "User is able to access the restricted endpoint" });
        }

        [HttpGet("get")]
        public IActionResult CheckApiEndPoint()
        {
            return Ok(new { message = "User is able to access the non restricted endpoint" });
        }

        [HttpGet("throwVaidationError")]
        public IActionResult ThrowVaidationError()
        {
            throw new ValidationException("Vaidation Error has been throwed");
            return Ok();
        }
    }
}
