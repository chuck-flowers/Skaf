using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Skaf.IO.SourceCode.Metadata;

namespace Skaf.Parsing.Code
{
    public class CSharpMetadataExtractor : IMetadataExtractor
    {
        public IEnumerable<TypeMetadata> ExtractedMetadata => walker.ExtractedMetadata;

        public void ProcessCodeFile(string code)
        {
            var root = (CompilationUnitSyntax)CSharpSyntaxTree.ParseText(code).GetRoot();
            walker.Visit(root);
        }

        private class CSharpMetadataWalker : CSharpSyntaxWalker
        {
            public IEnumerable<TypeMetadata> ExtractedMetadata => extractedMetadata;

            public override void VisitNamespaceDeclaration(NamespaceDeclarationSyntax node)
            {
                foreach (var t in node.Members.OfType<TypeDeclarationSyntax>())
                    ExtractTypeMetadata(node.Name.ToString(), t);
            }

            private MethodMetadata ExtractMethodMetadata(MethodDeclarationSyntax node)
            {
                var methodName = node.Identifier.Text;
                return new MethodMetadata(methodName);
            }

            private void ExtractTypeMetadata(string namespaceText, TypeDeclarationSyntax node)
            {
                //If the type isn't public, don't extract metadata for it
                if (!node.Modifiers.Any(mod => mod.Kind() == SyntaxKind.PublicKeyword))
                    return;

                var typeName = node.Identifier.Text;
                var methodData = new LinkedList<MethodMetadata>();

                var publicMethods = node.Members
                    .OfType<MethodDeclarationSyntax>()
                    .Where(m => m.Modifiers.Any(mod => mod.Kind() == SyntaxKind.PublicKeyword));
                foreach (MethodDeclarationSyntax methodNode in publicMethods)
                    methodData.AddLast(ExtractMethodMetadata(methodNode));

                extractedMetadata.AddLast(new TypeMetadata(namespaceText, typeName, methodData));
            }

            private LinkedList<TypeMetadata> extractedMetadata = new LinkedList<TypeMetadata>();
        }

        private CSharpMetadataWalker walker = new CSharpMetadataWalker();
    }
}