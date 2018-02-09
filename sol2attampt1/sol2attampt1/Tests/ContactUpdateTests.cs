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
    public class ContactUpdateTests : AuthTestBase
    {
        ContactData contactInfoForCreation = new ContactData("Roman", "Borodavka");

        [Test]
        public void VerifyContactModication()
        {
            ContactData contactInfoForUpdate = new ContactData("Vasya", "Pupkin");
            const int numberOfItemTModify = 5;
            app.Contact.VerifyContactExists(numberOfItemTModify, contactInfoForCreation);
            app.Contact.Modify(numberOfItemTModify,contactInfoForUpdate);
        }
    }
}