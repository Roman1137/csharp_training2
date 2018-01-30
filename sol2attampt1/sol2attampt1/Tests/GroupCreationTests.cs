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
            app.Navigator.OpenHomePage();
            app.Auth.Login(new AccountData("Admin", "secret"));
            app.Navigator.GoToGroupsPage();
            app.Group.InitGroupCreation();
            app.Group.FillGroupForm(new GroupData("someName", "someHeader", "someFooter"));
            app.Group.SubmitGroupCreation();
            app.Navigator.GoToGroupsPage();
            app.Auth.Logout();
        }
    }
}
