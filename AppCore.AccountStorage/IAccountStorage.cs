using System.Collections.Generic;

namespace AppCore.AccountStorage
{
    public interface IAccountStorage
    {
        public void Create(Account account);

        public IList<Account> Read();

        public Account Read(string id);

        public IList<Account> Read(AccountField filterField, object filterValue);

        public void Update(AccountField filterField, object filterValue, AccountField updateField, object updateValue);

        public void Remove();

        public void Remove(string id);
    }
}
