using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Skaf.IO.Config.Input;
using Skaf.IO.SourceCode;
using Skaf.IO.SourceCode.Metadata;
using Skaf.Parsing.Config;

namespace Skaf.Orchestration.Input
{
    public class InputPhase
    {
        public InputPhase(string baseDirectory, InputConfiguration configuration)
        {
            BaseDirectory = baseDirectory;
            Configuration = configuration;
        }

        public string BaseDirectory { get; }

        public InputConfiguration Configuration { get; }

        public IEnumerable<MethodMetadata> Execute()
        {
            var globStrings = MakeGlobStrings();
            var sourceFilePaths = Globber.ExpandPath(BaseDirectory, globStrings);
            var methodMetadata = ParseMethods(sourceFilePaths);

            foreach (var m in methodMetadata)
                Console.WriteLine("Parsed " + m);
            Console.WriteLine();

            return methodMetadata;
        }

        private IEnumerable<string> MakeGlobStrings()
        {
            Console.WriteLine("INPUT");
            return Configuration
                .SourceFileRules
                .Select(r => Path.Combine(Configuration.SourcePath, r.Include));
        }

        private IEnumerable<MethodMetadata> ParseMethods(IEnumerable<string> sourceFilePaths)
        {
            return sourceFilePaths
                .Select(p => new SourceFile(p))
                .SelectMany(f => f.Methods);
        }
    }
}