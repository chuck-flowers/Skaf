using System.IO;
using System.Runtime.CompilerServices;

namespace Skaf.Test.SampleFiles.CSharp
{
    internal static class CSharpFileFetcher
    {
        public static string GetSampleCode(string fileName) =>
            GetSampleCodeContent(fileName);

        private static string GetSampleCodeContent(string fileName, [CallerFilePath] string thisFilePath = "")
        {
            string thisDirectory = Path.GetDirectoryName(thisFilePath);
            return File.ReadAllText(Path.Combine(thisDirectory, fileName));
        }
    }
}