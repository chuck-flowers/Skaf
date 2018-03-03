using System;
using CommandLine;

namespace TestSketch.IO.Shell
{
    public class CommandLineOptions
    {
        [Option('c', "config", Required = true, HelpText = "The file that contains the configuration for how the tests are generated")]
        public string ConfigFile { get; set; }

        /// <summary>
        /// Parses the arguments passed to the command line and returns the constructed object
        /// </summary>
        /// <param name="args">The arguments that the user of the application passed through the command line</param>
        /// <returns>The CommandLineOptions parsed and constructed into an object</returns>
        public static CommandLineOptions ParseArgs(string[] args)
        {
            var result = Parser.Default.ParseArguments<CommandLineOptions>(args);
            if (result is Parsed<CommandLineOptions> success)
                return success.Value;
            else
                throw new Exception("There was an error while parsing");
        }
    }
}