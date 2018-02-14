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
        ContactData contactInfoForCreation = new ContactData(RandomString(10), RandomString(10));
        [Test]
        public void VerifyContactRemoval()
        {
            const int numberOfItemToDelete = 3;
            app.Contact.VerifyContactExists(numberOfItemToDelete, contactInfoForCreation);
            List<ContactData> contactsBefore = app.Contact.GetContactsList();
            ContactData contactToBeRemoved = contactsBefore[numberOfItemToDelete];

            app.Contact.Delete(numberOfItemToDelete);

            Assert.AreEqual(contactsBefore.Count - 1, app.Contact.GetContactCount());

            List<ContactData> contactsAfter = app.Contact.GetContactsList();
            contactsBefore.RemoveAt(numberOfItemToDelete);
            contactsAfter.Sort();
            contactsBefore.Sort();
            Assert.AreEqual(contactsBefore,contactsAfter);

            foreach (ContactData contact in contactsAfter)
            {
                Assert.AreNotEqual(contactToBeRemoved.Id,contact.Id);
            }
        }
    }
}

