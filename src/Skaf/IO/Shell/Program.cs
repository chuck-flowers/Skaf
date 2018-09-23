using System.IO;
using Skaf.IO.Config;
using Skaf.Orchestration;

namespace Skaf.IO.Shell
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            switch (CommandLineOptions.ParseArgs(args))
            {
                case UpdateOptions update:
                    Run(update);
                    break;
            }
        }

        private static void Run(UpdateOptions options)
        {
            string configFileText = File.ReadAllText(options.ConfigFile);
            Configuration config = ConfigurationSerializer.Deserialize(configFileText);
            new ProcessOrchestrator(Directory.GetCurrentDirectory(), config).Execute();
        }
    }
}