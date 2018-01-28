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
    public class GroupCreationTests:TestBase
    {
        
        [Test]
        public void VerifyGroupCreation()
        {
            navigationHelper.OpenHomePage();
            loginHelper.Login(new AccountData("Admin", "secret"));
            navigationHelper.GoToGroupsPage();
            groupHelper.InitGroupCreation();
            groupHelper.FillGroupForm(new GroupData("someName", "someHeader", "someFooter"));
            groupHelper.SubmitGroupCreation();
            navigationHelper.GoToGroupsPage();
            loginHelper.Logout();
        }
    }
}
