using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserRegistration2.Models
{
    public interface IUserRepository
    {
        IEnumerable<UserModel> GetUsers();
        UserModel GetUserByID(int userId);
        void InsertUser(UserModel user);
        void deleteUser(int userId);
        void UpdateUser(UserModel user);
    }
}
