using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using System.Collections.Generic;

namespace WebAddressBookTests
{
    [TestFixture]
    public class GroupUpdateTests : GroupTestBase
    {
        GroupData groupInfoForCreation = new GroupData(RandomString(10), RandomString(10), RandomString(10));

        [Test]
        public void VerifyGroupModification()
        {
            GroupData groupInfoForUpdate = new GroupData(RandomString(10), RandomString(10), RandomString(10));
            const int numberOfItemToEdited = 5;
            groupInfoForUpdate.Footer = null;
            App.Group.VerifyGroupExists(numberOfItemToEdited, groupInfoForCreation);
            var groupsBefore = GroupData.GetAll();
            var groupToBeModified = groupsBefore[numberOfItemToEdited];

            App.Group.Modify(groupToBeModified, groupInfoForUpdate);

            Assert.AreEqual(groupsBefore.Count, App.Group.GetGroupCount());

            var groupsAfter = GroupData.GetAll();
            groupsBefore[numberOfItemToEdited].Name = groupInfoForUpdate.Name;
            groupsBefore.Sort();
            groupsAfter.Sort();
            Assert.AreEqual(groupsBefore, groupsAfter);

            foreach (GroupData group in groupsAfter)
            {
                if(group.Id == groupToBeModified.Id)
                    Assert.AreEqual(groupInfoForUpdate.Name, group.Name);
            }
        }
    }
}
