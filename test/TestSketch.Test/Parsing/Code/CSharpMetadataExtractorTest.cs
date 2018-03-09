using System.Collections.Generic;
using System.Linq;
using TeskSketch.Test.SampleFiles.CSharp;
using TestSketch.Parsing.Code;
using Xunit;

namespace TeskSketch.Test.Parsing.Code
{
    public class CSharpMetadataExtractorTest
    {
        [Theory]
        [InlineData("codeSnippet1.txt", new string[] { "Foo", "Bar" })]
        public void ProcessMetadata(string codeFileName, IEnumerable<string> methodNames)
        {
            //Extract metadata
            CSharpMetadataExtractor extractor = new CSharpMetadataExtractor();
            string csharpCode = CSharpFileFetcher.GetSampleCode(codeFileName);
            extractor.ProcessCodeFile(csharpCode);

            //Confirm all expected metadata
            var methods = extractor.ExtractedMetadata.SelectMany(t => t.Methods).Select(m => m.Name);
            bool containsAll = true;
            foreach (string methodName in methodNames)
                containsAll &= methods.Contains(methodName);

            Assert.True(containsAll);
        }
    }
}