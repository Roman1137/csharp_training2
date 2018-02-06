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
    public class ContactRemovalTests: AuthTestBase
    {
        ContactData contactInfoForCreation = new ContactData("Roman", "Borodavka");

        [Test]
        public void VerifyContactRemoval()
        {
            const int numberOfItemToDelete = 1;
            app.Contact.Delete(numberOfItemToDelete,contactInfoForCreation);
        }
    }
}

