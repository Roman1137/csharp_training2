using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using System.Collections.Generic;

namespace WebAddressBookTests
{
    [TestFixture]
    public class ContactCreationTests : AuthTestBase
    {

        [Test]
        public void VerifyContactCreation()
        {
            ContactData contactInfoForCreation = new ContactData("Roman","Borodavka");
            List<ContactData> contactsBefore = app.Contact.GetContactsList();
            app.Contact.Create(contactInfoForCreation);
            List<ContactData> contactsAfter = app.Contact.GetContactsList();
            contactsBefore.Add(contactInfoForCreation);
            contactsBefore.Sort();
            contactsAfter.Sort();
            Assert.AreEqual(contactsBefore, contactsAfter);

        }
        [Test]
        public void VerifyEmptryContactCreation()
        {
            ContactData emptyContactInfo = new ContactData("","");
            List<ContactData> contactsBefore = app.Contact.GetContactsList();
            app.Contact.Create(emptyContactInfo);
            List<ContactData> contactsAfter = app.Contact.GetContactsList();
            contactsBefore.Add(emptyContactInfo);
            contactsBefore.Sort();
            contactsAfter.Sort();
            Assert.AreEqual(contactsBefore, contactsAfter);
        }
    }
}

