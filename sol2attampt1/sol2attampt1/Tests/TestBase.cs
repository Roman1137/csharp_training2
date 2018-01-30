using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;

namespace WebAddressBookTests
{
    [TestFixture]
    public class TestBase
    {
        public ApplicationManager app { get; set; }

        [SetUp]
        public void SetupTest()
        {
            app = new ApplicationManager();
            app.Navigator.OpenHomePage();
            app.Auth.Login(new AccountData("Admin", "secret"));
        }

        [TearDown]
        public void TeardownTest()
        {
            app.Auth.Logout();
            app.Stop();
        }
    }
}
