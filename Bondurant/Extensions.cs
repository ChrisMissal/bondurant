using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Web.Mvc;

namespace Bondurant
{
    [DebuggerStepThrough]
    public static class Extensions
    {
        public static void ForEach<T>(this IEnumerable<T> @this, Action<T> action)
        {
            foreach (var item in @this)
                action(item);
        }

        public static string Join(this IEnumerable<string> @this)
        {
            var stringBuilder = new StringBuilder();
            @this.ForEach(x => stringBuilder.AppendLine(x));
            return stringBuilder.ToString();
        }

        public static bool IsNullOrWhitespace(this string @this)
        {
            return string.IsNullOrWhiteSpace(@this);
        }

        public static TagBuilder WithInnerHtml(this TagBuilder @this, string innerHtml)
        {
            @this.InnerHtml = innerHtml;
            return @this;
        }
    }
}