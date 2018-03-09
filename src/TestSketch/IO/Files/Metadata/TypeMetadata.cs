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
        /// <param name="namespaceName">The name of the namespace that the type is contained within</param>
        /// <param name="typeName">The name of the type</param>
        /// <param name="methods">The metadata for each of the methods within the type</param>
        public TypeMetadata(string namespaceName, string typeName, IEnumerable<MethodMetadata> methods)
        {
            Namespace = namespaceName;
            Name = typeName;
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

        public string Namespace { get; }
    }
}