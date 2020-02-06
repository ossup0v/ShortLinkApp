using AppCore.AccountStorage;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppCore.Main.Core
{
    public class UserService : IUserService
    {
        private IAccountStorage _accountStorage;
        public UserService(IAccountStorage userStorage)
        {
            _accountStorage = userStorage;
        }

        public void Create(User user)
        {
            _accountStorage.Create(UserToAccount(user));
        }

        public IList<User> Read()
        {
            var users = new List<User>();
            var accounts = _accountStorage.Read();
            foreach (var account in accounts)
            {
                users.Add(AccountToUser(account));
            }
            return users;
        }

        public User Read(string id)
        {
            var account = _accountStorage.Read(id);
            return AccountToUser(account);
        }

        // check permissions >moderator 
        public void Update(string id, User userUpdate)
        {
            _accountStorage.Update(AccountField.Id, id, AccountField.Account, UserToAccount(userUpdate));
        }

        // check permissions >admin
        public bool Remove(string id)
        {
            return false;
        }

        private User AccountToUser(Account account)
        {
            throw new NotImplementedException();
        }

        private Account UserToAccount(User user)
        {
            throw new NotImplementedException();
        }
    }
}
