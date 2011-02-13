using System.Collections.Generic;
using System.Web;

namespace Bondurant
{
    public class HttpSessionInjector : TagBuilderInjector
    {
        public HttpSessionInjector()
        {
            HttpContext.Current.Session[GetType().Name + "_TagBuilderQueue"] = new Queue<TagBuilder>();
            HttpContext.Current.Session[GetType().Name + "_Prerequisites"] = new Queue<TagBuilder>();
        }

        protected override Queue<TagBuilder> TagBuilderQueue
        {
            get
            {
                return HttpContext.Current.Session[GetType().Name + "_TagBuilderQueue"] as Queue<TagBuilder>;
            }
        }

        protected override Queue<TagBuilder> Prerequisites
        {
            get
            {
                return HttpContext.Current.Session[GetType().Name + "_Prerequisites"] as Queue<TagBuilder>;
            }
        }
    }
}