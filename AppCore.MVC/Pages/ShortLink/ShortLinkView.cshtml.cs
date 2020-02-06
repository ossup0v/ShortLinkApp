using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppCore.Main.API;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace AppCore.MVC
{
    public class ShortLinkViewModel : PageModel
    {
        private IShortLinkService _shortLinkService;
        public List<ServiceURI> Uries => _shortLinkService.ReadUriAsync().Result;

        public ShortLinkViewModel(IShortLinkService shortLinkService)
        {
            _shortLinkService = shortLinkService;
        }

        public void OnGet()
        {

        }
    }
}