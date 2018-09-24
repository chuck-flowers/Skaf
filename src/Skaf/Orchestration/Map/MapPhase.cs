using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Skaf.IO.Config.Map;
using Skaf.IO.SourceCode.Metadata;

namespace Skaf.Orchestration.Map
{
    public class MapPhase
    {
        public MapPhase(IEnumerable<MethodMetadata> types, MapConfiguration mapConfig)
        {
            MapConfig = mapConfig ?? throw new ArgumentNullException(nameof(mapConfig));
            Types = types ?? throw new ArgumentNullException(nameof(types));
        }

        public MapConfiguration MapConfig { get; }

        public IEnumerable<MethodMetadata> Types { get; }

        public IEnumerable<(MethodMetadata, MethodMetadata)> Execute()
        {
            var testProjectRoot = Path.Combine("..", "..", "test");
            testProjectRoot = Path.GetFullPath(testProjectRoot);

            return Types.Select(m => (m, MakeTestMethod(m)));
        }

        private MethodMetadata MakeTestMethod(MethodMetadata sourceMethod)
        {
            foreach (var rule in MapConfig.MappingRules)
                if (rule.Input.Matches(sourceMethod))
                    return rule.Output.GenerateTest(sourceMethod);

            //TODO: Throw exception
            return null;
        }
    }
}