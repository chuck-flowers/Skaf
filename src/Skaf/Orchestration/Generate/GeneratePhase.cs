using System;
using System.Collections.Generic;
using System.IO;
using Skaf.IO.Config.Generate;
using Skaf.IO.SourceCode.Metadata;
using Skaf.IO.SourceCode.Writers;

namespace Skaf.Orchestration.Generate
{
    public class GeneratePhase
    {
        public GeneratePhase(IEnumerable<(MethodMetadata, MethodMetadata)> mappings, GenerateConfiguration generateConfig)
        {
            Mappings = mappings;
            GenerateConfig = generateConfig ?? throw new ArgumentNullException(nameof(generateConfig));
        }

        public GenerateConfiguration GenerateConfig { get; }

        public IEnumerable<(MethodMetadata, MethodMetadata)> Mappings { get; }

        public void Execute()
        {
            foreach (var (method, test) in Mappings)
            {
                string fullPath = Path.GetFullPath(Path.Combine(GenerateConfig.Root, test.ParentType.Path));
                TestFileWriter writer = new TestFileWriter(fullPath);
                writer.Write(method);
            }
        }
    }
}