using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace UserRegistration2.Models
{
    public class UserRepository : IUserRepository
    {
        private UserDataContext _dataContext;
        public string connectionStringSettings = ConfigurationManager.ConnectionStrings["DB_KDPMVCConnectionString"].ToString();
        
        public string ConnectionStringSettings {  get => connectionStringSettings; set =>
                connectionStringSettings = value; }
        public UserRepository() 
        { 
            _dataContext = new UserDataContext(ConnectionStringSettings);

        }

        public void deleteUser(int userId)
        {
            User user = _dataContext.Users.Where(u => u.Id == userId).SingleOrDefault();
            _dataContext.Users.DeleteOnSubmit(user);
            _dataContext.SubmitChanges();
           
        }

        public UserModel GetUserByID(int userId)
        {
            var query = from u in _dataContext.Users
                        where
                        u.Id == userId
                        select u;
            var user = query.FirstOrDefault();
            var model = new UserModel()
            {
                Id = userId,
                Name = user.Name,
                Email = user.Email,
                Age = user.Age,
            };
            return model;

        }

        public IEnumerable<UserModel> GetUsers()
        {
            IList<UserModel> userList = new List<UserModel>();
            var query = from user in _dataContext.Users
                        select user;
            var users = query.ToList();
            foreach (var userData in users)
            {
                userList.Add(new UserModel()
                {
                    Id = userData.Id,
                    Name = userData.Name,
                    Email = userData.Email,
                    Age = userData.Age
                });
            }
               return userList;
            //throw new NotImplementedException();
        }

        public void InsertUser(UserModel user)
        {
            var userData = new User()
            {
                Name = user.Name,
                Email = user.Email,
                Age = user.Age
            };
            _dataContext.Users.InsertOnSubmit(userData);
            _dataContext.SubmitChanges();
        }

        public void UpdateUser(UserModel user)
        {
            User userData = _dataContext.Users.Where(u => u.Id == user.Id).SingleOrDefault();
            userData.Name = user.Name;
            userData.Email = user.Email;
            userData.Age = user.Age;
            _dataContext.SubmitChanges();
           
        }
    }
}