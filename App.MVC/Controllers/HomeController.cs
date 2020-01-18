using App.API;
using System.Web;
using System.Web.Mvc;

namespace short_link_tasts.Controllers
{
    public class HomeController : Controller
    {
        private IService _service;

        public HomeController(IService service)
        {
            _service = service;
        }

        [Route("/{link}")]
        public string Do(string shortLink)
        {
            return _service.FindFullLink(shortLink);
        }

        [Route("")]
        public bool Create(string link)
        {
            return _service.CreateShortLink(link);
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