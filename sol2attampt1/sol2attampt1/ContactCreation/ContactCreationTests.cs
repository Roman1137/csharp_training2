﻿using System;
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
    public class ContactCreationTests : BasePageHelperClass
    {

        [Test]
        public void VerifyContactCreation()
        {
            OpenHomePage();
            Login(new AccountData("Admin", "secret"));
            GoToContactsPage();
            FillAllContactForms(new ContactData("Roman", "Borodavka"));
            SubmitContactCreations();
            ReturnToHomePage();
            Logout();
        }
    }
}
