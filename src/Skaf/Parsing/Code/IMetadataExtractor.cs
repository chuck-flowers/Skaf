using System.Collections.Generic;
using Skaf.IO.SourceCode.Metadata;

namespace Skaf.Parsing.Code
{
    internal interface IMetadataExtractor
    {
        IEnumerable<TypeMetadata> ExtractedMetadata { get; }

        void ProcessCodeFile(string path);
    }
}