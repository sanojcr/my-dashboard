using Microsoft.AspNetCore.Mvc;

namespace MyDashboard.WebApi.Controllers
{
    [ApiController]
    [Route("api/home")]
    public class HomeController : ControllerBase
    {
        [HttpGet("get")]
        public IActionResult Index()
        {
            return Ok("User is able to login successfully");
        }
    }
}
