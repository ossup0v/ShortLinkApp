using App.API;
using System.Web;
using System.Web.Mvc;

namespace short_link_tasts.Controllers
{
    [RoutePrefix("home")]
    public class HomeController : Controller
    {
        //TODO logging
        private IShortLinkService _shortLinkService;

        public HomeController(IShortLinkService shortLinkService)
        {
            _shortLinkService = shortLinkService;
        }

        [Route("FindFullLink/{shortLink=default}")]
        public string FindFullLink(string shortLink)
        {
            return _shortLinkService.FindFullLink(shortLink);
        }

        [Route("CreateShortLink/{fullLink=default}")]
        public string Create(string fullLink)
        {
            return _shortLinkService.CreateShortLink(fullLink);
        }

        [Route("ListShortLinks")]
        public ActionResult ListShortLinks()
        {
            ViewBag.Links = _shortLinkService.FindAllShortLinks();
            return View();
        }

        [Route("Index")]
        public ActionResult Index()
        {
            return View();
        }

        [Route("About")]
        public ActionResult About()
        {
            ViewBag.Message = "https://vk.com";

            return View();
        }

        [Route("Contact")]
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}