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
        
        [Test]
        public void VerifyContactModication()
        {
            ContactData contactInfoForUpdate = new ContactData("Vasya", "Pupkin");
            const int numberOfItemTModify = 1;
            app.Contact.Modify(numberOfItemTModify, contactInfoForUpdate);
        }
    }
}