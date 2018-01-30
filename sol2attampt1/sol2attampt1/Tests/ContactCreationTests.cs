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
            app.Navigator.OpenHomePage();
            app.Auth.Login(new AccountData("Admin", "secret"));
            app.Navigator.GoToContactsPage();
            app.contactHelper.FillAllContactForms(new ContactData("Roman", "Borodavka"));
            app.contactHelper.SubmitContactCreations();
            app.Navigator.ReturnToHomePage();
            app.Auth.Logout();
        }
    }
}

