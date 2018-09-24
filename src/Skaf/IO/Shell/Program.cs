using System;
using System.IO;
using Skaf.IO.Config;
using Skaf.Orchestration;

namespace Skaf.IO.Shell
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                switch (CommandLineOptions.ParseArgs(args))
                {
                    case InitOptions init:
                        break;

                    case UpdateOptions update:
                        Run(update);
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex.Message);
                return;
            }
        }

        private static void Run(InitOptions options)
        {
            //TODO: Present the questionaire if necessary
            //TODO: Output the configuration file
        }

        private static void Run(UpdateOptions options)
        {
            string configFile = Path.Combine(Environment.CurrentDirectory, options.ConfigFile);

            // If the file doesn't exist, alert the user.
            if (!File.Exists(options.ConfigFile))
                throw new Exception($"The configuration file '{options.ConfigFile}' does not exist.");

            Configuration config = ConfigurationSerializer.Load(configFile);

            // Runs the update process
            new ProcessOrchestrator(Directory.GetCurrentDirectory(), config).Execute();
        }
    }
}