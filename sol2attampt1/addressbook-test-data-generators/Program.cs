using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAddressBookTests;
using System.Xml;
using System.Xml.Serialization;
using Newtonsoft.Json;
using Excel = Microsoft.Office.Interop.Excel;
using Microsoft.CSharp;
using Formatting = Newtonsoft.Json.Formatting;

namespace addressbook_test_data_generators
{
    class Program
    {
        static void Main(string[] args)
        {
            string valueType = args[0];
            int count = Convert.ToInt32(args[1]);

            string fileName = args[2];
            string format = args[3];
            switch (valueType)
            {
                case "groups":
                    {
                        List<GroupData> groups = new List<GroupData>();
                        for (int i = 0; i < count; i++)
                        {
                            groups.Add(new GroupData(TestBase.GenerateRandomString(10))
                            {
                                Header = TestBase.GenerateRandomString(10),
                                Footer = TestBase.GenerateRandomString(10)
                            });
                        }

                        if (format == "excel")
                        {
                            WriteGroupsToExcelFile(groups, fileName);
                        }
                        else
                        {
                            StreamWriter writer = new StreamWriter(fileName);
                            if (format == "csv")
                            {
                                WriteGroupsToCsvFile(groups, writer);
                            }
                            else if (format == "xml")
                            {
                                WriteGroupsToXmlFile(groups, writer);
                            }
                            else if (format == "json")
                            {
                                WriteGroupsToJsonFile(groups, writer);
                            }
                            else
                            {
                                Console.Out.Write("Unrecognized format" + format);
                            }
                            writer.Close();
                        }
                        break;
                    }
                case "contacts":
                {
                    List<ContactData> contacts = new List<ContactData>();
                    for (int i = 0; i < count; i++)
                    {
                        contacts.Add(new ContactData()
                        {
                            FirstName = TestBase.GenerateRandomString(10),
                            LastName = TestBase.GenerateRandomString(10),
                            MiddleName = TestBase.GenerateRandomString(10),
                            NickName = TestBase.GenerateRandomString(10),
                            Company = TestBase.GenerateRandomString(10),
                            Tittle = TestBase.GenerateRandomString(10),
                            Address = TestBase.GenerateRandomString(10),
                            HomePhone = TestBase.GenerateRandomString(10),
                            MobilePhone = TestBase.GenerateRandomString(10),
                            WorkPhone = TestBase.GenerateRandomString(10),
                            Fax = TestBase.GenerateRandomString(10),
                            Email = TestBase.GenerateRandomString(10),
                            EmailSecondField = TestBase.GenerateRandomString(10),
                            EmailThirdField = TestBase.GenerateRandomString(10),
                            Homepage = TestBase.GenerateRandomString(10),
                            AddressSecondField = TestBase.GenerateRandomString(10),
                            HomeSecondField = TestBase.GenerateRandomString(10),
                            Notes = TestBase.GenerateRandomString(10)
                        });
                    }

                    if (format == "excel")
                    {
                        WriteContactsToExcelFile(contacts, fileName);
                    }
                    else
                    {
                        StreamWriter writer = new StreamWriter(fileName);
                        if (format == "csv")
                        {
                            WriteContactsToCsvFile(contacts, writer);
                        }
                        else if (format == "xml")
                        {
                            WriteContactsToXmlFile(contacts, writer);
                        }
                        else if (format == "json")
                        {
                            WriteContactsToJsonFile(contacts, writer);
                        }
                        else
                        {
                            Console.Out.Write("Unrecognized format" + format);
                        }
                        writer.Close();
                    }
                    break;
                }
                default:
                {
                    Console.Out.Write("The value type is incorrect");
                    break;
                }

            }

        }

        #region methods for groups

        static void WriteGroupsToExcelFile(List<GroupData> groups, string fileName)
        {
            Excel.Application app = new Excel.Application();
            app.Visible = true;
            Excel.Workbook workBook = app.Workbooks.Add();
            Excel.Worksheet workSheet = workBook.ActiveSheet;
            int row = 1;
            foreach (GroupData group in groups)
            {
                workSheet.Cells[row, 1] = group.Name;
                workSheet.Cells[row, 2] = group.Header;
                workSheet.Cells[row, 3] = group.Footer;
                row++;
            }

            string fullPath = Path.Combine(Directory.GetCurrentDirectory(), fileName);
            File.Delete(fullPath);
            workBook.SaveAs(fullPath);

            workBook.Close();
            app.Visible = false;
            app.Quit();
        }

        static void WriteGroupsToCsvFile(List<GroupData> groups, StreamWriter writer)
        {
            foreach (GroupData group in groups)
            {
                writer.WriteLine($"{group.Name},{group.Header},{group.Footer}");
            }
        }

        static void WriteGroupsToXmlFile(List<GroupData> groups, StreamWriter writer)
        {
            new XmlSerializer(typeof(List<GroupData>)).Serialize(writer, groups);
        }

        static void WriteGroupsToJsonFile(List<GroupData> groups, StreamWriter writer)
        {
            writer.Write(JsonConvert.SerializeObject(groups, Formatting.Indented));
        }
        #endregion

        #region methods for contacts

        static void WriteContactsToExcelFile(List<ContactData> contacts, string fileName)
        {
            Excel.Application app = new Excel.Application();
            app.Visible = true;
            Excel.Workbook workBook = app.Workbooks.Add();
            Excel.Worksheet workSheet = workBook.ActiveSheet;
            int row = 1;
            foreach (ContactData contact in contacts)
            {
                workSheet.Cells[row, 1] = contact.FirstName;
                workSheet.Cells[row, 2] = contact.LastName;
                workSheet.Cells[row, 3] = contact.MiddleName;
                workSheet.Cells[row, 4] = contact.NickName;
                workSheet.Cells[row, 5] = contact.Company;
                workSheet.Cells[row, 6] = contact.Tittle;
                workSheet.Cells[row, 7] = contact.Address;
                workSheet.Cells[row, 8] = contact.HomePhone;
                workSheet.Cells[row, 9] = contact.MobilePhone;
                workSheet.Cells[row, 10] = contact.WorkPhone;
                workSheet.Cells[row, 11] = contact.Fax;
                workSheet.Cells[row, 12] = contact.Email;
                workSheet.Cells[row, 13] = contact.EmailSecondField;
                workSheet.Cells[row, 14] = contact.EmailThirdField;
                workSheet.Cells[row, 15] = contact.Homepage;
                workSheet.Cells[row, 16] = contact.AddressSecondField;
                workSheet.Cells[row, 17] = contact.HomeSecondField;
                workSheet.Cells[row, 18] = contact.Notes;
                row++;
            }

            string fullPath = Path.Combine(Directory.GetCurrentDirectory(), fileName);
            File.Delete(fullPath);
            workBook.SaveAs(fullPath);

            workBook.Close();
            app.Visible = false;
            app.Quit();
        }

        static void WriteContactsToCsvFile(List<ContactData> contacts, StreamWriter writer)
        {
            foreach (ContactData contact in contacts)
            {
                writer.WriteLine($"{contact.FirstName}," +
                             $"{contact.LastName}," +
                             $"{contact.MiddleName}," +
                             $"{contact.NickName}," +
                             $"{contact.Company}," +
                             $"{contact.Tittle}," +
                             $"{contact.Address}," +
                             $"{contact.HomePhone}," +
                             $"{contact.MobilePhone}," +
                             $"{contact.WorkPhone}," +
                             $"{contact.Fax}," +
                             $"{contact.Email}," +
                             $"{contact.EmailSecondField}," +
                             $"{contact.EmailThirdField}," +
                             $"{contact.Homepage}," +
                             $"{contact.AddressSecondField}," +
                             $"{contact.HomeSecondField}," +
                             $"{contact.Notes}"
                             );
            }
        }

        static void WriteContactsToXmlFile(List<ContactData> contacts, StreamWriter writer)
        {
            new XmlSerializer(typeof(List<ContactData>)).Serialize(writer, contacts);
        }

        static void WriteContactsToJsonFile(List<ContactData> contacts, StreamWriter writer)
        {
            writer.Write(JsonConvert.SerializeObject(contacts, Formatting.Indented));
        }
        #endregion
    }
}
