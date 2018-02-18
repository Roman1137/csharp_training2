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
            App.Group.VerifyGroupExists(numberOfItemToDelete, groupInfoForCreation);
            List<GroupData> groupsBefore = App.Group.GetGroupsList();
            GroupData groupToBeRemoved = groupsBefore[numberOfItemToDelete];

            App.Group.Remove(numberOfItemToDelete);

            Assert.AreEqual(groupsBefore.Count - 1, App.Group.GetGroupCount());

            List<GroupData> groupsAfter = App.Group.GetGroupsList();
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

