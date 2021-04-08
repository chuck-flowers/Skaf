using Newtonsoft.Json;
using Skaf.IO.Config.Generate;
using Skaf.IO.Config.Input;
using Skaf.IO.Config.Map;

namespace Skaf.IO.Config
{
    /// <summary>
    /// Specifies the options that instruct the program how to
    /// generate test files from source files
    /// </summary>
    public class Configuration
    {
        [JsonProperty("generate", Required = Required.Always)]
        public GenerateConfiguration GenerateConfig { get; set; } = new GenerateConfiguration();

        /// <summary>
        /// Represents the array of glob patterns that the user
        /// supplies as input to TestSketch
        /// </summary>
        [JsonProperty("input")]
        public InputConfiguration InputConfig { get; set; } = new InputConfiguration();

        [JsonProperty("map")]
        public MapConfiguration MapConfig { get; set; } = new MapConfiguration();
    }
}