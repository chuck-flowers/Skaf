using CommandLine;

namespace Skaf.IO.Shell
{
    [Verb("update", HelpText = "Generates tests based on the input/source files.")]
    public class UpdateOptions : CommandLineOptions
    {
        [Option('c', "config", HelpText = "The file that contains the configuration for how the tests are generated")]
        public string ConfigFile { get; set; }

        [Option('w', "watch", HelpText = "Specifies that the command should automatically rerun when files change")]
        public bool IsWatchMode { get; set; }

        private const string ConfigHelpText = "The file that contains the configuration options for generating tests";
    }
}