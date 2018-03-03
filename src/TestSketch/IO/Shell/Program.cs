using System;
using System.Collections.Generic;
using CommandLine;

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
            Console.WriteLine("ConfigFile = " + options.ConfigFile);
        }
    }
}