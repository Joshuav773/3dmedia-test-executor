using _3dMedia.Test.Executor.Config;
using _3dMedia.Test.Executor.Data.Context;
using _3dMedia.Test.Executor.Extensions;
using _3dMedia.Test.Executor.Models.Enums;
using JenkinsNET;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace _3dMedia.Test.Executor.Service
{
    public interface IJenkinsService
    {
        Task<IActionResult> GetListOfTestsAsync(string projectName);
        Task<IActionResult> RunJenkinsBuild_Postman(string buildName, IEnumerable<string> selectedTest);
    }

    public class JenkinsService : IJenkinsService
    {
        private readonly BeamDbContext _context;
        private readonly AppSettings _config;
        private readonly JenkinsSettings _jenkinsSettings;
        private readonly JenkinsClient _jenkinsClient;

        public JenkinsService(BeamDbContext context, IOptions<AppSettings> options, IOptions<JenkinsSettings> jenkinsOptions)
        {
            _context = context;
            _config = options.Value;
            _jenkinsSettings = jenkinsOptions.Value;
            _jenkinsClient = new JenkinsClient
            {
                BaseUrl = _jenkinsSettings.JenkinsUrl,
                UserName = _jenkinsSettings.JenkinsUsername,
                ApiToken = _jenkinsSettings.JenkinsApiKey,
            };

            //var response = await Task.Run(() => client.Jobs.Build("Run-PostmanTest"));
        }

        private IList<Data.Models.Test> TestData = new List<Data.Models.Test>
        {
            new Data.Models.Test() { Name = $"Test-{new Random().Next(0, 100)}", TestResult = (TestResult)new Random().Next(1, 4), Description = "This is a Test description", ProjectName = "Beam Api" },
            new Data.Models.Test() { Name = $"Test-{new Random().Next(0, 100)}", TestResult = (TestResult)new Random().Next(1, 4), Description = "This is a Test description", ProjectName = "Beam Api" },
            new Data.Models.Test() { Name = $"Test-{new Random().Next(0, 100)}", TestResult = (TestResult)new Random().Next(1, 4), Description = "This is a Test description", ProjectName = "Beam Api" },
            new Data.Models.Test() { Name = $"Test-{new Random().Next(0, 100)}", TestResult = (TestResult)new Random().Next(1, 4), Description = "This is a Test description", ProjectName = "Beam Api" },
            new Data.Models.Test() { Name = $"Test-{new Random().Next(0, 100)}", TestResult = (TestResult)new Random().Next(1, 4), Description = "This is a Test description", ProjectName = "Beam Api" },
            new Data.Models.Test() { Name = $"Test-{new Random().Next(0, 100)}", TestResult = (TestResult)new Random().Next(1, 4), Description = "This is a Test description", ProjectName = "Beam Api" },
            new Data.Models.Test() { Name = $"Test-{new Random().Next(0, 100)}", TestResult = (TestResult)new Random().Next(1, 4), Description = "This is a Test description", ProjectName = "Beam Api" }
        };

        public async Task<IActionResult> GetListOfTestsAsync(string projectName)
        {
            try
            {

                if (_config.DbActive)
                {
                    var data = await _context.Tests
                                .Where(test => test.ProjectName.ToLower().Equals(projectName.ToLower()))
                                .OrderBy(test => test.Id)
                                .ToListAsync();

                    return data.GetSuccessResponse();
                        
                }

                return TestData.AsEnumerable().GetSuccessResponse();
            }
            catch (Exception ex)
            {

                return ex.GetServerErrorResponse();
            }
        }

        public async Task<IActionResult> RunJenkinsBuild_Postman(string buildName, IEnumerable<string> selectedTest)
        {
            try
            {
                if (!_config.DbActive)
                {
                    return "https://Google.com".GetSuccessResponse();
                }

                if (string.IsNullOrEmpty(buildName))
                {
                    return "Build Name is missing cannot continue".GetErrorResponse();
                }

                var @params = new Dictionary<string, string>
                {
                    { "selectedTest", string.Join(',', selectedTest) }
                };

                var build = await _jenkinsClient.Jobs.BuildWithParametersAsync(buildName, @params);

                if (string.IsNullOrEmpty(build.QueueItemUrl))
                {
                    return "There was an error, build was not queued".GetErrorResponse();
                }

                return build.QueueItemUrl.GetSuccessResponse();
            }
            catch (Exception ex)
            {
                return ex.GetServerErrorResponse();
            }
        }

        private void UpdateTestResults(IEnumerable<Data.Models.Test> tests)
        {
            //do something with the test before this.
            Task.Run(() => _context.Tests.UpdateRange(tests)).GetAwaiter();
        }
    }
}
