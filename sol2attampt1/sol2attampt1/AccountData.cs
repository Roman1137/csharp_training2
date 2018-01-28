using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAddressBookTests
{
    public class AccountData
    {
        public string UserName { get; set; }
        public string Password { get; set; }

        public AccountData(string userName, string password)
        {
            this.UserName = userName;
            this.Password = password;
        }
    }
}
