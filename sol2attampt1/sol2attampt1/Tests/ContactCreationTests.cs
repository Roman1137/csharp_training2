using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using System.Collections.Generic;
using System.Linq;

namespace WebAddressBookTests
{
    [TestFixture]
    public class ContactCreationTests : AuthTestBase
    {

        [Test]
        public void VerifyContactCreation()
        {
            ContactData contactInfoForCreation = new ContactData(RandomString(10), RandomString(10));
            List<ContactData> contactsBefore = app.Contact.GetContactsList();

            app.Contact.Create(contactInfoForCreation);

            Assert.AreEqual(contactsBefore.Count + 1, app.Contact.GetContactCount());

            List<ContactData> contactsAfter = app.Contact.GetContactsList();

            var contactAfterMaxId = contactsAfter.Max(x => x.Id);

            foreach (ContactData contactAfter in contactsAfter)
            {
                if (contactAfter.Id == contactAfterMaxId)
                {
                    Assert.AreEqual(contactInfoForCreation.FirstName, contactAfter.FirstName);
                    Assert.AreEqual(contactInfoForCreation.LastName, contactAfter.LastName);
                }

            }
            contactsBefore.Add(contactInfoForCreation);
            contactsBefore.Sort();
            contactsAfter.Sort();
            Assert.AreEqual(contactsBefore, contactsAfter);
        }
        [Test]
        public void VerifyEmptryContactCreation()
        {
            ContactData emptyContactInfo = new ContactData("", "");
            List<ContactData> contactsBefore = app.Contact.GetContactsList();

            app.Contact.Create(emptyContactInfo);

            Assert.AreEqual(contactsBefore.Count + 1, app.Contact.GetContactCount());

            List<ContactData> contactsAfter = app.Contact.GetContactsList();
            var contactAfterMaxId = contactsAfter.Max(x => x.Id);

            foreach (ContactData contactAfter in contactsAfter)
            {
                if (contactAfter.Id == contactAfterMaxId)
                {
                    Assert.AreEqual(emptyContactInfo.FirstName, contactAfter.FirstName);
                    Assert.AreEqual(emptyContactInfo.LastName, contactAfter.LastName);
                }

            }
            contactsBefore.Add(emptyContactInfo);
            contactsBefore.Sort();
            contactsAfter.Sort();
            Assert.AreEqual(contactsBefore, contactsAfter);
        }
    }
}

