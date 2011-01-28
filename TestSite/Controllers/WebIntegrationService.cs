using System;
using Bondurant;
using TestSite.Clients;

namespace TestSite.Controllers
{
    public class WebIntegrationService : IntegrationService
    {
        public WebIntegrationService()
            : base(new HttpSessionInjector(), new IClient[] { new PageViewClient(), new MessagingClient() })
        {
        }

        public void HomePageViewed()
        {
            NotifyAll<PageViewClient>(c => AddClient<PageViewClient>(c, x => x.HomePageViewed()));
        }

        public void RedirectOccured()
        {
            NotifyAll<PageViewClient>(c => AddClient<PageViewClient>(c, x => x.RedirectOccured()));
        }

        public void MessageQueued(string message)
        {
            NotifyAll<MessagingClient>(c => AddClient<MessagingClient>(c, x => x.QueueMessage(message)));
        }
    }
}