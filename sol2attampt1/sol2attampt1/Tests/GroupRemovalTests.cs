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
    public class GroupRemovalTests: AuthTestBase
    {
        GroupData groupInfoForCreation = new GroupData("someName", "someHeader", "someFooter");

        [Test]
        public void VerifyGroupRemoval()
        {
            const int numberOfItemToDelete = 5;
            app.Group.Remove(numberOfItemToDelete,groupInfoForCreation);
        }
    }
}

