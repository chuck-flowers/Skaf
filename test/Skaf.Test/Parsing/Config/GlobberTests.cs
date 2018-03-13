using System.Collections.Generic;
using System.IO;
using System.Linq;
using Skaf.Parsing.Config;
using Xunit;

using PathExpansionEvaluator = System.Func<System.Collections.Generic.IEnumerable<string>, bool>;

namespace TeskSketch.Test.Parsing.Config
{
    public class GlobberTests
    {
        public static IEnumerable<object[]> ExpandPathInputs => new object[][]
        {
            new object[]
            {
                Path.Combine(AbsoluteDemoDir, "src"),
                new string[] { "**.cs" },
                (PathExpansionEvaluator) (paths => paths.Count() == 2)
            },
            new object[]
            {
                Path.Combine(AbsoluteDemoDir, "src"),
                new string[] { "**.vb" },
                (PathExpansionEvaluator) (paths => paths.Count() == 0)
            }
        };

        [Theory]
        [MemberData(nameof(ExpandPathInputs))]
        public void ExpandPathTest(string baseDir, IEnumerable<string> patterns, PathExpansionEvaluator eval)
        {
            var result = Globber.ExpandPath(baseDir, patterns);
            Assert.True(eval(result));
        }

        private static string AbsoluteDemoDir => Path.GetFullPath(RelativeDemoDir);

        private static string RelativeDemoDir => Path.Combine("..", "..", "..", "..", "..", "demo");
    }
}