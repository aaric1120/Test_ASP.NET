using Microsoft.AspNetCore.Mvc;

namespace YourNamespace.Controllers {
    
    [ApiController]
    [Route("api/[controller]")]
    
    public class DataController : ControllerBase {
        private readonly IMyDataService _dataService;

        public DataController(IMyDataService dataService) {
            _dataService = dataService;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] MyData data) {
            if (data == null) {
                return BadRequest();
            }

            await _dataService.ProcessDataAsync(data);

            return Ok(new { message = "Data received and processed", data});
        }

        [HttpOptions]
        public IActionResult Options() {
            Response.Headers.Add("Access-Control-Allow-Origin","*");
            Response.Headers.Add("Access-Control-Allow-Methods","GET, POST, OPTIONS");
            Response.Headers.Add("Access-Control-Allow-Headers", "Content-Type, text/plain");
            return NoContent();
        }
    }
}