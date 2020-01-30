
using System.Collections.Generic;
using System.Threading.Tasks;

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

        Task<string> FindFullLinkAsync(ServiceURI ServiceUri);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fullLink"></param>
        /// <returns>short link</returns>
        string CreateShortLink(ServiceURI ServiceUri);

        Task<string> CreateShortLinkAsync(ServiceURI ServiceUri);

        List<(string, int)> FindAllShortLinks();

        Task<List<(string, int)>> FindAllShortLinksAsync();
    }
}
