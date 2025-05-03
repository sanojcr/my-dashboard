using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyDashboard.Model.Dtos;
using MyDashboard.Service;
using MyDashboard.Service.Interface;

namespace MyDashboard.WebApi.Controllers
{
    [Route("api/log")]
    [ApiController]
    public class LogController : ControllerBase
    {
        private readonly ILoggerService _loggerService;
        public LogController(ILoggerService loggerService) {
            _loggerService = loggerService;
        }

        [HttpPost]
        public IActionResult Log([FromBody] ErrorLogDto error)
        {
            var result = _loggerService.AddLogToDatabase(error);
            return Ok(result);
        }
    }
}
