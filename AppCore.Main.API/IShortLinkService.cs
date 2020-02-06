
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AppCore.Main.API
{
    public interface IShortLinkService
    {

        /// <returns>short link</returns>
        string CreateShortLink(ServiceURI ServiceUri);

        /// <returns>Full link</returns>
        ServiceURI ReadUri(string token);

        Task<ServiceURI> ReadUriAsync(string token);


        Task<string> CreateShortLinkAsync(ServiceURI ServiceUri);

        /// <returns>all links</returns>
        List<ServiceURI> ReadUri();

        /// <returns>all links</returns>
        Task<List<ServiceURI>> ReadUriAsync();
    }
}
