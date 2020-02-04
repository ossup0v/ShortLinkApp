using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AppCore.LinkStorage.API
{
    public interface ILinkStorage
    {
        void Create(Entry entry);

        Task CreateAsync(Entry entry);

        /// <summary>
        /// Read all entries
        /// </summary>
        /// <returns></returns>
        IList<Entry> Read();

        IList<Entry> Read(Field Field, string value);

        Task<IList<Entry>> ReadAsync();

        Task<IList<Entry>> ReadAsync(Field Field, string value);

        void Update(string id, Entry entry);

        void Update(Field filter, object filterValue, Field updateFiled, object updateValue);

        Task UpdateAsync(string id, Entry entry);

        Task UpdateAsync(Field filterField, object filterValue, Field updateField, object updateValue);

        bool Remove();

        bool Remove(Field Field, string value);
    }
}
