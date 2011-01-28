using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        protected void AddClient<T>(IClient client, Func<T, string> tagFactory) where T : class, IClient
        {
            var tagBuilder = client.CreateTag(tagFactory);

            if (tagBuilder.InnerHtml.IsNullOrWhitespace())
                return;

            client.Prerequisites.ForEach(tagBuilderInjector.AddPrerequisite);

            tagBuilderInjector.Inject(tagBuilder);
        }

        protected void NotifyAll<T>(Action<T> action) where T : IClient
        {
            clients.OfType<T>().ForEach(action);
        }

        public virtual string ScriptBlock()
        {
            var builders = tagBuilderInjector.ToArray();

            var prereqs = from tagBuilder in builders
                          where tagBuilder.Type == TagBuilder.TagType.Prerequisite
                          select tagBuilder;

            var allInnerHtml = from tagBuilder in builders
                               where tagBuilder.Type == TagBuilder.TagType.Grouped
                               select tagBuilder.InnerHtml;

            var groupedTagBuilder = new TagBuilder("script")
                .WithInnerHtml(Environment.NewLine + allInnerHtml.Join())
                .WithAttribute("type", "text/javascript");

            var builder = new StringBuilder();

            prereqs.ForEach(pr => builder.AppendLine(pr.ToString(TagRenderMode.Normal)));
            builder.AppendLine(groupedTagBuilder.ToString(TagRenderMode.Normal));

            return builder.ToString();
        }
    }
}