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
        /// <summary>
        /// Represents the array of glob patterns that the user
        /// supplies as input to TestSketch
        /// </summary>
        [JsonProperty("src")]
        private IEnumerable<string> SourceFilePaths { get; set; }
    }
}