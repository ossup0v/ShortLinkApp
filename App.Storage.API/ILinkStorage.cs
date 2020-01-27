using App.Storage.API;
using System;
using System.Collections.Generic;

namespace App.LinkStorage.API
{
    //CRUD
    public interface ILinkStorage
    {
        Guid Create(IEntryConfig config);

        /// <summary>
        /// Read all entries
        /// </summary>
        /// <returns></returns>
        IList<IEntry> Read();

        IList<IEntry> Read(FilterBy filterBy, string value);

        bool Update(Guid id, IEntryConfig config);

        bool Remove();

        bool Remove(FilterBy filterBy, string value);
    }
}
