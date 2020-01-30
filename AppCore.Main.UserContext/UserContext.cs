using AppCore.Main.API;

namespace AppCore.Main.Context
{
    //TODO logging
    public class UserContext
    {
        private static UserContext _context;
        public User Executor { get; private set; }

        public UserContext()
        {
        }

        public static UserContext GetContext()
        {
            if (_context == null)
                return new UserContext();

            return _context;
        }

        public void UpdateExecutor(User executor)
        {
            Executor = executor;
        }
    }
}
