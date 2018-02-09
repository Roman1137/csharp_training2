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
        GroupData groupInfoForCreation = new GroupData("someName", "someHeader", "someFooter");

        [Test]
        public void VerifyGroupModification()
        {
            GroupData groupInfoForUpdate = new GroupData("new Name", "new Header", "new Footer");
            const int numberOfItemToEdited = 5;
            
            groupInfoForUpdate.Footer = null;
            app.Group.VerifyGroupExists(numberOfItemToEdited, groupInfoForCreation);
            app.Group.Modify(numberOfItemToEdited,groupInfoForUpdate);
        }
    }
}
