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
    public class ContactRemovalTests: ContactTestBase
    {
        ContactData contactInfoForCreation = new ContactData(RandomString(10), RandomString(10));
        [Test]
        public void VerifyContactRemoval()
        {
            const int numberOfItemToDelete = 3;
            App.Contact.VerifyContactExists(numberOfItemToDelete, contactInfoForCreation);
            var contactsBefore = ContactData.GetAll();
            var contactToBeRemoved = contactsBefore[numberOfItemToDelete];

            App.Contact.Delete(contactToBeRemoved);

            Assert.AreEqual(contactsBefore.Count - 1, App.Contact.GetContactCount());

            var contactsAfter = ContactData.GetAll();
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

