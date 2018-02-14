using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Assert = NUnit.Framework.Assert;

namespace WebAddressBookTests
{
    [TestFixture]
    public class GroupCreationTests : AuthTestBase
    {
        [Test]
        public void VerifyGroupCreation()
        {
            GroupData groupInfoForCreation = new GroupData(RandomString(10), RandomString(10), RandomString(10));
            List<GroupData> groupsBefore = app.Group.GetGroupsList();

            app.Group.Create(groupInfoForCreation);

            Assert.AreEqual(groupsBefore.Count + 1, app.Group.GetGroupCount());

            List<GroupData> groupsAfter = app.Group.GetGroupsList();
            var groupAfterMaxId = groupsAfter.Max(x => x.Id);

            foreach (GroupData groupAfter in groupsAfter)
            {
                if(groupAfter.Id == groupAfterMaxId)
                    Assert.AreEqual(groupInfoForCreation.Name, groupAfter.Name);
            }
            groupsBefore.Add(groupInfoForCreation);
            groupsBefore.Sort();
            groupsAfter.Sort();
            Assert.AreEqual(groupsBefore, groupsAfter);
        }
        [Test]
        public void VerifyEmptyGroupCreation()
        {
            GroupData emptyGroupInfo = new GroupData("");
            List<GroupData> groupsBefore = app.Group.GetGroupsList();

            app.Group.Create(emptyGroupInfo);

            Assert.AreEqual(groupsBefore.Count + 1, app.Group.GetGroupCount());

            List<GroupData> groupsAfter = app.Group.GetGroupsList();
            var groupAfterMaxId = groupsAfter.Max(x => x.Id);

            foreach (GroupData groupAfter in groupsAfter)
            {
                if (groupAfter.Id == groupAfterMaxId)
                    Assert.AreEqual(emptyGroupInfo.Name, groupAfter.Name);
            }
            groupsBefore.Add(emptyGroupInfo);
            groupsBefore.Sort();
            groupsAfter.Sort();
            Assert.AreEqual(groupsBefore, groupsAfter);
        }

        //[Test]
        //[NUnit.Framework.Ignore("test")]
        public void VerifyBadNameGroupCreation()
        {
            GroupData badGroupInfo = new GroupData("a'a");
            List<GroupData> groupsBefore = app.Group.GetGroupsList();

            app.Group.Create(badGroupInfo);

            Assert.AreEqual(groupsBefore.Count + 1, app.Group.GetGroupCount());

            List<GroupData> groupsAfter = app.Group.GetGroupsList();
            var groupAfterMaxId = groupsAfter.Max(x => x.Id);

            foreach (GroupData groupAfter in groupsAfter)
            {
                if (groupAfter.Id == groupAfterMaxId)
                    Assert.AreEqual(badGroupInfo.Name, groupAfter.Name);
            }
            groupsBefore.Add(badGroupInfo);
            groupsBefore.Sort();
            groupsAfter.Sort();
            Assert.AreEqual(groupsBefore, groupsAfter);
        }
    }
}
