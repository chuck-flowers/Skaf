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
        public ProcessOrchestrator(string baseDir, Configuration configuration)
        {
            BaseDirectory = baseDir;
            Configuration = configuration;
        }

        public void Execute()
        {
            var inputTypes = ProcessInputFiles();
            var mappings = MapToTestFiles(inputTypes);
            foreach (var pair in mappings)
            {
                (var type, var testFile) = pair;
                Console.WriteLine("{0}.{1} -> {2}", type.Namespace, type.Name, testFile.Path);
            }
        }

        private string BaseDirectory { get; }

        private Configuration Configuration { get; }

        /// <summary>
        /// Associates each of the input types with a test file
        /// </summary>
        /// <param name="types">The types to associate with test files</param>
        /// <returns>The pairing of type metadata with its appropriate test file</returns>
        private IEnumerable<(TypeMetadata, TestFile)> MapToTestFiles(IEnumerable<TypeMetadata> types)
        {
            var testProjectRoot = Path.Combine("..", "..", "test");
            testProjectRoot = Path.GetFullPath(testProjectRoot);
            TestFileMapper testFileMapper = new TestFileMapper(testProjectRoot);

            return types.Select(t => (t, testFileMapper.MapTypeToTestFile(t)));
        }

        /// <summary>
        /// Determines the type definitions to act upon
        /// </summary>
        /// <returns>The files to act upon</returns>
        private IEnumerable<TypeMetadata> ProcessInputFiles()
        {
            return Globber.ExpandPath(BaseDirectory, Configuration.SourceFilePaths)
                .Select(p => new SourceFile(p))
                .SelectMany(t => t.Types);
        }
    }
}