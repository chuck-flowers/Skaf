using System.Collections.Generic;
using System.IO;
using System.Linq;
using Skaf.IO.Config;
using Skaf.IO.Files;
using Skaf.IO.Files.Mapping;
using Skaf.IO.Files.Metadata;
using Skaf.IO.Files.Writers;
using Skaf.Parsing.Config;

namespace Skaf.Orchestration
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
                TestFileWriter writer = new TestFileWriter(type, testFile);
                writer.Write();
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
            ICollection<string> globStrings = new List<string>();
            foreach (var rule in Configuration.InputConfig.SourceFileRules)
            {
                string baseGlobPath = Configuration.InputConfig.SourcePath;
                string fullGlobString = Path.Combine(baseGlobPath, rule.Include);
                globStrings.Add(fullGlobString);
            }

            return Globber.ExpandPath(BaseDirectory, globStrings)
                .Select(p => new SourceFile(p))
                .SelectMany(t => t.Types);
        }
    }
}