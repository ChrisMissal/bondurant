using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Bondurant
{
    public class IntegrationService
    {
        private readonly ITagBuilderInjector tagBuilderInjector;
        private readonly IEnumerable<IClient> clients;

        public IntegrationService(ITagBuilderInjector tagBuilderInjector, IEnumerable<IClient> clients)
        {
            this.tagBuilderInjector = tagBuilderInjector;
            this.clients = clients;
        }

        public IEnumerable<IClient> Clients
        {
            get { return clients; }
        }

        protected void AddScript(TagBuilder tagBuilder)
        {
            if (tagBuilder.InnerHtml.IsNullOrWhitespace())
                return;

            tagBuilderInjector.Inject(tagBuilder);
        }

        protected void NotifyAll<T>(Action<T> action) where T : IClient
        {
            Clients.OfType<T>().ForEach(action);
        }

        public virtual TagBuilder TagBlock(string tagName)
        {
            var allInnerHtml = from tagBuilder in tagBuilderInjector
                               where tagBuilder.TagName == tagName
                               select tagBuilder.InnerHtml;

            return new TagBuilder(tagName) { InnerHtml = allInnerHtml.Join() };
        }
    }
}