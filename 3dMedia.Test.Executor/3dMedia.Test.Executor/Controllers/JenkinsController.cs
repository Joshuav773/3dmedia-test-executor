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

        [HttpGet("GetListOfTests")]
        public async Task<IActionResult> GetListOfTests()
        {
            var response = await _jenkinsService.GetListOfTestsAsync("Beam Api");

            return response;
        }

        [HttpPost("ExecuteTests")]
        public async Task<IActionResult> ExecuteTests([FromBody]IEnumerable<string> selectTests)
        {
            var buildResponse = await _jenkinsService.RunJenkinsBuild_Postman("Run-PostmanTest", selectTests);

            return buildResponse;
        }
    }
}