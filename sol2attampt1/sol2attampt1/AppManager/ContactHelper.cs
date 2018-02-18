using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;

namespace WebAddressBookTests
{
    public class ContactHelper : HelperBase
    {
        public ContactHelper(ApplicationManager manager) : base(manager)
        { }
        public ContactHelper Create(ContactData contact)
        {
            Manager.Navigator.GoToContactsPage();
            FillAllContactForms(contact).
            SubmitContactCreations();
            Manager.Navigator.GoToHomePage();
            return this;
        }

        public ContactHelper Modify(int numberOfItemTModify, ContactData infoForUpdate)
        {
            Manager.Navigator.GoToHomePage();
            InitContactModification(numberOfItemTModify);
            FillAllContactForms(infoForUpdate);
            SubmitContactModification();
            Manager.Navigator.GoToHomePage();
            return this;
        }
        public ContactHelper Delete(int numberOfElementToDelete)
        {
            Manager.Navigator.GoToHomePage();
            SelectCheckBox(numberOfElementToDelete).
            SubmitContactRemoval();
            Manager.Navigator.GoToHomePage();
            return this;
        }

        public ContactHelper SubmitContactCreations()
        {
            Driver.FindElement(By.Name("submit")).Click();
            contactCache = null;
            return this;
        }
        public ContactHelper SubmitContactModification()
        {
            Driver.FindElement(By.Name("update")).Click();
            contactCache = null;
            return this;
        }

        public ContactHelper SubmitContactRemoval()
        {
            Driver.FindElement(By.XPath("//input[@value='Delete']")).Click();
            Driver.SwitchTo().Alert().Accept(); // it will click on OK button
            contactCache = null;
            return this;
        }

        public ContactHelper FillAllContactForms(ContactData contact)
        {
            //Filling fields in the first block

            Type(By.Name("firstname"), contact.FirstName);
            Type(By.Name("middlename"), contact.MiddleName);
            Type(By.Name("lastname"), contact.LastName);
            Type(By.Name("nickname"), contact.NickName);
            Type(By.Name("title"), contact.Tittle);
            Type(By.Name("company"), contact.Company);
            Type(By.Name("address"), contact.Address);
            //Filling fields in the second block
            Type(By.Name("home"), contact.HomePhone);
            Type(By.Name("mobile"), contact.MobilePhone);
            Type(By.Name("work"), contact.WorkPhone);
            Type(By.Name("fax"), contact.Fax);
            //Filling fields in the third block
            Type(By.Name("email"), contact.Email);
            Type(By.Name("email2"), contact.EmailSecondField);
            Type(By.Name("email3"), contact.EmailThirdField);
            Type(By.Name("homepage"), contact.Homepage);
            //Filling fields in the "Secondary" block
            Type(By.Name("address2"), contact.AddressSecondField);
            Type(By.Name("phone2"), contact.HomeSecondField);
            Type(By.Name("notes"), contact.Notes);
            return this;
        }
        public ContactHelper SelectCheckBox(int index)
        {
            Driver.FindElement(By.XPath($"(//input[@name='selected[]'])[{index + 1}]")).Click();
            return this;
        }

        public ContactHelper InitContactModification(int indexOfContact)
        {
            Driver.FindElement(By.XPath($"(//img[@title='Edit'])[{indexOfContact + 1}]")).Click();
            return this;
        }

        public bool VerifyContactExists(int indexOfContact, ContactData contactInfoForCreation)
        {
            Manager.Navigator.GoToHomePage();
            while (!IsElementPresent(By.XPath($"(//tr[@name='entry'])[{indexOfContact + 1}]")))
            {
                contactInfoForCreation = new ContactData(AuthTestBase.RandomString(10), AuthTestBase.RandomString(10));
                Create(contactInfoForCreation);
            }
            return true;
        }

        public List<ContactData> contactCache { get; private set; }

        public List<ContactData> GetContactsList()
        {
            if (contactCache == null)
            {
                contactCache = new List<ContactData>();
                Manager.Navigator.GoToHomePage();
                ICollection<IWebElement> elements = SearchCollection(By.CssSelector("[name=entry]"));
                //string allText = Driver.FindElement(By.TagName("tbody")).Text;
                foreach (IWebElement element in elements)
                {
                    var lastName = element.FindElement(By.CssSelector("[name=entry] td:nth-of-type(2)")).Text;
                    var firstName = element.FindElement(By.CssSelector("[name=entry] td:nth-of-type(3)")).Text;
                    contactCache.Add(new ContactData(firstName, lastName)
                    {
                        Id = element.FindElement(By.Name("selected[]")).GetAttribute("value")
                    });
                }
            }
            return new List<ContactData>(contactCache);
        }

        public int GetContactCount()
        {
            return SearchCollection(By.CssSelector("[name=entry]")).Count;
        }

        public ContactData GetContactInfoFromTable(int contactIndex)
        {
            Manager.Navigator.GoToHomePage();
            IList<IWebElement> cells = SearchCollection(By.Name("entry"));
            IList<IWebElement> cellsOfIndex = SearchCollection(By.TagName("td"), cells[contactIndex]);

            var lastName = cellsOfIndex[1].Text;
            var firstName = cellsOfIndex[2].Text;
            var address = cellsOfIndex[3].Text;

            var allEmails = cellsOfIndex[4].Text;
            var allPhones = cellsOfIndex[5].Text;

            return new ContactData(firstName, lastName)
            {
                Address = address,
                AllEmails = allEmails,
                AllPhones = allPhones
            };
        }

        public ContactData GetContactInfoFromEditForm(int contactIndex)
        {
            Manager.Navigator.GoToHomePage();
            InitContactModification(contactIndex);
            var firstName = Driver.FindElement(By.Name("firstname")).GetAttribute("value");
            var lastName = Driver.FindElement(By.Name("lastname")).GetAttribute("value");
            var nickName = Driver.FindElement(By.Name("nickname")).GetAttribute("value");

            var title = Driver.FindElement(By.Name("title")).GetAttribute("value");
            var address = Driver.FindElement(By.Name("address")).Text;

            var email = Driver.FindElement(By.Name("email")).GetAttribute("value");
            var emailSecond = Driver.FindElement(By.Name("email2")).GetAttribute("value");
            var emailThird = Driver.FindElement(By.Name("email3")).GetAttribute("value");

            var homePhone = Driver.FindElement(By.Name("home")).GetAttribute("value");
            var mobilePhone = Driver.FindElement(By.Name("mobile")).GetAttribute("value");
            var workPhone = Driver.FindElement(By.Name("work")).GetAttribute("value");
            var homePhoneSecond = Driver.FindElement(By.Name("phone2")).GetAttribute("value");
            return new ContactData(firstName, lastName)
            {
                Address = address,
                Email = email,
                EmailSecondField = emailSecond,
                EmailThirdField = emailThird,
                HomePhone = homePhone,
                MobilePhone = mobilePhone,
                WorkPhone = workPhone,
                HomeSecondField = homePhoneSecond,
                Tittle = title,
                NickName = nickName
            };
        }

        public ContactData GetContactInfoFromDetailsForm(int numberOfContact)
        {
            Manager.Navigator.GoToHomePage();
            OpenDetailsForm(numberOfContact);
            var allInfo = (Driver.FindElement(By.CssSelector("#content")).Text);
            var cleanedInfo = Regex.Replace(allInfo, "[ -()\n\r]", "");
            return new ContactData
            {
                AllInfo = cleanedInfo
            };
        }

        public ContactHelper OpenDetailsForm(int numberOfContact)
        {
            Driver.FindElement(By.XPath($"(//img[@title='Details'])[{numberOfContact + 1}]")).Click();
            return this;
        }
    }
}
