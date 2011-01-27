using System;
using System.Web.Mvc;

namespace Bondurant
{
    public interface IClient
    {
        TagBuilder CreateTag<TClient>(Func<TClient, string> valueFactory) where TClient : class;
    }
}