using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace WebAddressBookTests
{
    [TestFixture]
    public class LoginTests:TestBase
    {
        [Test]
        public void LoginWithValidCredentials()
        {
            //prepare
            App.Auth.Logout();
            //action
            AccountData account = new AccountData("admin", "secret");
            App.Auth.Login(account);
            //verification
            Assert.IsTrue(App.Auth.IsLoggedIn(account));
        }
        [Test]
        public void LoginWithInvalidCredentials()
        {
            //prepare
            App.Auth.Logout();
            //action
            AccountData account = new AccountData("admin", "123456");
            App.Auth.Login(account);
            //verification
            Assert.IsFalse(App.Auth.IsLoggedIn(account));
        }
    }
}
