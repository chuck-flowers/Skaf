using System;
using System.IO;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Formatting;
using Skaf.IO.SourceCode.Metadata;

using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Skaf.IO.SourceCode.Writers
{
    public class TestFileWriter
    {
        public TestFileWriter(string testFilePath)
        {
            TestFilePath = testFilePath ?? throw new ArgumentNullException(nameof(testFilePath));
        }

        public string TestFilePath { get; }

        public void Write(MethodMetadata test)
        {
            //Create the structure
            CompilationUnitSyntax? compilationUnit = null;
            if (File.Exists(TestFilePath))
                compilationUnit = (CompilationUnitSyntax)CSharpSyntaxTree.ParseText(File.ReadAllText(TestFilePath)).GetRoot();

            compilationUnit = CreateCompilationUnit(test, compilationUnit);

            //Write the structure
            var formatted = Formatter.Format(compilationUnit, new AdhocWorkspace());
            Directory.CreateDirectory(Path.GetDirectoryName(TestFilePath) ?? "");
            using (var writer = File.CreateText(TestFilePath))
                formatted.WriteTo(writer);
        }

        private CompilationUnitSyntax CreateCompilationUnit(MethodMetadata test, CompilationUnitSyntax? compilationUnit)
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
            var newNsDeclaration = CreateNameSpaceSyntax(test, oldNsDeclaration);

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
                Identifier(methodMetadata.Name))
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

        private NamespaceDeclarationSyntax CreateNameSpaceSyntax(MethodMetadata test, NamespaceDeclarationSyntax? namespaceDeclaration)
        {
            if (namespaceDeclaration == null)
                namespaceDeclaration = NamespaceDeclaration(IdentifierName(test.ParentType.Namespace));

            var oldMembers = namespaceDeclaration.Members;
            var oldClass = oldMembers
                .OfType<ClassDeclarationSyntax>()
                .FirstOrDefault();
            var newClass = CreateTestClass(test, oldClass);

            var newMembers = oldClass != null ?
                oldMembers.Replace(oldClass, newClass) :
                oldMembers.Add(newClass);

            return namespaceDeclaration.WithMembers(newMembers);
        }

        private ClassDeclarationSyntax CreateTestClass(MethodMetadata test, ClassDeclarationSyntax? classDeclaration)
        {
            //If the class declaration does not already exist, create it
            if (classDeclaration == null)
                classDeclaration = ClassDeclaration(test.ParentType.Name)
                    .WithModifiers(TokenList(Token(SyntaxKind.PublicKeyword)));

            //If the type does not already have a definition for the method, create one
            if (!classDeclaration.Members.OfType<MethodDeclarationSyntax>().Any(d => d.Identifier.Text.Equals(test.Name)))
                classDeclaration = classDeclaration.AddMembers(CreateMethodSyntax(test));

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