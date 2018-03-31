using System.Collections.Generic;
using Newtonsoft.Json;
using Skaf.IO.Config.Map.Rules;

namespace Skaf.IO.Config.Map
{
    public class MapConfiguration
    {
        [JsonProperty("rules")]
        public IEnumerable<MappingRule> MappingRules { get; set; } = new List<MappingRule>() { new MappingRule() };
    }
}