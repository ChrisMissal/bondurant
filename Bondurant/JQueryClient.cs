namespace Bondurant
{
    public class JQueryClient : TagBuilderClient
    {
        public JQueryClient()
        {
            AddPrerequisite(new TagBuilder("script").WithAttribute("src", "/scripts/jquery-1.4.1.min.js"));
        }

        public override TagBuilder CreateTag<T>(System.Func<T, string> valueFactory)
        {
            var tagBuilder = base.CreateTag(valueFactory);
            tagBuilder.WrapInnerHtml("$(function() {", "}");
            return tagBuilder;
        }
    }
}