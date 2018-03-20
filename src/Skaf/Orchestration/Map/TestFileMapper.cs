using System.Collections.Generic;
using System.IO;
using Skaf.IO.SourceCode.Metadata;

namespace Skaf.IO.SourceCode.Mapping
{
    /// <summary>
    /// The class that is responsible for creating the test
    /// file that corresponds to a given type
    /// </summary>
    public class TestFileMapper
    {
        /// <summary>
        /// Creates a mapper that is based from a given
        /// directory
        /// </summary>
        /// <param name="baseDir">The directory to base all mappings from</param>
        public TestFileMapper(string baseDir)
        {
            BaseDirectory = baseDir;
        }

        /// <summary>
        /// The base directory that all file paths are relative to
        /// </summary>
        public string BaseDirectory { get; }

        /// <summary>
        /// Returns the test file that is associated with the
        /// given type
        /// </summary>
        /// <param name="type">The type to determine the test file for</param>
        /// <returns>The test file that should contain the tests for the specified type</returns>
        public TestFile MapTypeToTestFile(TypeMetadata type)
        {
            List<string> paths = new List<string>();
            paths.Add(BaseDirectory);
            paths.AddRange(type.Namespace.Split('.'));
            paths.Add(type.Name + "Tests.cs");

            string pathToTestFile = Path.Combine(paths.ToArray());
            return new TestFile(pathToTestFile);
        }
    }
}