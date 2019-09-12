using Newtonsoft.Json;

namespace Skaf.IO.Config.Generate
{
    public class GenerateConfiguration
    {
        [JsonProperty("root", Required = Required.Always)]
        public string Root { get; set; }
    }
}