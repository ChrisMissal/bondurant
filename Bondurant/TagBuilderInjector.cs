using System.Collections;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Bondurant
{
    public class TagBuilderInjector : ITagBuilderInjector
    {
        private readonly Queue<TagBuilder> queue = new Queue<TagBuilder>();

        public IEnumerator<TagBuilder> GetEnumerator()
        {
            while (TagBuilderQueue.Count > 0)
                yield return TagBuilderQueue.Dequeue();
        }

        public int Count
        {
            get { return TagBuilderQueue.Count; }
        }

        public void Inject(TagBuilder tagBuilder)
        {
            TagBuilderQueue.Enqueue(tagBuilder);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        protected virtual Queue<TagBuilder> TagBuilderQueue
        {
            get { return queue; }
        }
    }
}