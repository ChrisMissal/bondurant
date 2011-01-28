using System.Collections;
using System.Collections.Generic;

namespace Bondurant
{
    public class TagBuilderInjector : ITagBuilderInjector
    {
        private readonly Queue<TagBuilder> queue = new Queue<TagBuilder>();
        private readonly Queue<TagBuilder> prerequisites = new Queue<TagBuilder>();

        public IEnumerator<TagBuilder> GetEnumerator()
        {
            while (prerequisites.Count > 0)
            {
                var tagBuilder = prerequisites.Dequeue();
                tagBuilder.Type = TagBuilder.TagType.Prerequisite;
                yield return tagBuilder;
            }
            while (TagBuilderQueue.Count > 0)
            {
                var tagBuilder = TagBuilderQueue.Dequeue();
                tagBuilder.Type = TagBuilder.TagType.Grouped;
                yield return tagBuilder;
            }
        }

        public int TagCount
        {
            get { return TagBuilderQueue.Count; }
        }

        public void Inject(TagBuilder tagBuilder)
        {
            TagBuilderQueue.Enqueue(tagBuilder);
        }

        public void AddPrerequisite(TagBuilder tagBuilder)
        {
            if (prerequisites.Contains(tagBuilder))
                return;

            prerequisites.Enqueue(tagBuilder);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        protected virtual Queue<TagBuilder> TagBuilderQueue
        {
            get { return queue; }
        }

        public int PrerequisiteCount
        {
            get { return prerequisites.Count; }
        }
    }
}