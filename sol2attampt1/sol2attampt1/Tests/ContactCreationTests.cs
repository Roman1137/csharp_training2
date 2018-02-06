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
    public class ContactCreationTests : AuthTestBase
    {

        [Test]
        public void VerifyContactCreation()
        {
            ContactData contactInfoForCreation = new ContactData("Roman", "Borodavka");

            app.Contact.Create(contactInfoForCreation);
        }
        [Test]
        public void VerifyEmptryContactCreation()
        {
            ContactData emptyContactInfo = new ContactData("","");

            app.Contact.Create(emptyContactInfo);
        }
    }
}

