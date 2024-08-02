using DomainClass.User;
using DataLayer.SqlServer.Common;
using AuthServices.Models.UserModels;

namespace AuthServices.Services.UserService
{

    public class UserService : IUserService
    {
        private readonly ApplicationContext context;

        public UserService(ApplicationContext context)
        {
            this.context = context;
        }

        public bool CanLogin(LoginUserDto loginModel)
        {
            var user = context.Users.FirstOrDefault(x => x.Email == loginModel.Email);
            if (user != null)
            {
                return Security.SecurePasswordHasher.Verify(loginModel.Password, user.Password);
            }
            return false;
        }

        public Dictionary<string, object> GetUserClaim(string email)
        {

            var user = context.Users.FirstOrDefault(x => x.Email == email);
            var userInfo = new Dictionary<string, object>();
            userInfo.Add("UserID", user.UserID);
            userInfo.Add("UserName", user.Name);
            userInfo.Add("Roles", user.Roles);
            userInfo.Add("LoginDate", DateTime.Now);
            userInfo.Add("ExpireDate", DateTime.Now.AddHours(24));
            return userInfo;
        }

        public User GetUserByMobile(string mobile)
        {
            return context.Users.FirstOrDefault(x => x.Email == mobile);

        }

        public User Register(AddUserDto addUserModel)
        {

            var hashpassword = Security.SecurePasswordHasher.Hash(addUserModel.Password);

            var user = new User()
            {
                UserID = Guid.NewGuid(),
                Email = addUserModel.Email,
                Name = addUserModel.Name,
                Password = hashpassword,
                Roles = "User",
                CreatedDate = DateTime.Now,
                IsActive = true

            };
            context.Users.Add(user);
            context.SaveChanges();
            return user;
        }
    }
}
