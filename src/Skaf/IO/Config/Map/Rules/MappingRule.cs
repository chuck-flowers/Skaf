using Newtonsoft.Json;

namespace Skaf.IO.Config.Map.Rules
{
    public class MappingRule
    {
        [JsonProperty("input")]
        public TypeMatcher Input { get; set; }

        [JsonProperty("output")]
        public TestGenerator Output { get; set; }
    }
}