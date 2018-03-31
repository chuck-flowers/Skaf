using System.Collections.Generic;
using Skaf.IO.SourceCode;
using Skaf.IO.SourceCode.Metadata;

namespace Skaf.Parsing.Code
{
    internal interface IMetadataExtractor
    {
        IEnumerable<MethodMetadata> ExtractedMetadata { get; }

        void ProcessCodeFile(CodeFile path);
    }
}