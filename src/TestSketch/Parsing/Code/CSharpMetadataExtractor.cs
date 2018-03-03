using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using TestSketch.IO.Files.Metadata;

namespace TestSketch.Parsing.Code
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
            public LinkedList<TypeMetadata> ExtractedMetadata { get; } = new LinkedList<TypeMetadata>();

            public override void VisitClassDeclaration(ClassDeclarationSyntax node)
            {
                ExtractTypeMetadata(node);
            }

            public override void VisitStructDeclaration(StructDeclarationSyntax node)
            {
                ExtractTypeMetadata(node);
            }

            private MethodMetadata ExtractMethodMetadata(MethodDeclarationSyntax node)
            {
                var methodName = node.Identifier.Text;
                return new MethodMetadata(methodName);
            }

            private void ExtractTypeMetadata(TypeDeclarationSyntax node)
            {
                var typeName = node.Identifier.Text;
                var methodData = new LinkedList<MethodMetadata>();

                foreach (MethodDeclarationSyntax methodNode in node.Members.Where(m => m is MethodDeclarationSyntax))
                    methodData.AddLast(ExtractMethodMetadata(methodNode));

                ExtractedMetadata.AddLast(new TypeMetadata(typeName, methodData));
            }
        }

        private CSharpMetadataWalker walker = new CSharpMetadataWalker();
    }
}