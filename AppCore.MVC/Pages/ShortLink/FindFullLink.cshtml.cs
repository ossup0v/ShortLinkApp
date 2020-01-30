using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppCore.Main.API;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AppCore.MVC
{
    public class FindFullLinkModel : PageModel
    {
        private IShortLinkService _shortLinkService;
        public FindFullLinkModel(IShortLinkService shortLinkService)
        {
            _shortLinkService = shortLinkService;
        }

        public ObjectResult OnGet(string token)
        {
            var tempToken = token?.Split('/');
            var resultToken = tempToken?[(int)tempToken?.Length - 1].Split('=');
            var obj = new ObjectResult(_shortLinkService.FindFullLink(new ServiceURI { Token = resultToken[resultToken.Length - 1] }));
            return obj;
        }
    }
}