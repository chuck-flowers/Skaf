using System;
using System.IO;
using System.Linq;
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
            CompilationUnitSyntax compilationUnit = null;
            if (File.Exists(TestFile.Path))
                compilationUnit = (CompilationUnitSyntax)CSharpSyntaxTree.ParseText(File.ReadAllText(TestFile.Path)).GetRoot();

            compilationUnit = CreateCompilationUnit(compilationUnit);

            //Write the structure
            var formatted = Formatter.Format(compilationUnit, new AdhocWorkspace());
            Directory.CreateDirectory(TestFile.Directory);
            using (var writer = File.CreateText(TestFile.Path))
                formatted.WriteTo(writer);
        }

        private CompilationUnitSyntax CreateCompilationUnit(CompilationUnitSyntax compilationUnit)
        {
            if (compilationUnit == null)
                compilationUnit = CompilationUnit();

            var oldUsings = compilationUnit.Usings;
            var newUsings = oldUsings;

            bool uSystem, uXunit;
            uSystem = uXunit = false;

            //Detect usings
            foreach (var u in oldUsings)
            {
                var ns = u.Name.GetText().ToString();

                switch (ns)
                {
                    case "System":
                        uSystem = true;
                        break;

                    case "Xunit":
                        uXunit = true;
                        break;
                }
            }

            //Create missing usings
            if (!uSystem)
            {
                newUsings = newUsings.Add(
                    UsingDirective(IdentifierName("System"))
                );
            }

            if (!uXunit)
            {
                newUsings = newUsings.Add(
                    UsingDirective(IdentifierName("Xunit"))
                );
            }

            var oldMembers = compilationUnit.Members;
            var oldNsDeclaration = oldMembers
                .OfType<NamespaceDeclarationSyntax>()
                .FirstOrDefault();
            var newNsDeclaration = CreateNameSpaceSyntax(oldNsDeclaration);

            var newMembers = oldNsDeclaration != null ?
                oldMembers.Replace(oldNsDeclaration, newNsDeclaration) :
                oldMembers.Add(newNsDeclaration);

            return compilationUnit
                .WithUsings(newUsings)
                .WithMembers(newMembers);
        }

        private MethodDeclarationSyntax CreateMethodSyntax(MethodMetadata methodMetadata)
        {
            return MethodDeclaration(
                PredefinedType(Token(SyntaxKind.VoidKeyword)),
                Identifier(methodMetadata.Name + "Test"))
                    .AddAttributeLists(
                        AttributeList(
                            SingletonSeparatedList(
                                Attribute(IdentifierName("Fact"))
                            )
                        )
                    ).AddBodyStatements(
                        CreateThrowStatementSyntax()
                ).WithModifiers(TokenList(Token(SyntaxKind.PublicKeyword)));
        }

        private NamespaceDeclarationSyntax CreateNameSpaceSyntax(NamespaceDeclarationSyntax namespaceDeclaration)
        {
            if (namespaceDeclaration == null)
                namespaceDeclaration = NamespaceDeclaration(IdentifierName(Type.Namespace));

            var oldMembers = namespaceDeclaration.Members;
            var oldClass = oldMembers
                .OfType<ClassDeclarationSyntax>()
                .FirstOrDefault();
            var newClass = CreateTestClass(oldClass);

            var newMembers = oldClass != null ?
                oldMembers.Replace(oldClass, newClass) :
                oldMembers.Add(newClass);

            return namespaceDeclaration.WithMembers(newMembers);
        }

        private ClassDeclarationSyntax CreateTestClass(ClassDeclarationSyntax classDeclaration)
        {
            if (classDeclaration == null)
                classDeclaration = ClassDeclaration(Type.Name + "Tests")
                    .WithModifiers(TokenList(Token(SyntaxKind.PublicKeyword)));

            foreach (var m in Type.Methods)
            {
                if (classDeclaration.Members.OfType<MethodDeclarationSyntax>().Any(d => d.Identifier.Text.Equals(m.Name + "Test")))
                    continue;
                else
                    classDeclaration = classDeclaration.AddMembers(CreateMethodSyntax(m));
            }

            return classDeclaration;
        }

        private ThrowStatementSyntax CreateThrowStatementSyntax()
        {
            return ThrowStatement(
                ObjectCreationExpression(
                    IdentifierName(
                        nameof(NotImplementedException))
                    ).WithArgumentList(
                        ArgumentList()
                    )
                );
        }
    }
}