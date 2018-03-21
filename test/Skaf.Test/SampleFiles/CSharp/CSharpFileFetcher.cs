using System.IO;
using System.Runtime.CompilerServices;

namespace Skaf.Test.SampleFiles.CSharp
{
    internal static class CSharpFileFetcher
    {
        public static string GetSampleFile(string fileName) =>
            GetSampleCodeContent(fileName);

        private static string GetSampleCodeContent(string fileName, [CallerFilePath] string thisFilePath = "")
        {
            string thisDirectory = Path.GetDirectoryName(thisFilePath);
            return Path.Combine(thisDirectory, fileName);
        }
    }
}