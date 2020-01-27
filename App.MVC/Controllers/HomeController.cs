using App.API;
using System.Web;
using System.Web.Mvc;

namespace short_link_tasts.Controllers
{
    public class HomeController : Controller
    {
        //TODO logging
        //private IShortLinkService _shortLinkService;

        //public HomeController(IShortLinkService shortLinkService)
        //{
        //    _shortLinkService = shortLinkService;
        //}

        [Route("/{link}")]
        public string Do(string shortLink)
        {
            return "ewq";//_shortLinkService.FindFullLink(shortLink);
        }

        [Route("")]
        public bool Create(string link)
        {
            return true;//_shortLinkService.CreateShortLink(link);
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}