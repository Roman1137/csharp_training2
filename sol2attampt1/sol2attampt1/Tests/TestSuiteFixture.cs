using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace WebAddressBookTests
{
    [SetUpFixture]
    public class TestSuiteFixture
    {
        ApplicationManager app = ApplicationManager.GetInstance();

        [SetUp]
        public void InitApplicationManager()
        {
            app.Navigator.OpenHomePage();
            app.Auth.Login(new AccountData("Admin", "secret"));
        }
        [TearDown]
        public void StopApplicationManager()
        {
            app.Auth.Logout();
            app.Stop();
        }
    }
}
