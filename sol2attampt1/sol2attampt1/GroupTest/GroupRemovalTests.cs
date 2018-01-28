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
            navigationHelper.OpenHomePage();
            loginHelper.Login(new AccountData("Admin", "secret"));
            navigationHelper.GoToGroupsPage();
            groupHelper.SelectGroupToDelete(1);
            groupHelper.InitGroupRemoval();
            navigationHelper.GoToGroupsPage();
            loginHelper.Logout();
        }
    }
}

