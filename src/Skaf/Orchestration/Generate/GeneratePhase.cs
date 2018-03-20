using System.Collections.Generic;
using Skaf.IO.Files;
using Skaf.IO.Files.Metadata;
using Skaf.IO.Files.Writers;

namespace Skaf.Orchestration.Generate
{
    public class GeneratePhase
    {
        public GeneratePhase(IEnumerable<(TypeMetadata, TestFile)> mappings)
        {
            Mappings = mappings;
        }

        public IEnumerable<(TypeMetadata, TestFile)> Mappings { get; }

        public void Execute()
        {
            foreach (var pair in Mappings)
            {
                (var type, var testFile) = pair;
                TestFileWriter writer = new TestFileWriter(type, testFile);
                writer.Write();
            }
        }
    }
}