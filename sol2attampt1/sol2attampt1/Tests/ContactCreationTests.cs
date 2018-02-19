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
        public static IEnumerable<ContactData> RandomContactDataProvider()
        {
            List<ContactData> groups = new List<ContactData>();
            for (int i = 0; i < 5; i++)
            {
                groups.Add(new ContactData
                {
                    FirstName = GenerateRandomString(100),
                    LastName = GenerateRandomString(100),
                    MiddleName = GenerateRandomString(100),
                    NickName = GenerateRandomString(100),
                    Company = GenerateRandomString(100),
                    Tittle = GenerateRandomString(100),
                    Address = GenerateRandomString(100),
                    HomePhone = GenerateRandomString(100),
                    MobilePhone = GenerateRandomString(100),
                    WorkPhone = GenerateRandomString(100),
                    Fax = GenerateRandomString(100),
                    Email = GenerateRandomString(100),
                    EmailSecondField = GenerateRandomString(100),
                    EmailThirdField = GenerateRandomString(100),
                    Homepage = GenerateRandomString(100),
                    AddressSecondField = GenerateRandomString(100),
                    HomeSecondField = GenerateRandomString(100),
                    Notes = GenerateRandomString(100)
                });
            }
            return groups;
        }

        [Test, TestCaseSource("RandomContactDataProvider")]
        public void VerifyContactCreation(ContactData contactInfoForCreation)
        {
            List<ContactData> contactsBefore = App.Contact.GetContactsList();

            App.Contact.Create(contactInfoForCreation);

            Assert.AreEqual(contactsBefore.Count + 1, App.Contact.GetContactCount());

            List<ContactData> contactsAfter = App.Contact.GetContactsList();

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
    }
}

