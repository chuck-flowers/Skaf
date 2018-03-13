using System.IO;
using Skaf.IO.Config;
using Skaf.Orchestration;

namespace Skaf.IO.Shell
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            CommandLineOptions options = CommandLineOptions.ParseArgs(args);
            Run(options);
        }

        private static void Run(CommandLineOptions options)
        {
            string configFileText = File.ReadAllText(options.ConfigFile);
            Configuration config = ConfigurationSerializer.Deserialize(configFileText);
            new ProcessOrchestrator(Directory.GetCurrentDirectory(), config).Execute();
        }
    }
}