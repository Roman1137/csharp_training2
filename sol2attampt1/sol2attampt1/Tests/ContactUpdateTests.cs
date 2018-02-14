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
    public class ContactUpdateTests : AuthTestBase
    {
        ContactData contactInfoForCreation = new ContactData(RandomString(10), RandomString(10));

        [Test]
        public void VerifyContactModication()
        {

            ContactData contactInfoForUpdate = new ContactData(RandomString(10), RandomString(10));
            const int numberOfItemTModify = 5;
            app.Contact.VerifyContactExists(numberOfItemTModify, contactInfoForCreation);
            List<ContactData> contactsBefore = app.Contact.GetContactsList();
            ContactData contactToBeModified = contactsBefore[numberOfItemTModify];

            app.Contact.Modify(numberOfItemTModify,contactInfoForUpdate);

            Assert.AreEqual(contactsBefore.Count,app.Contact.GetContactCount());

            List<ContactData> contactsAfter = app.Contact.GetContactsList();
            contactsBefore[numberOfItemTModify].LastName = contactInfoForUpdate.LastName;
            contactsBefore[numberOfItemTModify].FirstName = contactInfoForUpdate.FirstName;
            contactsBefore.Sort();
            contactsAfter.Sort();
            Assert.AreEqual(contactsBefore, contactsAfter);

            foreach (ContactData contact in contactsAfter)
            {
                if (contact.Id == contactToBeModified.Id)
                {
                    Assert.AreEqual(contactInfoForUpdate.FirstName, contact.FirstName);
                    Assert.AreEqual(contactInfoForUpdate.LastName,contact.LastName);
                }
            }
        }
    }
}