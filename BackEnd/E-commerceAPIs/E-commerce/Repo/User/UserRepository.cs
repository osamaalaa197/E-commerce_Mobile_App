using E_commerce.Database;
using E_commerce.models;

namespace E_commerce.Repo.User
{
    public class UserRepository : IUserRepository
    {
        private readonly E_commerceDataBase _context;

        public UserRepository(E_commerceDataBase e_CommerceData)
        {
            _context = e_CommerceData;
        }
        public Users GetUserById(string id)
        {
            var user = _context.Users.FirstOrDefault(p => p.Id == id);
            return user;
        }
    }
}
