using System.Collections.Generic;
using System.IO;
using System.Linq;
using Skaf.IO.Files;
using Skaf.IO.Files.Mapping;
using Skaf.IO.Files.Metadata;

namespace Skaf.Orchestration.Map
{
    public class MapPhase
    {
        public MapPhase(IEnumerable<TypeMetadata> types)
        {
            Types = types;
        }

        public IEnumerable<TypeMetadata> Types { get; }

        public IEnumerable<(TypeMetadata, TestFile)> Execute()
        {
            var testProjectRoot = Path.Combine("..", "..", "test");
            testProjectRoot = Path.GetFullPath(testProjectRoot);
            TestFileMapper testFileMapper = new TestFileMapper(testProjectRoot);

            return Types.Select(t => (t, testFileMapper.MapTypeToTestFile(t)));
        }
    }
}