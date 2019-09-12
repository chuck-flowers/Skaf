using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Skaf.IO.SourceCode;
using Skaf.IO.SourceCode.Metadata;

namespace Skaf.Parsing.Code
{
    public class CSharpMetadataExtractor : IMetadataExtractor
    {
        public IEnumerable<MethodMetadata> ExtractedMetadata => walker.ExtractedMetadata;

        public void ProcessCodeFile(CodeFile code)
        {
            var codeText = File.ReadAllText(code.Path);
            var root = (CompilationUnitSyntax)CSharpSyntaxTree.ParseText(codeText).GetRoot();
            walker.CodeFile = code;
            walker.Visit(root);
        }

        private class CSharpMetadataWalker : CSharpSyntaxWalker
        {
            public CodeFile? CodeFile { get; set; }

            public IEnumerable<MethodMetadata> ExtractedMetadata => extractedMetadata;

            public override void VisitNamespaceDeclaration(NamespaceDeclarationSyntax node)
            {
                foreach (var t in node.Members.OfType<TypeDeclarationSyntax>())
                    ExtractTypeMetadata(node.Name.ToString(), t);
            }

            private MethodMetadata ExtractMethodMetadata(MethodDeclarationSyntax node, TypeMetadata parentType)
            {
                var methodName = node.Identifier.Text;
                return new MethodMetadata(methodName, parentType);
            }

            private void ExtractTypeMetadata(string namespaceText, TypeDeclarationSyntax node)
            {
                //If the type isn't public, don't extract metadata for it
                if (!node.Modifiers.Any(mod => mod.Kind() == SyntaxKind.PublicKeyword))
                    return;

                var typeName = node.Identifier.Text;
                var typeMetadata = new TypeMetadata(CodeFile!.Path, namespaceText, typeName);

                var publicMethods = node.Members
                    .OfType<MethodDeclarationSyntax>()
                    .Where(m => m.Modifiers.Any(mod => mod.Kind() == SyntaxKind.PublicKeyword));
                foreach (MethodDeclarationSyntax methodNode in publicMethods)
                    extractedMetadata.AddLast(ExtractMethodMetadata(methodNode, typeMetadata));
            }

            private readonly LinkedList<MethodMetadata> extractedMetadata = new LinkedList<MethodMetadata>();
        }

        private readonly CSharpMetadataWalker walker = new CSharpMetadataWalker();
    }
}