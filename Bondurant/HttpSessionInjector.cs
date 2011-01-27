using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;

namespace Bondurant
{
    public class HttpSessionInjector : TagBuilderInjector
    {
        public HttpSessionInjector()
        {
            HttpContext.Current.Session[GetType().FullName] = new Queue<TagBuilder>();
        }

        protected override Queue<TagBuilder> TagBuilderQueue
        {
            get
            {
                return HttpContext.Current.Session[GetType().FullName] as Queue<TagBuilder>;
            }
        }
    }
}