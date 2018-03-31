using System.Text.RegularExpressions;
using Newtonsoft.Json;
using Skaf.IO.SourceCode.Metadata;

using static System.IO.Path;

namespace Skaf.IO.Config.Map.Rules
{
    public class MethodMatcher
    {
        [JsonProperty("method")]
        public string Method { get; set; } = "*";

        [JsonProperty("namespace")]
        public string Namespace { get; set; } = "**";

        [JsonProperty("path")]
        public string Path { get; set; } = "**";

        [JsonProperty("type")]
        public string Type { get; set; } = "*";

        public bool Matches(MethodMetadata method)
        {
            var pathRegex = CreatePathRegex(Path);
            var namespaceRegex = CreateNamespaceRegex(Namespace);
            var typeRegex = CreateTypeRegex(Type);
            var methodRegex = CreateMethodRegex(Method);

            var pathPass = pathRegex.IsMatch(method.ParentType.Path);
            var namespacePass = namespaceRegex.IsMatch(method.ParentType.Namespace);
            var typePass = typeRegex.IsMatch(method.ParentType.Name);
            var methodPass = methodRegex.IsMatch(method.Name);

            return pathPass && namespacePass && typePass && methodPass;
        }

        private Regex CreateMethodRegex(string pattern)
        {
            pattern = Regex.Escape(pattern)
                .Replace(@"\*", ".*");
            return MatchWholeString(pattern);
        }

        private Regex CreateNamespaceRegex(string pattern)
        {
            pattern = Regex.Escape(pattern)
                .Replace(@"\*\*", ".*")
                .Replace(@"\*", @"[^\.]*");
            return MatchWholeString(pattern);
        }

        private Regex CreatePathRegex(string pattern)
        {
            pattern = Regex.Escape(pattern)
                .Replace(@"\*\*", ".*")
                .Replace(@"\*", @"[^" + Regex.Escape(DirectorySeparatorChar.ToString()) + "]*");
            return MatchWholeString(pattern);
        }

        private Regex CreateTypeRegex(string pattern)
        {
            pattern = Regex.Escape(pattern)
                .Replace(@"\*", ".*");
            return MatchWholeString(pattern);
        }

        private Regex MatchWholeString(string pattern) => new Regex("^" + pattern + "$");
    }
}