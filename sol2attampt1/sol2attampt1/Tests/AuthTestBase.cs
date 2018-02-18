using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace WebAddressBookTests
{
    public class AuthTestBase: TestBase
    {
        [SetUp]
        public void SetupLogin()
        {
            App.Auth.Login(new AccountData("Admin", "secret"));
        }

        public static Random random = new Random((int)DateTime.Now.Ticks);

        public static  string RandomString(int length)
        {
            string chars = "0123456789abcdefghijklmnopqrstuvwxyz";
            StringBuilder builder = new StringBuilder(length);

            for (int i = 0; i < length; ++i)
                builder.Append(chars[random.Next(chars.Length)]);

            return builder.ToString();
        }
    }
}
