using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfDemo.DomainDto;

namespace WpfDemo.Service.UserServices
{
   public interface IUserService
    {
        public UserDto GetUser(UserDto userDto);

        public int AddUser(UserDto userDto);
    }
}
