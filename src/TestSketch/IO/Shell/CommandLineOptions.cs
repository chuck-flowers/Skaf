using CommandLine;

namespace TestSketch.IO.Shell
{
    internal class CommandLineOptions
    {
        [Option('c', "config", Required = true, HelpText = "The file that contains the configuration for how the tests are generated")]
        public string ConfigFile { get; set; }
    }
}