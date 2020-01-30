using System;
using System.Collections.Generic;

namespace AppCore.LinkStorage.API
{
    public interface ILinkStorage
    {
        void Create(IEntry entry);

        /// <summary>
        /// Read all entries
        /// </summary>
        /// <returns></returns>
        IList<IEntry> Read();

        IList<IEntry> Read(FilterBy filterBy, string value);

        void Update(string id, IEntry entry);

        void Update(string token, int timesClicked);

        bool Remove();

        bool Remove(FilterBy filterBy, string value);
    }
}
