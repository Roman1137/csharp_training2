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
    public class ContactUpdateTests : AuthTestBase
    {
        [Test]
        public void VerifyContactModication()
        {
            const int numberOfItemTModify = 1;
            ContactData infoForUpdate = new ContactData("Vasya", "Pupkin");
            app.Contact.Modify(numberOfItemTModify, infoForUpdate);
        }
    }
}