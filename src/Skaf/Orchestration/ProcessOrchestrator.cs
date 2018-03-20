using Skaf.IO.Config;
using Skaf.IO.Files.Writers;
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
            foreach (var pair in mappings)
            {
                (var type, var testFile) = pair;
                TestFileWriter writer = new TestFileWriter(type, testFile);
                writer.Write();
            }
        }

        private string BaseDirectory { get; }

        private Configuration Configuration { get; }
    }
}