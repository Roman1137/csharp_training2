using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;

namespace WebAddressBookTests
{
    [TestFixture]
    public class GroupRemovalTests: TestBase
    {
        [Test]
        public void VerifyGroupRemoval()
        {
            app.Navigator.OpenHomePage();
            app.Auth.Login(new AccountData("Admin", "secret"));
            app.Navigator.GoToGroupsPage();
            app.Group.SelectGroupToDelete(1);
            app.Group.InitGroupRemoval();
            app.Navigator.GoToGroupsPage();
            app.Auth.Logout();
        }
    }
}

