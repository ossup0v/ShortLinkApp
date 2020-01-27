
using System.Collections.Generic;

namespace App.API
{
    public interface IShortLinkService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="shortLink"></param>
        /// <returns>Full link</returns>
        string FindFullLink(string shortLink);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fullLink"></param>
        /// <returns>short link</returns>
        string CreateShortLink(string fullLink);

        IList<string> FindAllShortLinks();
    }
}
