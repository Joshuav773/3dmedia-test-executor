using _3dMedia.Test.Executor.Service;
using Microsoft.AspNetCore.Mvc;

namespace _3dMedia.Test.Executor.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class JenkinsController : ControllerBase
    {
        private readonly ILogger<JenkinsController> _logger;
        
        private readonly IJenkinsService _jenkinsService;

        public JenkinsController(ILogger<JenkinsController> logger, IJenkinsService jenkinsService)
        {
            _logger = logger;
            _jenkinsService = jenkinsService;
        }

        [HttpGet("GetListOfTestsAsync")]
        public async Task<IActionResult> GetListOfTestsAsync()
        {
            var tests = await _jenkinsService.GetListOfTestsAsync("Beam Api");

            return Ok(tests);
        }
    }
}