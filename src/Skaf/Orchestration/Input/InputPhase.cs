using System.Collections.Generic;
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
            return ParseMethods(sourceFilePaths);
        }

        private IEnumerable<string> MakeGlobStrings() => Configuration.SourceFileRules
            .Select(r => r.Include);

        private IEnumerable<MethodMetadata> ParseMethods(IEnumerable<string> sourceFilePaths)
        {
            return sourceFilePaths
                .Select(p => new SourceFile(p))
                .SelectMany(f => f.Methods);
        }
    }
}