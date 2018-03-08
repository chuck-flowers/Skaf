using System;
using System.Collections.Generic;
using System.Linq;
using TestSketch.IO.Config;
using TestSketch.IO.Files;
using TestSketch.IO.Files.Metadata;
using TestSketch.Parsing.Config;

namespace TestSketch.Orchestration
{
    public class ProcessOrchestrator
    {
        public void Execute(string baseDir, Configuration configuration)
        {
            IEnumerable<string> paths = Globber.ExpandPath(baseDir, configuration.SourceFilePaths);
            IEnumerable<SourceFile> inputFiles = paths.Select(p => new SourceFile(p));
            IEnumerable<TypeMetadata> inputTypes = inputFiles.SelectMany(f => f.Types);
            foreach (var type in inputTypes)
            {
                Console.WriteLine(type.Name);
                foreach (var method in type.Methods)
                    Console.WriteLine("    {0}", method.Name);
            }
        }
    }
}