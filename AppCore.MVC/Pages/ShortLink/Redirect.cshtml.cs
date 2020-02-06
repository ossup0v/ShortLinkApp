using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppCore.Main.API;
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
            string url = "/.auth/login/aad?post_login_redirect_url="
                + Request.Query["redirect_url"];

            return Redirect(fullUri.FullURI);

        }
    }
}