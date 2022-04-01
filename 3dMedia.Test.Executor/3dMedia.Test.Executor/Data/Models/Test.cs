using _3dMedia.Test.Executor.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace _3dMedia.Test.Executor.Data.Models
{
    public class Test
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ProjectName { get; set; }
        public TestResult TestResult { get; set; }  
    }
}
