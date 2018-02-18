using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using System.Collections.Generic;
using NUnit.Framework.Interfaces;

namespace WebAddressBookTests
{
    [TestFixture]
    public class ContactInformationTests: AuthTestBase
    {
        ContactData contactInfoForCreation = new ContactData(RandomString(10), RandomString(10));
        [Test]
        public void VerifyContactInformationAtMainPage()
        {
            const int numberOfContact = 0;
            App.Contact.VerifyContactExists(numberOfContact, contactInfoForCreation);
            ContactData fromTable = App.Contact.GetContactInfoFromTable(numberOfContact);
            ContactData fromForm = App.Contact.GetContactInfoFromEditForm(numberOfContact);

            Assert.AreEqual(fromTable, fromForm);
            Assert.AreEqual(fromTable.Address, fromForm.Address);
            Assert.AreEqual(fromTable.Address, fromForm.Address);
            Assert.AreEqual(fromTable.AllPhones, fromForm.AllPhones);
            Assert.AreEqual(fromTable.AllEmails,fromForm.AllEmails);
        }

        [Test]
        public void VerifyContactInformationAtDetailsPage()
        {
            const int numberOfContact = 3;
            App.Contact.VerifyContactExists(numberOfContact, contactInfoForCreation);
            ContactData fromDetailsForm = App.Contact.GetContactInfoFromDetailsForm(numberOfContact);
            ContactData fromForm = App.Contact.GetContactInfoFromEditForm(numberOfContact);

            Assert.AreEqual(fromForm.AllInfo, fromDetailsForm.AllInfo);
        }

    }
}
