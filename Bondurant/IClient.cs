using System;
using System.Collections.Generic;

namespace Bondurant
{
    public interface IClient
    {
        IEnumerable<TagBuilder> Prerequisites { get; }
        TagBuilder CreateTag<TClient>(Func<TClient, string> valueFactory) where TClient : class;
    }
}