using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;

namespace WebAddressBookTests
{
    [TestFixture]
    public class GroupUpdateTests : TestBase
    {
        [Test]
        public void VerifyGroupModification()
        {
            const int numberOfItemToEdited = 1;
            GroupData newInfo = new GroupData("new Name","new Header","new Footer");
            app.Group.Modify(numberOfItemToEdited,newInfo);
        }
    }
}
