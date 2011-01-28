using System;
using System.Collections.Generic;

namespace Bondurant
{
    public abstract class TagBuilderClient : IClient
    {
        private readonly List<TagBuilder> prerequisites = new List<TagBuilder>();

        public virtual IEnumerable<TagBuilder> Prerequisites
        {
            get { return prerequisites; }
        }

        public void AddPrerequisite(TagBuilder tagBuilder)
        {
            prerequisites.Add(tagBuilder);
        }

        public virtual TagBuilder CreateTag<T>(Func<T, string> valueFactory) where T : class
        {
            var innerHtml = valueFactory(this as T);
            return innerHtml.IsNullOrWhitespace()
                ? null
                : new TagBuilder("script").WithAttribute("type", "text/javascript").WithInnerHtml(innerHtml);
        }
    }
}
