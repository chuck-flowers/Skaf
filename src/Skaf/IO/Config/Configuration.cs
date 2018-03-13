using System.Collections.Generic;
using Newtonsoft.Json;

namespace Skaf.IO.Config
{
    /// <summary>
    /// Specifies the options that instruct the program how to
    /// generate test files from source files
    /// </summary>
    public class Configuration
    {
        /// <summary>
        /// Represents the array of glob patterns that the user
        /// supplies as input to TestSketch
        /// </summary>
        [JsonProperty("src")]
        public IEnumerable<string> SourceFilePaths { get; set; }
    }
}