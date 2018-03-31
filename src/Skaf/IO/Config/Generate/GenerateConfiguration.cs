using Newtonsoft.Json;

namespace Skaf.IO.Config.Generate
{
    public class GenerateConfiguration
    {
        [JsonProperty("root")]
        public string Root { get; set; } = "../../test";
    }
}