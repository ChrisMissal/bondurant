using System;
using System.Web.Mvc;
using NSubstitute;
using NUnit.Framework;

namespace Bondurant.Tests
{
    [TestFixture]
    public class IntegrationServiceTests
    {
        private ITagBuilderInjector testTagBuilderInjector;
        private IClient testClient;
        private IClient anotherTestClient;
        private ITagBuilderInjector tagBuilderInjector;

        [SetUp]
        public void SetUp()
        {
            tagBuilderInjector = new TagBuilderInjector();
            testTagBuilderInjector = Substitute.For<ITagBuilderInjector>();
            testClient = Substitute.For<TestClient>();
            anotherTestClient = Substitute.For<AnotherTestClient>();
        }

        [Test]
        public void AddScript_should_not_Inject_empty_TagBuilder()
        {
            var tagBuilder = new TagBuilder("script");
            var service = GetTestIntegrationService();
            service.AddScript(tagBuilder);
            testTagBuilderInjector.DidNotReceive().Inject(tagBuilder);
        }

        [Test]
        public void AddScript_should_Inject_non_empty_TagBuilder()
        {
            var tagBuilder = new TagBuilder("script") { InnerHtml = "var x = 1;" };
            var service = GetTestIntegrationService();
            service.AddScript(tagBuilder);
            testTagBuilderInjector.Received().Inject(tagBuilder);
        }

        [Test]
        public void NotifyAll_should_not_call_CreateTag_for_Clients_not_of_the_type()
        {
            var service = GetTestIntegrationService();
            service.NotifyAll<TestClient>(c => { });
            anotherTestClient.DidNotReceive().CreateTag(Arg.Any<Func<AnotherTestClient, string>>());
        }

        [Test]
        public void NotifyAll_should_call_CreateTag_for_Clients_of_the_type()
        {
            var service = GetTestIntegrationService();
            service.NotifyAll<TestClient>(c => { });
            testClient.DidNotReceive().CreateTag(Arg.Any<Func<TestClient, string>>());
        }

        [Test]
        public void ScriptBlock_should_join_tags()
        {
            const string tagName = "script";
            var builder1 = new TagBuilder(tagName) { InnerHtml = "function one() {}" };
            var builder2 = new TagBuilder(tagName) { InnerHtml = "function two() {}" };
            var service = GetIntegrationService();

            tagBuilderInjector.Inject(builder1);
            tagBuilderInjector.Inject(builder2);

            var result = service.TagBlock(tagName).ToString();

            Assert.That(result, Is.EqualTo("<script>function one() {}\r\nfunction two() {}\r\n</script>"));
        }

        private IntegrationService GetIntegrationService()
        {
            return new IntegrationService(tagBuilderInjector, new[] { testClient, anotherTestClient });
        }

        private TestIntegrationService GetTestIntegrationService()
        {
            return new TestIntegrationService(testTagBuilderInjector, new[] { testClient, anotherTestClient });
        }
    }
}
