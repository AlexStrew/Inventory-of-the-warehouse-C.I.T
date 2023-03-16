using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InvAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatusController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(new { Status = "OK" });
        }        
    }
}
