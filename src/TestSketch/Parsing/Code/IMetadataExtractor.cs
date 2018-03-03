using System.Collections.Generic;
using TestSketch.IO.Files.Metadata;

namespace TestSketch.Parsing.Code
{
    internal interface IMetadataExtractor
    {
        IEnumerable<TypeMetadata> ExtractedMetadata { get; }

        void ProcessCodeFile(string path);
    }
}