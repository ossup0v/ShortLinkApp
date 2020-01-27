
namespace App.API
{
    public interface IShortLinkService
    {
        string FindFullLink(string shortLink);

        bool CreateShortLink(string fullLink);
    }
}
