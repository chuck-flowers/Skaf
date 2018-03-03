using System.Collections.Generic;

namespace TestSketch.IO.Files.Metadata
{
    /// <summary>
    /// Represents the pertinent metadata about a type extracted
    /// from a code file
    /// </summary>
    public class TypeMetadata
    {
        /// <summary>
        /// Constructs type metadata with the given name and method metadata
        /// </summary>
        /// <param name="name">The name of the type</param>
        /// <param name="methods">The metadata for each of the methods within the type</param>
        public TypeMetadata(string name, IEnumerable<MethodMetadata> methods)
        {
            Name = name;
            Methods = methods;
        }

        /// <summary>
        /// Returns the metadata for each of the methods contained
        /// within the type
        /// </summary>
        public IEnumerable<MethodMetadata> Methods { get; }

        /// <summary>
        /// Returns the name of the type
        /// </summary>
        public string Name { get; }
    }
}