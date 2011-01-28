using System;
using System.Linq;
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
        private TagBuilder nonEmptyTagBuilder;
        private TagBuilder emptyTagBuilder;
        private TagBuilder prereqTagBuilder;

        [SetUp]
        public void SetUp()
        {
            tagBuilderInjector = new TagBuilderInjector();
            testTagBuilderInjector = Substitute.For<ITagBuilderInjector>();
            testClient = Substitute.For<TestClient>();
            anotherTestClient = Substitute.For<AnotherTestClient>();
            emptyTagBuilder = new TagBuilder("script");
            nonEmptyTagBuilder = new TagBuilder("script") { InnerHtml = "var x = 1;" };
            prereqTagBuilder = new TagBuilder("script");
            prereqTagBuilder.Attributes.Add("src", "include.js");
        }

        [Test]
        public void AddScript_should_not_Inject_Client_with_empty_TagBuilder()
        {
            testClient.CreateTag(Arg.Any<Func<TestClient, string>>()).Returns(emptyTagBuilder);
            var service = GetTestIntegrationService();

            service.AddClient<TestClient>(testClient, x => "");

            testTagBuilderInjector.DidNotReceive().Inject(emptyTagBuilder);
        }

        [Test]
        public void AddScript_should_Inject_non_empty_TagBuilder()
        {
            testClient.CreateTag(Arg.Any<Func<TestClient, string>>()).Returns(nonEmptyTagBuilder);
            var service = GetTestIntegrationService();

            service.AddClient<TestClient>(testClient, x => "");

            testTagBuilderInjector.Received().Inject(nonEmptyTagBuilder);
        }

        [Test]
        public void AddClient_should_Add_Prerequisite_if_not_empty()
        {
            testClient.Prerequisites.Returns(new[] { prereqTagBuilder });
            testClient.CreateTag(Arg.Any<Func<TestClient, string>>()).Returns(nonEmptyTagBuilder);
            var service = GetTestIntegrationService();

            service.AddClient<TestClient>(testClient, x => "");

            testTagBuilderInjector.Received().AddPrerequisite(prereqTagBuilder);
        }

        [Test]
        public void AddClient_should_not_Add_Prerequisite_if_empty()
        {
            testClient.Prerequisites.Returns(Enumerable.Empty<TagBuilder>());
            testClient.CreateTag(Arg.Any<Func<TestClient, string>>()).Returns(nonEmptyTagBuilder);
            var service = GetTestIntegrationService();

            service.AddClient<TestClient>(testClient, x => "");

            testTagBuilderInjector.DidNotReceive().AddPrerequisite(Arg.Any<TagBuilder>());
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
            var builder1 = new TagBuilder(tagName).WithInnerHtml("function one() {}");
            var builder2 = new TagBuilder(tagName).WithInnerHtml("function two() {}");
            var service = GetIntegrationService();

            tagBuilderInjector.Inject(builder1);
            tagBuilderInjector.Inject(builder2);

            var result = service.ScriptBlock();

            Assert.That(result, Is.EqualTo("<script type=\"text/javascript\">\r\nfunction one() {}\r\nfunction two() {}\r\n</script>\r\n"));
        }

        [Test]
        public void TagBlock_should_Join_Prerequisite_and_Injected()
        {
            var builder = new TagBuilder("script").WithInnerHtml("function a() {}");
            var service = GetIntegrationService();

            tagBuilderInjector.Inject(builder);
            tagBuilderInjector.AddPrerequisite(prereqTagBuilder);

            var result = service.ScriptBlock();

            Assert.That(result, Is.EqualTo("<script src=\"include.js\"></script>\r\n<script type=\"text/javascript\">\r\nfunction a() {}\r\n</script>\r\n"));
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
