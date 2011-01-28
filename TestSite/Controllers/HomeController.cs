using System.Web.Mvc;

namespace TestSite.Controllers
{
    public class HomeController : Controller
    {
        private static readonly WebIntegrationService integrationService = new WebIntegrationService();

        public ActionResult Index()
        {
            integrationService.HomePageViewed();
            return View();
        }

        public ActionResult BackToHome()
        {
            integrationService.RedirectOccured();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Message()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Message(string message)
        {
            integrationService.MessageQueued(message);
            return RedirectToAction("Index");
        }

        public ContentResult Scripts()
        {
            return Content(integrationService.ScriptBlock());
        }
    }
}
