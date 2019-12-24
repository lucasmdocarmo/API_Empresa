using MyAPI.Controllers.Auth.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyAPI.Extensions.Provider
{
    public class UserService
    {
        public LoginUserViewModel GetUserByCredentials(string email, string password)
        {
            LoginUserViewModel user = new LoginUserViewModel() { Email = "testeapple@ioasys.com.br", Password = "12341234"};
            if (user != null)
            {
                user.Password = string.Empty;
            }
            return user;
        }
    }
}
