namespace Skaf.IO.SourceCode.Metadata
{
    /// <summary>
    /// Represents all the metadata for a method that is
    /// extracted from a code file
    /// </summary>
    public class MethodMetadata
    {
        /// <summary>
        /// Constructs method metadata from the given name
        /// </summary>
        /// <param name="name">The name of the method</param>
        public MethodMetadata(string name)
        {
            Name = name;
        }

        /// <summary>
        /// The name of the method
        /// </summary>
        public string Name { get; }
    }
}