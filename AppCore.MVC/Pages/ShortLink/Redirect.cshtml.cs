using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppCore.Main;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AppCore.MVC
{
    public class RedirectModel : PageModel
    {

        private IShortLinkService _shortLinkService;
        public RedirectModel(IShortLinkService shortLinkService)
        {
            _shortLinkService = shortLinkService;
        }

        public IActionResult OnGet(string token)
        {
            var fullUri = _shortLinkService.ReadUriAsync(token).Result;
            return Redirect(fullUri.FullURI);
        }
    }
}