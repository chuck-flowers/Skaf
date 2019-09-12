using System.Collections.Generic;
using Newtonsoft.Json;

namespace Skaf.IO.Config.Input
{
    public class InputConfiguration
    {
        [JsonProperty("files")]
        public IEnumerable<SourceFileRule>? SourceFileRules { get; set; }

        [JsonProperty("src")]
        public string? SourcePath { get; set; }
    }
}