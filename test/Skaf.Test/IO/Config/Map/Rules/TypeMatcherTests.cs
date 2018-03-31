using System.Collections.Generic;
using System.IO;
using Skaf.IO.Config.Map.Rules;
using Skaf.IO.SourceCode.Metadata;
using Xunit;

namespace Skaf.Test.IO.Config.Map.Rules
{
    public class TypeMatcherTests
    {
        public static IEnumerable<object[]> FailingCases => new object[][]
        {
            new object[]
            {
                new MethodMetadata(
                    "FooMethod",
                    new TypeMetadata(
                        Path.Combine("Root", "Dir", "Sub", "FooClass.cs"),
                        "Root.Dir.Sub",
                        "FooClass"
                    )
                ),
                new MethodMatcher()
                {
                    Path = "*.cs",
                    Namespace = "Root**",
                    Type = "*Class",
                    Method = "*Method"
                }
            }
        };

        public static IEnumerable<object[]> PassingCases => new object[][]
        {
            new object[]
            {
                new MethodMetadata(
                    "FooMethod",
                    new TypeMetadata(
                        Path.Combine("Root", "Dir", "Sub", "FooClass.cs"),
                        "Root.Dir.Sub",
                        "FooClass"
                    )
                ),
                new MethodMatcher()
                {
                    Path = "**.cs",
                    Namespace = "Root**",
                    Type = "*Class",
                    Method = "*Method"
                }
            }
        };

        [Theory]
        [MemberData(nameof(FailingCases))]
        public void MatchesFailingTest(MethodMetadata method, MethodMatcher matcher)
        {
            Assert.False(matcher.Matches(method));
        }

        [Theory]
        [MemberData(nameof(PassingCases))]
        public void MatchesPassingTest(MethodMetadata method, MethodMatcher matcher)
        {
            Assert.True(matcher.Matches(method));
        }
    }
}