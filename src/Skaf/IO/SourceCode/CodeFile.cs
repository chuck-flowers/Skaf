using System.Collections.Generic;
using System.IO;
using Skaf.IO.SourceCode.Metadata;
using Skaf.Parsing.Code;

namespace Skaf.IO.SourceCode
{
    /// <summary>
    /// Represents a file that contains source code
    /// </summary>
    public class CodeFile
    {
        /// <summary>
        /// Constructs a representation of a code file that is located at the
        /// specified path.
        /// </summary>
        /// <param name="path"></param>
        public CodeFile(string path) => Path = path;

        /// <summary>
        /// The name of the directory the file is located in (path without
        /// the file name)
        /// </summary>
        public string Directory => System.IO.Path.GetDirectoryName(Path);

        /// <summary>
        /// The name of the file without the directory
        /// </summary>
        public string Name => System.IO.Path.GetFileName(Path);

        /// <summary>
        /// The full path to the file
        /// </summary>
        public string Path { get; }

        /// <summary>
        /// All the TypeMetadata contained within the specified code file.
        /// The data is lazy loaded from the file.
        /// </summary>
        public IEnumerable<TypeMetadata> Types => types ?? (types = ExtractTypes());

        /// <summary>
        /// Extracts the TypeMetadata from the file
        /// </summary>
        /// <returns>Each of the instances of TypeMetadata from each type contained within the file</returns>
        private IEnumerable<TypeMetadata> ExtractTypes()
        {
            string extension = System.IO.Path.GetExtension(Path);
            IMetadataExtractor extractor = MetadataExtractorFactory.GetExtractor(extension);
            extractor.ProcessCodeFile(File.ReadAllText(Path));
            return extractor.ExtractedMetadata;
        }

        private IEnumerable<TypeMetadata> types = null;
    }
}