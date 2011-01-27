using Bondurant;

namespace TestSite.Clients
{
    public class PageViewClient : TagBuilderClient
    {
        public string HomePageViewed()
        {
            return "function doSomething() {}";
        }

        public string RedirectOccured()
        {
            return "alert('redirect');";
        }
    }
}