using _3dMedia.Test.Executor.Models;
using System.Linq;

namespace _3dMedia.Test.Executor.Service
{
    public interface IJenkinsService
    {
        Task<IEnumerable<Models.Test>> GetListOfTestsAsync();
    }

    public class JenkinsService : IJenkinsService
    {
        private IList<Models.Test> TestData = new List<Models.Test>
        {
            new Models.Test() { Name = $"Test-{new Random().Next(0, 100)}", TestResult = (TestResult)new Random().Next(1, 4) },
            new Models.Test() { Name = $"Test-{new Random().Next(0, 100)}", TestResult = (TestResult)new Random().Next(1, 4) },
            new Models.Test() { Name = $"Test-{new Random().Next(0, 100)}", TestResult = (TestResult)new Random().Next(1, 4) },
            new Models.Test() { Name = $"Test-{new Random().Next(0, 100)}", TestResult = (TestResult)new Random().Next(1, 4) },
            new Models.Test() { Name = $"Test-{new Random().Next(0, 100)}", TestResult = (TestResult)new Random().Next(1, 4) },
            new Models.Test() { Name = $"Test-{new Random().Next(0, 100)}", TestResult = (TestResult)new Random().Next(1, 4) },
            new Models.Test() { Name = $"Test-{new Random().Next(0, 100)}", TestResult = (TestResult)new Random().Next(1, 4) }
        };

        public async Task<IEnumerable<Models.Test>> GetListOfTestsAsync()
        {
            return await Task.FromResult(TestData.AsEnumerable());
        }
    }
}
