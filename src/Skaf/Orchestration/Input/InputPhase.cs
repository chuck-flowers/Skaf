using System.Collections.Generic;
using System.IO;
using System.Linq;
using Skaf.IO.Config.Input;
using Skaf.IO.Files;
using Skaf.IO.Files.Metadata;
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

        public IEnumerable<TypeMetadata> Execute()
        {
            ICollection<string> globStrings = new List<string>();
            foreach (var rule in Configuration.SourceFileRules)
            {
                string baseGlobPath = Configuration.SourcePath;
                string fullGlobString = Path.Combine(baseGlobPath, rule.Include);
                globStrings.Add(fullGlobString);
            }

            return Globber.ExpandPath(BaseDirectory, globStrings)
                .Select(p => new SourceFile(p))
                .SelectMany(t => t.Types);
        }
    }
}