using System;
using System.Collections.Generic;
using System.IO;
using CommandLine;
using TestSketch.IO.Config;

namespace TestSketch.IO.Shell
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            Parser.Default
                .ParseArguments<CommandLineOptions>(args)
                .WithParsed(Run)
                .WithNotParsed(HandleParseError);
        }

        private static void HandleParseError(IEnumerable<Error> errors)
        {
            foreach (Error error in errors)
                Console.WriteLine("There was an error: " + error);
        }

        private static void Run(CommandLineOptions options)
        {
            //Loads the configuration
            string configFileText = File.ReadAllText(options.ConfigFile);
            Configuration config = ConfigurationSerializer.Deserialize(configFileText);
        }
    }
}