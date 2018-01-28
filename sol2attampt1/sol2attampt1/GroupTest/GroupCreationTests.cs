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
    public class GroupCreationTests:BasePageHelperClass
    {
        
        [Test]
        public void VerifyGroupCreation()
        {
            OpenHomePage();
            Login(new AccountData("Admin", "secret"));
            GoToGroupsPage();
            InitGroupCreation();
            FillGroupForm(new GroupData("someName", "someHeader", "someFooter"));
            SubmitGroupCreation();
            ReturnToGroupsPage();
            Logout();
        }
    }
}
