using _3dMedia.Test.Executor.Config;
using _3dMedia.Test.Executor.Data.Context;
using _3dMedia.Test.Executor.Models;
using JenkinsNET;
using Microsoft.Extensions.Options;
using System.Linq;

namespace _3dMedia.Test.Executor.Service
{
    public interface IJenkinsService
    {
        Task<IEnumerable<Models.Test>> GetListOfTestsAsync(string projectName);
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
                UserName = _jenkinsSettings.JenkinsApiKey,
                ApiToken = _jenkinsSettings.JenkinsUsername,
            };

            //var response = await Task.Run(() => client.Jobs.Build("Run-PostmanTest"));
        }

        private IList<Models.Test> TestData = new List<Models.Test>
        {
            new Models.Test() { Name = $"Test-{new Random().Next(0, 100)}", TestResult = (TestResult)new Random().Next(1, 4), Description = "This is a Test description", ProjectName = "Beam Api" },
            new Models.Test() { Name = $"Test-{new Random().Next(0, 100)}", TestResult = (TestResult)new Random().Next(1, 4), Description = "This is a Test description", ProjectName = "Beam Api" },
            new Models.Test() { Name = $"Test-{new Random().Next(0, 100)}", TestResult = (TestResult)new Random().Next(1, 4), Description = "This is a Test description", ProjectName = "Beam Api" },
            new Models.Test() { Name = $"Test-{new Random().Next(0, 100)}", TestResult = (TestResult)new Random().Next(1, 4), Description = "This is a Test description", ProjectName = "Beam Api" },
            new Models.Test() { Name = $"Test-{new Random().Next(0, 100)}", TestResult = (TestResult)new Random().Next(1, 4), Description = "This is a Test description", ProjectName = "Beam Api" },
            new Models.Test() { Name = $"Test-{new Random().Next(0, 100)}", TestResult = (TestResult)new Random().Next(1, 4), Description = "This is a Test description", ProjectName = "Beam Api" },
            new Models.Test() { Name = $"Test-{new Random().Next(0, 100)}", TestResult = (TestResult)new Random().Next(1, 4), Description = "This is a Test description", ProjectName = "Beam Api" }
        };

        public async Task<IEnumerable<Models.Test>> GetListOfTestsAsync(string projectName)
        {
            if (_config.DbActive)
            {
                return await Task.FromResult(
                    _context.Tests
                            .Where(test => test.ProjectName.ToLower().Equals(projectName.ToLower()))
                            .OrderByDescending(test => test.Id)
                            .AsEnumerable()
                    );
            }

            return await Task.FromResult(TestData.AsEnumerable());

        }

        public async Task<JenkinsBuildResult> RunJenkinsBuild_Postman(string buildName, IEnumerable<string> selectedTest)
        {
            var @params = new Dictionary<string, string>();

            foreach(var test in selectedTest) @params.Add("selectedTest", test);

            return await _jenkinsClient.Jobs.BuildWithParametersAsync(buildName, @params);
        }
    }
}
