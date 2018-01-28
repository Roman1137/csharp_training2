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
    public class ContactCreationTests : TestBase
    {

        [Test]
        public void VerifyContactCreation()
        {
            navigationHelper.OpenHomePage();
            loginHelper.Login(new AccountData("Admin", "secret"));
            navigationHelper.GoToContactsPage();
            contactHelper.FillAllContactForms(new ContactData("Roman", "Borodavka"));
            contactHelper.SubmitContactCreations();
            navigationHelper.ReturnToHomePage();
            loginHelper.Logout();
        }
    }
}

