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
            var inputTypes = new InputPhase(BaseDirectory, Configuration.InputConfig).Execute();
            var mappings = new MapPhase(inputTypes).Execute();
            new GeneratePhase(mappings).Execute();
        }

        private string BaseDirectory { get; }

        private Configuration Configuration { get; }
    }
}