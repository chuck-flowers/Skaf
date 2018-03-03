using System.IO;
using TestSketch.IO.Config;

namespace TestSketch.IO.Shell
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
            //Loads the configuration
            string configFileText = File.ReadAllText(options.ConfigFile);
            Configuration config = ConfigurationSerializer.Deserialize(configFileText);
        }
    }
}