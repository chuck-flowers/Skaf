using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TestSketch.IO.Config;
using TestSketch.IO.Files;
using TestSketch.IO.Files.Mapping;
using TestSketch.IO.Files.Metadata;
using TestSketch.Parsing.Config;

namespace TestSketch.Orchestration
{
    public class ProcessOrchestrator
    {
        public void Execute(string baseDir, Configuration configuration)
        {
            var testProjectRoot = Path.Combine("..", "..", "test");
            testProjectRoot = Path.GetFullPath(testProjectRoot);
            TestFileMapper testFileMapper = new TestFileMapper(testProjectRoot);

            IEnumerable<string> paths = Globber.ExpandPath(baseDir, configuration.SourceFilePaths);
            IEnumerable<SourceFile> inputFiles = paths.Select(p => new SourceFile(p));
            IEnumerable<TypeMetadata> inputTypes = inputFiles.SelectMany(f => f.Types);
            IEnumerable<TestFile> testFiles = inputTypes.Select(t => testFileMapper.MapTypeToTestFile(t));
            foreach (var testFile in testFiles)
                Console.WriteLine(testFile.Path);
        }
    }
}