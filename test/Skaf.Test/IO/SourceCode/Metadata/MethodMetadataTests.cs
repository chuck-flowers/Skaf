using Skaf.IO.SourceCode.Metadata;
using Xunit;

namespace Skaf.Test.IO.SourceCode.Metadata
{
    public class MethodMetadataTests
    {
        [Fact]
        public void ToStringTest()
        {
            MethodMetadata metadata = new MethodMetadata("Baz", new TypeMetadata("", "Foo", "Bar"));
            Assert.Equal("Foo.Bar.Baz", metadata.ToString());
        }
    }
}