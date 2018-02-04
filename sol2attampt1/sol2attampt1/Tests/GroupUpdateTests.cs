using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;

namespace WebAddressBookTests
{
    [TestFixture]
    public class GroupUpdateTests : AuthTestBase
    {
        [Test]
        public void VerifyGroupModification()
        {
            const int numberOfItemToEdited = 1;
            GroupData newInfo = new GroupData("new Name");
            newInfo.Header = null;
            newInfo.Footer = null;
            app.Group.Modify(numberOfItemToEdited,newInfo);
        }
    }
}
