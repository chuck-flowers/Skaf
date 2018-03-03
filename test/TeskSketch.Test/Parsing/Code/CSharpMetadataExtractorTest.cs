using TestSketch.Parsing.Code;
using Xunit;

namespace TeskSketch.Test.Parsing.Code
{
    public class CSharpMetadataExtractorTest
    {
        [Fact]
        public void ProcessMetadata()
        {
            CSharpMetadataExtractor extractor = new CSharpMetadataExtractor();
            extractor.ProcessCodeFile(csharpCode);

            bool containsFoo = false, containsBar = false;
            foreach (var type in extractor.ExtractedMetadata)
            {
                foreach (var method in type.Methods)
                {
                    containsFoo |= method.Name.Equals("Foo");
                    containsBar |= method.Name.Equals("Bar");
                }
            }

            Assert.True(containsBar && containsFoo);
        }

        private const string csharpCode =
@"using System;
using System.IO;

namespace Baz
{
    public class FooClass
    {
        public static void Main(string[] args)
        {
            Foo();
            Bar();
        }

        private static void Foo()
        {
            //Do some stuff in here
        }

        private static void Bar()
        {
            Console.WriteLine();
        }
    }
}";
    }
}