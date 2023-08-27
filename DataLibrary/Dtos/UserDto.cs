using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLibrary.Dtos
{
    public class UserDto:BaseDto
    {
        public string Account { get; set; }

        public string UserName { get; set; }
        public string PassWord { get; set; }

        public string NewPassWord { get; set; }
    }
}
