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

        public ContentResult Scripts()
        {
            var scripts = integrationService.TagBlock("script");
            return Content(scripts.ToString(TagRenderMode.Normal));
        }
    }
}
