using System;
using System.Web.Mvc;

namespace Bondurant
{
    public abstract class TagBuilderClient : IClient
    {
        public virtual TagBuilder CreateTag<T>(Func<T, string> valueFactory) where T : class
        {
            var innerHtml = valueFactory(this as T);
            return innerHtml.IsNullOrWhitespace()
                ? null
                : new TagBuilder("script").WithInnerHtml(innerHtml);
        }
    }
}
