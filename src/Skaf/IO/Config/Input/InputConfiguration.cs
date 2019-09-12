using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Skaf.IO.Config.Input
{
    public class InputConfiguration
    {
        [JsonProperty("files")]
        public IEnumerable<SourceFileRule> SourceFileRules { get; set; } = Enumerable.Empty<SourceFileRule>();

        [JsonProperty("src")]
        public string SourcePath { get; set; } = "";
    }
}