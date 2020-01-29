using App.API;
using System;
using System.Web;
using System.Web.Mvc;

namespace short_link_tasts.Controllers
{
    [Route("Home")]
    public class HomeController : Controller
    {
        //TODO logging
        private IShortLinkService _shortLinkService;

        public HomeController(IShortLinkService shortLinkService)
        {
            _shortLinkService = shortLinkService;
        }

        [Route("FindFullLink")]
        public string FindFullLink(string token)
        {
            return _shortLinkService.FindFullLink(new ServiceURI { Token = token });
        }

        [Route("~/q")]
        public RedirectResult RedirectToFullUri(string token)
        {
            var fullUri = _shortLinkService.FindFullLink(new ServiceURI { Token = token });
            return new RedirectResult(fullUri);
        }

        [Route("CreateShortLink")]
        public string Create(string fullLink)
        {
            return _shortLinkService.CreateShortLink(new ServiceURI { FullURI = fullLink, Created = DateTime.Now });
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
    }
}