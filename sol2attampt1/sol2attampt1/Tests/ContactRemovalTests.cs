using System;
using System.Collections.Generic;
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
    public class ContactRemovalTests: AuthTestBase
    {
        ContactData contactInfoForCreation = new ContactData("Roman", "Borodavka");

        [Test]
        public void VerifyContactRemoval()
        {
            const int numberOfItemToDelete = 3;
            app.Contact.VerifyContactExists(numberOfItemToDelete, contactInfoForCreation);
            List<ContactData> contactsBefore = app.Contact.GetContactsList();
            app.Contact.Delete(numberOfItemToDelete);
            List<ContactData> contactsAfter = app.Contact.GetContactsList();
            contactsBefore.RemoveAt(numberOfItemToDelete-1);
            contactsBefore.Sort();
            contactsAfter.Sort();
            Assert.AreEqual(contactsBefore,contactsAfter);
        }
    }
}

