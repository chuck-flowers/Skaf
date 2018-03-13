using System.Collections.Generic;
using Skaf.IO.Shell;
using Xunit;
using CommandLineEvaluator = System.Func<Skaf.IO.Shell.CommandLineOptions, bool>;

namespace TeskSketch.Test.IO.Shell
{
    public class CommandLineOptionsTests
    {
        public static IEnumerable<object[]> ParseTestInput => new object[][]
        {
            new object[]
            {
                new string[] { "-c", "config.json" },
                (CommandLineEvaluator) (o => o.ConfigFile.Equals("config.json"))
            },
            new object[]
            {
                new string[] { "--config", "config.json" },
                (CommandLineEvaluator) (o => o.ConfigFile.Equals("config.json"))
            }
        };

        [Theory]
        [MemberData(nameof(ParseTestInput))]
        public void ParseTest(string[] args, CommandLineEvaluator eval)
        {
            CommandLineOptions options = CommandLineOptions.ParseArgs(args);
            Assert.True(eval(options));
        }
    }
}