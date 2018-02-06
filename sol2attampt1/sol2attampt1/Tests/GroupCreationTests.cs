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
            GroupData groupInfoForUpdate = new GroupData("new Name", "new Header", "new Footer");
            app.Group.Create(groupInfoForUpdate);
        }
        [Test]
        public void VerifyEmptyGroupCreation()
        {
            GroupData emptyGroupInfo = new GroupData("");

            app.Group.Create(emptyGroupInfo);
        }
    }
}
