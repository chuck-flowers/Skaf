using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Skaf.Parsing.Config
{
    /// <summary>
    /// Responsible for resolving pattern paths into file names.
    /// </summary>
    public static class Globber
    {
        /// <summary>
        /// Finds all the paths within a directory that matches the given pattern
        /// </summary>
        /// <param name="baseDir">The directory to search for the files within</param>
        /// <param name="pattern">The pattern that is used to match files within the baseDir</param>
        /// <returns>Each of the files that matches the pattern</returns>
        public static IEnumerable<string> ExpandPath(string baseDir, string pattern) =>
            ExpandPath(baseDir, new string[] { pattern });

        /// <summary>
        /// Finds all the paths within a directory that matches one of the given patterns
        /// </summary>
        /// <param name="baseDir">The directory to search for the files within</param>
        /// <param name="pattern">The patterns that are used to match files within the baseDir</param>
        /// <returns>Each of the files that matches one of the patterns</returns>
        public static IEnumerable<string> ExpandPath(string baseDir, IEnumerable<string> patterns) =>
            patterns.SelectMany(p => ResolveGlob(baseDir, p));

        private static IEnumerable<string> ResolveGlob(string baseDir, string glob) =>
            Directory.EnumerateFiles(baseDir, glob, SearchOption.AllDirectories).Select(p => Path.GetRelativePath(baseDir, p));
    }
}