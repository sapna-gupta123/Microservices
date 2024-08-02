using DomainClass.User;
using AuthServices.Models.UserModels;

namespace AuthServices.Services.UserService
{
    public interface IUserService
    {
        bool CanLogin(LoginUserDto loginModel);
        User Register(AddUserDto addUserModel);
        User GetUserByMobile(string mobile);
        Dictionary<string, object> GetUserClaim(string mobile);

    }
}
