using System.Web.Mvc;
using NUnit.Framework;

namespace Bondurant.Tests
{
    [TestFixture]
    public class TagBuilderInjectorTests
    {
        [Test]
        public void Inject_should_add_the_the_enumeration()
        {
            var tagBuilder = new TagBuilder("script");
            var injector = GetTagBuilderInjector();
            injector.Inject(tagBuilder);
            injector.Inject(tagBuilder);

            Assert.That(injector.Count, Is.EqualTo(2));
            foreach (var item in injector)
                Assert.AreSame(item, tagBuilder);
        }

        private static TagBuilderInjector GetTagBuilderInjector()
        {
            return new TagBuilderInjector();
        }
    }
}