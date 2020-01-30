using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppCore.Main.API;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace AppCore.MVC.Controllers
{
    public class ShortLinkController : Controller
    {
        private IShortLinkService _shortLinkService;

        public ShortLinkController(IShortLinkService shortLinkService)
        {
            _shortLinkService = shortLinkService;
        }

        public string FindFullLink(string token)
        {
            return _shortLinkService.FindFullLink(new ServiceURI { Token = token });
        }

        public string PassFromBody([FromBody]object token)
        {
            return _shortLinkService.FindFullLink(new ServiceURI { Token = ((ServiceURI)token).Token });
        }

        public RedirectResult RedirectToFullUri(string token)
        {
            var fullUri = _shortLinkService.FindFullLink(new ServiceURI { Token = token });
            return new RedirectResult(fullUri);
        }

        public string Create(string fullLink)
        {
            return _shortLinkService.CreateShortLink(new ServiceURI { FullURI = fullLink, Created = DateTime.Now });
        }

        public string ListShortLinks()
        {
            var links = _shortLinkService.FindAllShortLinks();
            return JsonConvert.SerializeObject(links, Formatting.Indented);
        }

        public ActionResult Index()
        {
            return View();
        }
    }
}