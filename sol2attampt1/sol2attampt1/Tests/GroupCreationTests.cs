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
        public static IEnumerable<GroupData> RandomGroupDataProvider()
        {
            List<GroupData> groups = new List<GroupData>();
            for (int i = 0; i < 5; i++)
            {
                groups.Add(new GroupData(GenerateRandomString(30))
                {
                    Header = GenerateRandomString(100),
                    Footer = GenerateRandomString(100)
                });
            }
            return groups;
        }

        [Test, TestCaseSource("RandomGroupDataProvider")]
        public void VerifyGroupCreation(GroupData groupInfoForCreation)
        {

            List<GroupData> groupsBefore = App.Group.GetGroupsList();

            App.Group.Create(groupInfoForCreation);

            Assert.AreEqual(groupsBefore.Count + 1, App.Group.GetGroupCount());

            List<GroupData> groupsAfter = App.Group.GetGroupsList();
            var groupAfterMaxId = groupsAfter.Max(x => x.Id);

            foreach (GroupData groupAfter in groupsAfter)
            {
                if (groupAfter.Id == groupAfterMaxId)
                    Assert.AreEqual(groupInfoForCreation.Name, groupAfter.Name);
            }
            groupsBefore.Add(groupInfoForCreation);
            groupsBefore.Sort();
            groupsAfter.Sort();
            Assert.AreEqual(groupsBefore, groupsAfter);
        }


        //[Test]
        //[NUnit.Framework.Ignore("test")]
        public void VerifyBadNameGroupCreation()
        {
            GroupData badGroupInfo = new GroupData { Name = "a'a" };
            List<GroupData> groupsBefore = App.Group.GetGroupsList();

            App.Group.Create(badGroupInfo);

            Assert.AreEqual(groupsBefore.Count + 1, App.Group.GetGroupCount());

            List<GroupData> groupsAfter = App.Group.GetGroupsList();
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
