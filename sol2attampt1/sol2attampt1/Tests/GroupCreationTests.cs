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
    public class GroupCreationTests: AuthTestBase
    {
        
        [Test]
        public void VerifyGroupCreation()
        {
            GroupData group = new GroupData("someName", "someHeader", "someFooter");

            app.Group.Create(group);
        }
        [Test]
        public void VerifyEmptyGroupCreation()
        {
            GroupData group = new GroupData("");

            app.Group.Create(group);
        }
    }
}
