using Newtonsoft.Json;

namespace Skaf.IO.Config.Map.Rules
{
    public class MappingRule
    {
        [JsonProperty("input")]
        public MethodMatcher Input { get; set; } = new MethodMatcher();

        [JsonProperty("output")]
        public TestGenerator Output { get; set; } = new TestGenerator();
    }
}