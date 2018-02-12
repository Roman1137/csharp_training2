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
    public class GroupRemovalTests: AuthTestBase
    {
        GroupData groupInfoForCreation = new GroupData("someName", "someHeader", "someFooter");

        [Test]
        public void VerifyGroupRemoval()
        {
            const int numberOfItemToDelete = 5;
            app.Group.VerifyGroupExists(numberOfItemToDelete, groupInfoForCreation);
            List<GroupData> groupsBefore = app.Group.GetGroupsList();
            app.Group.Remove(numberOfItemToDelete);
            List<GroupData> groupsaAfter = app.Group.GetGroupsList();
            groupsBefore.RemoveAt(numberOfItemToDelete-1);
            Assert.AreEqual(groupsBefore,groupsaAfter);
        }
    }
}

