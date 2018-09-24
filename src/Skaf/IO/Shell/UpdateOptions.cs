using CommandLine;

namespace Skaf.IO.Shell
{
    [Verb("update", HelpText = "Generates tests based on the input/source files.")]
    public class UpdateOptions : CommandLineOptions
    {
        /// <summary>
        /// The relative path to the configuration to use for running the update command.
        /// </summary>
        [Option('c', "config", HelpText = ConfigHelpText)]
        public string ConfigFile { get; set; } = "./skaf.json";

        /// <summary>
        /// A flag indicating whether the skaf should be automatically run again when the inputs
        /// change on disk.
        /// </summary>
        [Option('w', "watch", HelpText = WatchHelpText)]
        public bool IsWatchMode { get; set; } = false;

        private const string ConfigHelpText = "The file that contains the configuration options for generating tests";

        private const string WatchHelpText = "Specifies that the command should automatically rerun when files change";
    }
}