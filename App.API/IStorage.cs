using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.API
{
    //CRUD
    public interface IStorage
    {
        Guid Create();

        IEntry Read(Guid id);

        bool Update(Guid id, IEntryConfig config);

        bool Remove(Guid id);
    }
}
