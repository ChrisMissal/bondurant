using Bondurant;
using TestSite.Clients;

namespace TestSite.Controllers
{
    public class WebIntegrationService : IntegrationService
    {
        public WebIntegrationService()
            : base(new HttpSessionInjector(), new[] { new PageViewClient() })
        {
        }

        public void HomePageViewed()
        {
            NotifyAll<PageViewClient>(eachClient =>
            {
                var script = eachClient.CreateTag<PageViewClient>(x => x.HomePageViewed());
                AddScript(script);
            });
        }

        public void RedirectOccured()
        {
            NotifyAll<PageViewClient>(eachClient =>
            {
                var script = eachClient.CreateTag<PageViewClient>(x => x.RedirectOccured());
                AddScript(script);
            });
        }
    }
}