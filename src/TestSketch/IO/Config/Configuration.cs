using System.Collections.Generic;
using Newtonsoft.Json;

namespace TestSketch.IO.Config
{
    /// <summary>
    /// Specifies the options that instruct the program how to
    /// generate test files from source files
    /// </summary>
    internal class Configuration
    {
        [JsonProperty("src")]
        private IEnumerable<string> SourceFilePaths { get; set; }
    }
}