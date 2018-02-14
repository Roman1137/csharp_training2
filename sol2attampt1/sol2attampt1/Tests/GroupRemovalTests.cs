using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using System.Collections.Generic;

namespace WebAddressBookTests
{
    [TestFixture]
    public class GroupRemovalTests : AuthTestBase
    {
        GroupData groupInfoForCreation = new GroupData(RandomString(10), RandomString(10), RandomString(10));

        [Test]
        public void VerifyGroupRemoval()
        {
            const int numberOfItemToDelete = 5;
            app.Group.VerifyGroupExists(numberOfItemToDelete, groupInfoForCreation);
            List<GroupData> groupsBefore = app.Group.GetGroupsList();
            GroupData groupToBeRemoved = groupsBefore[numberOfItemToDelete];

            app.Group.Remove(numberOfItemToDelete);

            Assert.AreEqual(groupsBefore.Count - 1, app.Group.GetGroupCount());

            List<GroupData> groupsAfter = app.Group.GetGroupsList();
            groupsBefore.RemoveAt(numberOfItemToDelete);
            groupsAfter.Sort();
            groupsBefore.Sort();
            Assert.AreEqual(groupsBefore, groupsAfter);
            foreach (GroupData group in groupsAfter)
            {
                Assert.AreNotEqual(groupToBeRemoved.Id,group.Id);
            }
        }
    }
}

