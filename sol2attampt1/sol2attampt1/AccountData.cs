using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAddressBookTests
{
    class AccountData
    {
        private string userName;
        private string password;

        public AccountData(string userName,string password)
        {
            this.userName = userName;
            this.password = password;
        }

        public string UserName
        {
            get { return userName; }
            set { userName = value; }
        }
        public string Password
        {
            get { return password; }
            set { password = value; }
        }
    }
}
