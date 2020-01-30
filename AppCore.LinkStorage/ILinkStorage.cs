using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AppCore.LinkStorage.API
{
    public interface ILinkStorage
    {
        void Create(IEntry entry);

        Task CreateAsync(IEntry entry);

        /// <summary>
        /// Read all entries
        /// </summary>
        /// <returns></returns>
        IList<IEntry> Read();

        IList<IEntry> Read(FilterBy filterBy, string value);

        Task<IList<IEntry>> ReadAsync();

        Task<IList<IEntry>> ReadAsync(FilterBy filterBy, string value);

        void Update(string id, IEntry entry);

        void Update(string token, int timesClicked);

        Task UpdateAsync(string id, IEntry entry);

        Task UpdateAsync(string token, int timesClicked);

        bool Remove();

        bool Remove(FilterBy filterBy, string value);
    }
}
