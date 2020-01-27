using System;
using System.Collections.Generic;

namespace App.Storage.API
{
    //CRUD
    public interface IStorage
    {
        Guid Create(IEntryConfig config);

        IEntry Read(Guid id);

        IList<IEntry> Read(IList<Guid> ids);

        bool Update(Guid id, IEntryConfig config);

        bool Remove(Guid id);

        bool Remove(IList<Guid> ids);
    }
}
