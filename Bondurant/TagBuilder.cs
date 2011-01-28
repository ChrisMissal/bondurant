namespace Bondurant
{
    public class TagBuilder : System.Web.Mvc.TagBuilder
    {
        public TagBuilder(string tagName) : base(tagName)
        {
        }

        public TagType Type { get; set; }

        public enum TagType
        {
            Unknown,
            Grouped,
            Prerequisite,
        }
    }
}