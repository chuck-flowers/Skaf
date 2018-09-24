using System;
using CommandLine;

namespace Skaf.IO.Shell
{
    public abstract class CommandLineOptions
    {
        /// <summary>
        /// Parses the arguments passed to the command line and returns the constructed object
        /// </summary>
        /// <param name="args">
        /// The arguments that the user of the application passed through the command line
        /// </param>
        /// <returns>The CommandLineOptions parsed and constructed into an object</returns>
        public static CommandLineOptions ParseArgs(string[] args)
        {
            CommandLineOptions options = null;
            Parser.Default.ParseArguments<InitOptions, UpdateOptions>(args)
                .WithParsed<InitOptions>(init => options = init)
                .WithParsed<UpdateOptions>(update => options = update)
                .WithNotParsed(errs => throw new Exception("There was a problem parsing the command line arguments"));

            return options;
        }
    }

    [Verb("init", HelpText = "Generates a new skaf configuration")]
    public class InitOptions : CommandLineOptions
    {
        [Option('q', "quiet", HelpText = "Indicates that the default answer should be given for all the questions")]
        public bool IsQuiet { get; set; }

        [Option('o', "output", HelpText = "Indicates that a custom name should be used for the generated configuration file")]
        public string OutputName { get; set; } = "skaf.json";
    }
}