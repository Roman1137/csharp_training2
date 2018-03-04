using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
using Newtonsoft.Json;

namespace WebAddressBookTests
{
    [TestFixture]
    public class ContactCreationTests : ContactTestBase
    {
        public static IEnumerable<ContactData> RandomContactDataProvider()
        {
            List<ContactData> groups = new List<ContactData>();
            for (int i = 0; i < 5; i++)
            {
                groups.Add(new ContactData
                {
                    FirstName = GenerateRandomString(100),
                    LastName = GenerateRandomString(100),
                    MiddleName = GenerateRandomString(100),
                    NickName = GenerateRandomString(100),
                    Company = GenerateRandomString(100),
                    Tittle = GenerateRandomString(100),
                    Address = GenerateRandomString(100),
                    HomePhone = GenerateRandomString(100),
                    MobilePhone = GenerateRandomString(100),
                    WorkPhone = GenerateRandomString(100),
                    Fax = GenerateRandomString(100),
                    Email = GenerateRandomString(100),
                    EmailSecondField = GenerateRandomString(100),
                    EmailThirdField = GenerateRandomString(100),
                    Homepage = GenerateRandomString(100),
                    AddressSecondField = GenerateRandomString(100),
                    HomeSecondField = GenerateRandomString(100),
                    Notes = GenerateRandomString(100)
                });
            }
            return groups;
        }

        public static IEnumerable<ContactData> ContactsDataFromCsvFile()
        {
            List<ContactData> contacts = new List<ContactData>();
            string[] lines = File.ReadAllLines(TestContext.CurrentContext.TestDirectory + @"\ContactsDataFiles\contacts.csv");
            foreach (string l in lines)
            {
                string[] parts = l.Split(',');
                contacts.Add(new ContactData
                {
                    FirstName = parts[0],
                    LastName = parts[1],
                    MiddleName = parts[2],
                    NickName = parts[3],
                    Company = parts[4],
                    Tittle = parts[5],
                    Address = parts[6],
                    HomePhone = parts[7],
                    MobilePhone = parts[8],
                    WorkPhone = parts[9],
                    Fax = parts[10],
                    Email = parts[11],
                    EmailSecondField = parts[12],
                    EmailThirdField = parts[13],
                    Homepage = parts[14],
                    AddressSecondField = parts[15],
                    HomeSecondField = parts[16],
                    Notes = parts[17],
                });
            }
            return contacts;
        }

        public static IEnumerable<ContactData> ContactsDataFromXmlFile()
        {
            return (List<ContactData>)new XmlSerializer(typeof(List<ContactData>)).Deserialize(
                 new StreamReader(TestContext.CurrentContext.TestDirectory + @"\ContactsDataFiles\contacts.xml"));
        }

        public static IEnumerable<ContactData> ContactsDataFromJsonFile()
        {
            return JsonConvert.DeserializeObject<List<ContactData>>(
               File.ReadAllText(TestContext.CurrentContext.TestDirectory + @"\ContactsDataFiles\contacts.json"));
        }

        public static IEnumerable<ContactData> ContactsDataFromExcelFile()
        {
            List<ContactData> contacts = new List<ContactData>();
            Microsoft.Office.Interop.Excel.Application app = new Microsoft.Office.Interop.Excel.Application();
            app.Visible = true;
            Microsoft.Office.Interop.Excel.Workbook workBook = app.Workbooks.Open(TestContext.CurrentContext.TestDirectory + @"\ContactsDataFiles\contacts.xlsx");
            Microsoft.Office.Interop.Excel.Worksheet workSheet = workBook.Sheets[1];
            Microsoft.Office.Interop.Excel.Range range = workSheet.UsedRange;
            for (int i = 1; i <= range.Rows.Count; i++)
            {
                contacts.Add(new ContactData()
                {
                    FirstName = range.Cells[i, 1].Value,
                    LastName = range.Cells[i, 2].Value,
                    MiddleName = range.Cells[i, 3].Value,
                    NickName = range.Cells[i, 4].Value,
                    Company = range.Cells[i, 5].Value,
                    Tittle = range.Cells[i, 6].Value,
                    Address = range.Cells[i, 7].Value,
                    HomePhone = range.Cells[i, 8].Value,
                    MobilePhone = range.Cells[i, 9].Value,
                    WorkPhone = range.Cells[i, 10].Value,
                    Fax = range.Cells[i, 11].Value,
                    Email = range.Cells[i, 12].Value,
                    EmailSecondField = range.Cells[i, 13].Value,
                    EmailThirdField = range.Cells[i, 14].Value,
                    Homepage = range.Cells[i, 15].Value,
                    AddressSecondField = range.Cells[i, 16].Value,
                    HomeSecondField = range.Cells[i, 17].Value,
                    Notes = range.Cells[i, 18].Value
                });
            }
            workBook.Close();
            app.Visible = false;
            app.Quit();
            return contacts;
        }


        [Test, TestCaseSource("ContactsDataFromJsonFile")]
        public void VerifyContactCreation(ContactData contactInfoForCreation)
        {
            var contactsBefore = ContactData.GetAll();

            App.Contact.Create(contactInfoForCreation);

            Assert.AreEqual(contactsBefore.Count + 1, App.Contact.GetContactCount());

            var contactsAfter = ContactData.GetAll();

            var contactAfterMaxId = contactsAfter.Max(x => x.Id);

            foreach (ContactData contactAfter in contactsAfter)
            {
                if (contactAfter.Id == contactAfterMaxId)
                {
                    Assert.AreEqual(contactInfoForCreation.FirstName, contactAfter.FirstName);
                    Assert.AreEqual(contactInfoForCreation.LastName, contactAfter.LastName);
                }

            }
            contactsBefore.Add(contactInfoForCreation);
            contactsBefore.Sort();
            contactsAfter.Sort();
            Assert.AreEqual(contactsBefore, contactsAfter);
        }
    }
}

