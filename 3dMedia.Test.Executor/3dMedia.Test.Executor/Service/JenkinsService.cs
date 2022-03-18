using _3dMedia.Test.Executor.Config;
using _3dMedia.Test.Executor.Data.Context;
using _3dMedia.Test.Executor.Models;
using Microsoft.Extensions.Options;
using System.Linq;

namespace _3dMedia.Test.Executor.Service
{
    public interface IJenkinsService
    {
        Task<IEnumerable<Models.Test>> GetListOfTestsAsync();
    }

    public class JenkinsService : IJenkinsService
    {
        private readonly BeamDbContext _context;
        private readonly AppSettings _config;

        public JenkinsService(BeamDbContext context, IOptions<AppSettings> options)
        {
            _context = context;
            _config = options.Value;
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

        public async Task<IEnumerable<Models.Test>> GetListOfTestsAsync()
        {
            if (_config.DbActive)
            {
                return await Task.FromResult(_context.Tests.AsEnumerable());
            }

            return await Task.FromResult(TestData.AsEnumerable());

        }
    }
}
