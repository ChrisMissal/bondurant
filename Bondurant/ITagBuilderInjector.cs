using System.Collections.Generic;

namespace Bondurant
{
    public interface ITagBuilderInjector : IEnumerable<TagBuilder>
    {
        void AddPrerequisite(TagBuilder tagBuilder);
        int PrerequisiteCount { get; }
        int TagCount { get; }
        void Inject(TagBuilder tagBuilder);
    }
}