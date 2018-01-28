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
    public class GroupRemovalTests: BasePageHelperClass
    {
        [Test]
        public void VerifyGroupRemoval()
        {
            OpenHomePage();
            Login(new AccountData("Admin", "secret"));
            GoToGroupsPage();
            SelectGroupToDelete(1);
            InitGroupRemoval();
            ReturnToGroupsPage();
            Logout();
        }
    }
}

