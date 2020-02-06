using AppCore.LinkStorage;
using LSCore = AppCore.LinkStorage.Core;
using AppCore.Main;
using AppCore.Main.Core;
using NUnit.Framework;
using System;
using System.Linq;

namespace AppCore.Tests.Services
{
    public class ShortLinkServiceTest 
    {
        private ILinkStorage _linkStorage;
        private IShortLinkService _shortLinkService;


        [Test]
        [Category("ShortLinkService")]
        public void CreateReadShortLinkTest()
        {
            Configure();

            var timeCreated = DateTime.Now;
            var serviceUri = new ServiceURI
            {
                FullURI = "https://SomeUri.com",
                Creater = new User
                {
                    Credentials = new Credentials
                    {
                        Login = "SomeLogin",
                        Password = "SomePassword"
                    },
                    Created = timeCreated,
                    Permitions = Permissions.RegisteredUser,
                    Id = new Guid()
                },
                Created = timeCreated,
                Id = new Guid()
            };
            var shortLink = _shortLinkService.CreateShortLink(serviceUri);
            var token = shortLink.Split("token=")[1];
            var uri = _shortLinkService.ReadUri(token);
            Assert.IsTrue(uri.FullURI == serviceUri.FullURI);
            Assert.IsTrue(uri.Created == serviceUri.Created);
            Assert.IsTrue(uri.Creater.Credentials.Login == serviceUri.Creater.Credentials.Login);
        }

        private void Configure()
        {
            _linkStorage = new LSCore.LinkStorage();
            _shortLinkService = new ShortLinkService(_linkStorage);
        }
    }
}
