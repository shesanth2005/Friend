using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Friend.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TestController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get() => Ok("API is working!");
    }
}
