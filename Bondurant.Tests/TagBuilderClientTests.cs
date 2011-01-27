using NUnit.Framework;

namespace Bondurant.Tests
{
    [TestFixture]
    public class TagBuilderClientTests
    {
        [TestCase(null)]
        [TestCase("")]
        [TestCase("   ")]
        public void CreateTag_should_return_null_if_innerHtml_is_invalid(string tag)
        {
            var tagBuilderClient = GetTagBuilderClient();
            var result = tagBuilderClient.CreateTag<TestClient>(x => tag);

            Assert.Null(result);
        }

        [TestCase("tag")]
        [TestCase("result")]
        public void CreateTag_should_return_result_as_innerHtml_is_invalid(string tag)
        {
            var tagBuilderClient = GetTagBuilderClient();
            var result = tagBuilderClient.CreateTag<TestClient>(x => tag);

            Assert.That(result.InnerHtml, Is.EqualTo(tag));
        }

        private static TestTagBuilderClient GetTagBuilderClient()
        {
            return new TestTagBuilderClient();
        }
    }
}