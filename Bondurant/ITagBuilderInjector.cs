using System.Collections.Generic;
using System.Web.Mvc;

namespace Bondurant
{
    public interface ITagBuilderInjector : IEnumerable<TagBuilder>
    {
        int Count { get; }
        void Inject(TagBuilder tagBuilder);
    }
}