using Bondurant;

namespace TestSite.Clients
{
    public class MessagingClient : JQueryClient
    {
        public MessagingClient()
        {
            AddPrerequisite(new TagBuilder("script").WithAttribute("src", "/scripts/messaging.js"));
        }

        public string QueueMessage(string message)
        {
            return "queueMessage('" + message + "');";
        }
    }
}