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
            return Parser.Default.ParseArguments<UpdateOptions>(args)
                .MapResult(
                    (UpdateOptions opts) => opts,
                    errs => throw new Exception("There was an issue parsing the command line args.")
                );
        }
    }
}