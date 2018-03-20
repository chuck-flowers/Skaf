using System;
using Newtonsoft.Json;
using Skaf.IO.SourceCode.Metadata;

namespace Skaf.IO.Config.Map.Rules
{
    public class TestGenerator
    {
        [JsonProperty("method")]
        public string Method { get; set; }

        [JsonProperty("namespace")]
        public string Namespace { get; set; }

        [JsonProperty("path")]
        public string Path { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        public MethodMetadata GenerateTest()
        {
            throw new NotImplementedException();
        }
    }
}