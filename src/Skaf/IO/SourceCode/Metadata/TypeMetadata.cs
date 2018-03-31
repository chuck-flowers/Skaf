namespace Skaf.IO.SourceCode.Metadata
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
        public TypeMetadata(string path, string namespaceName, string typeName)
        {
            Path = path;
            Namespace = namespaceName;
            Name = typeName;
        }

        /// <summary>
        /// Returns the name of the type
        /// </summary>
        public string Name { get; }

        public string Namespace { get; }

        public string Path { get; }
    }
}