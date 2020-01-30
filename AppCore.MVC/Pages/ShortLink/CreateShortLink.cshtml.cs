using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppCore.Main.API;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AppCore.MVC
{
    public class CreateShortLinkModel : PageModel
    {
        private IShortLinkService _shortLinkService;
        public CreateShortLinkModel(IShortLinkService shortLinkService)
        {
            _shortLinkService = shortLinkService;
        }

        public ObjectResult OnGet(string fullLink)
        {
            return new ObjectResult(_shortLinkService.CreateShortLinkAsync(new ServiceURI { FullURI = fullLink, Created = DateTime.Now }).Result);
        }
    }
}