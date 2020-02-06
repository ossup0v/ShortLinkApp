using System;
using System.Collections.Generic;
using System.Text;

namespace AppCore.UserStorage
{
    public interface IUserStorage
    {
        public void Create(User user);

        public IList<User> Read();

        public User Read(string id);

        public IList<User> Read(UserField filterField, object filterValue);

        public void Update(UserField filterField, object filterValue, UserField updateField, object updateValue);

        public void Remove();

        public void Remove(string id);
    }
}
