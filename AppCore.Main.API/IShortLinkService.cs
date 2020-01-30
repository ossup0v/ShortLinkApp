
using System.Collections.Generic;

namespace AppCore.Main.API
{
    public interface IShortLinkService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="shortLink"></param>
        /// <returns>Full link</returns>
        string FindFullLink(ServiceURI ServiceUri);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fullLink"></param>
        /// <returns>short link</returns>
        string CreateShortLink(ServiceURI ServiceUri);

        List<(string, int)> FindAllShortLinks();
    }
}
