using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfDemo.DomainDto;

namespace WpfDemo.Service.UserServices
{
    public class UserService : IUserService
    {
        public int AddUser(UserDto userDto)
        {
            return 1;
        }

        public UserDto GetUser(UserDto userDto)
        {
            return new UserDto { Account = "cs", PassWord="123456" };
        }
    }
}
