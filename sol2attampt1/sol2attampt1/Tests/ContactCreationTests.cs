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
            ContactData contact = new ContactData("Roman", "Borodavka");

            app.Contact.Create(contact);
        }
        [Test]
        public void VerifyEmptryContactCreation()
        {
            ContactData contact = new ContactData("","");

            app.Contact.Create(contact);
        }
    }
}

