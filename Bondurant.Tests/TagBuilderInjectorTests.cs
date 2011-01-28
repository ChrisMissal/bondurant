using System.Linq;
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

            Assert.That(injector.TagCount, Is.EqualTo(2));
            foreach (var item in injector)
                Assert.AreSame(item, tagBuilder);
        }

        [Test]
        public void AddPrerequisite_multiple_times_should_not_duplicate_Prerequisites()
        {
            var tagBuilder = new TagBuilder("script");
            var injector = GetTagBuilderInjector();

            injector.AddPrerequisite(tagBuilder);
            injector.AddPrerequisite(tagBuilder);

            Assert.That(injector.PrerequisiteCount, Is.EqualTo(1));
            Assert.That(injector.PrerequisiteCount, Is.EqualTo(1));
        }

        private static TagBuilderInjector GetTagBuilderInjector()
        {
            return new TagBuilderInjector();
        }
    }
}