using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Bondurant.Tests
{
    public class TestIntegrationService : IntegrationService
    {
        public TestIntegrationService(ITagBuilderInjector tagBuilderInjector, IEnumerable<IClient> clients)
            : base(tagBuilderInjector, clients)
        {
        }

        public new void AddClient<T>(IClient client, Func<T, string> tagFactory) where T : class, IClient
        {
            base.AddClient(client, tagFactory);
        }

        public new void NotifyAll<T>(Action<T> action) where T : IClient
        {
            base.NotifyAll(action);
        }
    }
}