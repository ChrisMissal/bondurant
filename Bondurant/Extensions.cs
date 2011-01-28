using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Bondurant
{
    [DebuggerStepThrough]
    public static class Extensions
    {
        public static TagBuilder WithInnerHtml(this TagBuilder @this, string innerHtml)
        {
            @this.InnerHtml = innerHtml;
            return @this;
        }

        public static TagBuilder WithAttribute(this TagBuilder @this, string key, string value)
        {
            @this.Attributes.Add(key, value);
            return @this;
        }

        public static TagBuilder WrapInnerHtml(this TagBuilder @this, string front, string back)
        {
            @this.InnerHtml = front + @this.InnerHtml + back;
            return @this;
        }

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
    }
}