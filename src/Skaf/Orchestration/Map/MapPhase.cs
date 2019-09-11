using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Skaf.IO.Config.Map;
using Skaf.IO.SourceCode.Metadata;

namespace Skaf.Orchestration.Map
{
    /// <summary>
    /// The phase of the process that is responsible for mapping the methods within a source file to
    /// a test method within a test file.
    /// </summary>
    public class MapPhase
    {
        /// <summary>
        /// Creates the a <see cref="MapPhase"/> that maps a set of <paramref name="srcMethods"/>.
        /// </summary>
        /// <param name="srcMethods">The source methods for which to generate test methods.</param>
        /// <param name="mapConfig">
        /// The configuration that specifies how to transform the source methods into the test methods.
        /// </param>
        public MapPhase(IEnumerable<MethodMetadata> srcMethods, MapConfiguration mapConfig)
        {
            MapConfig = mapConfig ?? throw new ArgumentNullException(nameof(mapConfig));
            SourceMethods = srcMethods ?? throw new ArgumentNullException(nameof(srcMethods));
        }

        /// <summary>
        /// The configuration specifying how to transform the source methods into test methods.
        /// </summary>
        public MapConfiguration MapConfig { get; }

        /// <summary>
        /// The source methods to transform into test methods.
        /// </summary>
        public IEnumerable<MethodMetadata> SourceMethods { get; }

        /// <summary>
        /// Executes the mapping operation for the methods.
        /// </summary>
        /// <returns>
        /// A tuple that contains the source method and the test method that it is mapped to.
        /// </returns>
        public IEnumerable<(MethodMetadata srcMethod, MethodMetadata testMethod)> Execute()
        {
            var testProjectRoot = Path.Combine("..", "..", "test");
            testProjectRoot = Path.GetFullPath(testProjectRoot);

            return SourceMethods.Select(m => (m, MakeTestMethod(m)));
        }

        /// <summary>
        /// Generates a test method for a given source method.
        /// </summary>
        /// <param name="sourceMethod">The source method for which to generate the test method.</param>
        /// <returns>The generated test method.</returns>
        private MethodMetadata MakeTestMethod(MethodMetadata sourceMethod)
        {
            TypeMetadata srcType = sourceMethod.ParentType;

            // Generates the new values for the test method
            var testPath = srcType.Path;
            var testNamespace = srcType.Namespace;
            var testTypeName = $"{srcType.Name}Tests";
            var testName = $"{sourceMethod.Name}Test";

            var testType = new TypeMetadata(testPath, testNamespace, testTypeName);
            return new MethodMetadata(testName, testType);
        }
    }
}