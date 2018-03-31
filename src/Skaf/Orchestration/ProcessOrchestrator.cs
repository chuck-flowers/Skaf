using System.Linq;
using Skaf.IO.Config;
using Skaf.Orchestration.Generate;
using Skaf.Orchestration.Input;
using Skaf.Orchestration.Map;

namespace Skaf.Orchestration
{
    public class ProcessOrchestrator
    {
        public ProcessOrchestrator(string baseDir, Configuration configuration)
        {
            BaseDirectory = baseDir;
            Configuration = configuration;
        }

        public void Execute()
        {
            var inputTypes = new InputPhase(BaseDirectory, Configuration.InputConfig).Execute().ToList();
            var mappings = new MapPhase(inputTypes, Configuration.MapConfig).Execute().ToList();
            new GeneratePhase(mappings, Configuration.GenerateConfig).Execute();
        }

        private string BaseDirectory { get; }

        private Configuration Configuration { get; }
    }
}