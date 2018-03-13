using System;
using System.Collections.ObjectModel;

namespace Skaf.Parsing.Code
{
    internal static class MetadataExtractorFactory
    {
        public static IMetadataExtractor GetExtractor(string extension)
        {
            if (CSharpExtensions.Contains(extension))
                return new CSharpMetadataExtractor();
            else if (VisualBasicExtensions.Contains(extension))
                throw new Exception($"Visual Basic is not yet supported by {nameof(Skaf)}");

            throw new Exception($"The extension '{extension}' was unrecognized.");
        }

        private static readonly Collection<string> CSharpExtensions = new Collection<string>()
        {
            ".cs"
        };

        private static readonly Collection<string> VisualBasicExtensions = new Collection<string>()
        {
            ".vb"
        };
    }
}