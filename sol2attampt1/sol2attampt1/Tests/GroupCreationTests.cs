using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Assert = NUnit.Framework.Assert;

namespace WebAddressBookTests
{
    [TestFixture]
    public class GroupCreationTests: AuthTestBase
    {
        [Test]
        public void VerifyGroupCreation()
        {
            GroupData groupInfoForUpdate = new GroupData("new Name", "new Header", "new Footer");
            List<GroupData> groupsBefore = app.Group.GetGroupsList();
            app.Group.Create(groupInfoForUpdate);
            List<GroupData> groupsAfter = app.Group.GetGroupsList();
            groupsBefore.Add(groupInfoForUpdate);
            groupsBefore.Sort();
            groupsAfter.Sort();
            Assert.AreEqual(groupsBefore,groupsAfter);
        }
        [Test]
        public void VerifyEmptyGroupCreation()
        {
            GroupData emptyGroupInfo = new GroupData("");
            List<GroupData> groupsBefore = app.Group.GetGroupsList();
            app.Group.Create(emptyGroupInfo);
            List<GroupData> groupsAfter = app.Group.GetGroupsList();
            groupsBefore.Add(emptyGroupInfo);
            groupsBefore.Sort();
            groupsAfter.Sort();
            Assert.AreEqual(groupsBefore, groupsAfter);
        }
        
        [Test]
        [NUnit.Framework.Ignore("test")]
        public void VerifyBadNameGroupCreation()
        {
            GroupData badGroupInfo = new GroupData("a'a");
            List<GroupData> groupsBefore = app.Group.GetGroupsList();
            app.Group.Create(badGroupInfo);
            List<GroupData> groupsAfter = app.Group.GetGroupsList();
            groupsBefore.Add(badGroupInfo);
            groupsBefore.Sort();
            groupsAfter.Sort();
            Assert.AreEqual(groupsBefore, groupsAfter);
        }
    }
}
