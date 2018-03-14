using System.IO;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Formatting;
using Skaf.IO.Files.Metadata;

using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Skaf.IO.Files.Writers
{
    public class TestFileWriter
    {
        public TestFileWriter(TypeMetadata type, TestFile testFile)
        {
            Type = type;
            TestFile = testFile;
        }

        public TestFile TestFile { get; }

        public TypeMetadata Type { get; }

        public void Write()
        {
            //Create the structure
            var compilationUnit = CreateCompilationUnit();

            //Write the structure
            var formatted = Formatter.Format(compilationUnit, new AdhocWorkspace());
            Directory.CreateDirectory(TestFile.Directory);
            using (var writer = File.CreateText(TestFile.Path))
                formatted.WriteTo(writer);
        }

        private CompilationUnitSyntax CreateCompilationUnit() =>
            CompilationUnit()
                .AddUsings(
                    UsingDirective(IdentifierName("System")),
                    UsingDirective(IdentifierName("System.Collections")),
                    UsingDirective(IdentifierName("System.Collections.Generic"))
                ).AddMembers(
                    CreateNameSpaceSyntax()
                );

        private MethodDeclarationSyntax CreateMethodSyntax(MethodMetadata methodMetadata) =>
            MethodDeclaration(PredefinedType(Token(SyntaxKind.VoidKeyword)), Identifier(methodMetadata.Name + "Test"))
                .AddBodyStatements(CreateThrowStatementSyntax());

        private NamespaceDeclarationSyntax CreateNameSpaceSyntax() =>
            NamespaceDeclaration(IdentifierName(Type.Namespace))
                .AddMembers(CreateTestClass());

        private ClassDeclarationSyntax CreateTestClass()
        {
            var c = ClassDeclaration(Type.Name + "Tests")
                .AddModifiers(Token(SyntaxKind.PublicKeyword));

            foreach (var m in Type.Methods)
                c = c.AddMembers(CreateMethodSyntax(m));

            return c;
        }

        private ThrowStatementSyntax CreateThrowStatementSyntax() =>
            ThrowStatement(ObjectCreationExpression(IdentifierName("NotImplementedException")));
    }
}