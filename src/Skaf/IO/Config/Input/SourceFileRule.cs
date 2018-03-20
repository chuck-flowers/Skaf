using Newtonsoft.Json;

namespace Skaf.IO.Config.Input
{
    public class SourceFileRule
    {
        [JsonProperty("include")]
        public string Include { get; set; }
    }
}