using System.Collections.Generic;
using System.Linq;
using Skaf.IO.SourceCode;
using Skaf.Parsing.Code;
using Skaf.Test.SampleFiles.CSharp;
using Xunit;

namespace Skaf.Test.Parsing.Code
{
    public class CSharpMetadataExtractorTest
    {
        [Theory]
        [InlineData("codeSnippet1.txt", new string[] { "Foo", "Bar" })]
        public void ProcessMetadata(string codeFileName, IEnumerable<string> methodNames)
        {
            //Extract metadata
            CSharpMetadataExtractor extractor = new CSharpMetadataExtractor();
            string cSharpCodePath = CSharpFileFetcher.GetSampleFile(codeFileName);
            SourceFile sourceFile = new SourceFile(cSharpCodePath);
            extractor.ProcessCodeFile(sourceFile);

            //Confirm all expected metadata
            var methods = extractor.ExtractedMetadata.Select(m => m.Name);
            bool containsAll = true;
            foreach (string methodName in methodNames)
                containsAll &= methods.Contains(methodName);

            Assert.True(containsAll);
        }
    }
}