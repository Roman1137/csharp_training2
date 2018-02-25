using System.IO;
using System.Xml.Serialization;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Microsoft.CSharp;
using Excel = Microsoft.Office.Interop.Excel;
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

        public static IEnumerable<GroupData> GroupDataFromCsvFile()
        {
            List<GroupData> groups = new List<GroupData>();
            string[] lines = File.ReadAllLines(@"X:\project\csharp_training2\sol2attampt1\sol2attampt1\GroupsDataFiles\groups.csv");
            foreach (string l in lines)
            {
                string[] parts = l.Split(',');
                groups.Add(new GroupData(parts[0])
                {
                    Header = parts[1],
                    Footer = parts[2]
                });
            }
            return groups;
        }

        public static IEnumerable<GroupData> GroupDataFromXmlFile()
        {
            return (List<GroupData>)new XmlSerializer(typeof(List<GroupData>)).Deserialize(
                 new StreamReader(@"X:\project\csharp_training2\sol2attampt1\sol2attampt1\GroupsDataFiles\groups.xml"));
        }

        public static IEnumerable<GroupData> GroupDataFromJsonFile()
        {
            return JsonConvert.DeserializeObject<List<GroupData>>(
               File.ReadAllText(@"X:\project\csharp_training2\sol2attampt1\sol2attampt1\GroupsDataFiles\groups.json"));
        }

        public static IEnumerable<GroupData> GroupDataFromExcelFile()
        {
            List<GroupData> groups = new List<GroupData>();
            Excel.Application app = new Excel.Application();
            app.Visible = true;
            Excel.Workbook workBook = app.Workbooks.Open(@"X:\project\csharp_training2\sol2attampt1\sol2attampt1\GroupsDataFiles\groups.xlsx");
            Excel.Worksheet workSheet = workBook.Sheets[1];
            Excel.Range range = workSheet.UsedRange;
            for (int i = 1; i <= range.Rows.Count; i++)
            {
                groups.Add(new GroupData()
                {
                    Name = range.Cells[i, 1].Value,
                    Header = range.Cells[i, 2].Value,
                    Footer = range.Cells[i, 3].Value
                });
            }
            workBook.Close();
            app.Visible = false;
            app.Quit();
            return groups;
        }

        [Test, TestCaseSource("GroupDataFromExcelFile")]
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


        [Test]
        [NUnit.Framework.Ignore("test")]
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
