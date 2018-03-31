using DotLiquid;
using Newtonsoft.Json;
using Skaf.IO.SourceCode.Metadata;

namespace Skaf.IO.Config.Map.Rules
{
    public class TestGenerator
    {
        [JsonProperty("method")]
        public string Method { get; set; } = "{{method}}Test";

        [JsonProperty("namespace")]
        public string Namespace { get; set; } = "{{namespace}}";

        [JsonProperty("path")]
        public string Path { get; set; } = "{{namespace | replace: \"\\.\", \"/\"}}.{{type}}Tests.cs";

        [JsonProperty("type")]
        public string Type { get; set; } = "{{type}}Tests";

        public MethodMetadata GenerateTest(MethodMetadata sourceMethod)
        {
            var pathTemplate = Template.Parse(Path);
            var namespaceTemplate = Template.Parse(Namespace);
            var typeTemplate = Template.Parse(Type);
            var methodTemplate = Template.Parse(Method);

            var methodInput = new
            {
                path = sourceMethod.ParentType.Path,
                @namespace = sourceMethod.ParentType.Namespace,
                type = sourceMethod.ParentType.Name,
                method = sourceMethod.Name
            };

            var hash = Hash.FromAnonymousObject(methodInput);
            var path = pathTemplate.Render(hash);
            var @namespace = namespaceTemplate.Render(hash);
            var typeName = typeTemplate.Render(hash);
            var methodName = methodTemplate.Render(hash);

            return new MethodMetadata(methodName, new TypeMetadata(path, @namespace, typeName));
        }
    }
}