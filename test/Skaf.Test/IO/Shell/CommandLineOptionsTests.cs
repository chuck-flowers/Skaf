using System;
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
                new string[] { "update", "-c", "config.json" },
                (CommandLineEvaluator) (o => o is UpdateOptions u ? u.ConfigFile.Equals("config.json") : false)
            },
            new object[]
            {
                new string[] { "update", "--config", "config.json" },
                (CommandLineEvaluator) (o => o is UpdateOptions u ? u.ConfigFile.Equals("config.json") : false)
            }
        };

        [Theory]
        [MemberData(nameof(ParseTestInput))]
        public void ParseArgsTest(string[] args, Func<CommandLineOptions, bool> eval)
        {
            CommandLineOptions options = CommandLineOptions.ParseArgs(args);
            Assert.True(eval(options));
        }
    }
}