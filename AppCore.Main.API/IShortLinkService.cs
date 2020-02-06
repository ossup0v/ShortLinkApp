
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AppCore.Main
{
    public interface IShortLinkService
    {

        /// <returns>short link</returns>
        string CreateShortLink(ServiceURI ServiceUri);

        ServiceURI ReadUri(string token);

        Task<ServiceURI> ReadUriAsync(string token);

        Task<string> CreateShortLinkAsync(ServiceURI ServiceUri);

        /// <returns>all uries</returns>
        List<ServiceURI> ReadUri();

        /// <returns>all uries</returns>
        Task<List<ServiceURI>> ReadUriAsync();
    }
}
