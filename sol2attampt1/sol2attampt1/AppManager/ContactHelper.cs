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
            InitContactEditIcon(numberOfItemTModify);
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
            Type(By.Name("home"), contact.Home);
            Type(By.Name("mobile"), contact.Mobile);
            Type(By.Name("work"), contact.Work);
            Type(By.Name("fax"), contact.Fax);
            //Filling fields in the third block
            Type(By.Name("email"), contact.Email);
            Type(By.Name("email2"), contact.EmailSecondField);
            Type(By.Name("email3"), contact.EmailThirdField);
            Type(By.Name("homepage"), contact.Homepage);
            //Filling fields in the "Secondary" block
            Type(By.Name("address2"), contact.AddressSecondField);
            Type(By.Name("phone2"), contact.Home);
            Type(By.Name("notes"), contact.Notes);
            return this;
        }
        public ContactHelper SelectCheckBox(int number)
        {
            Driver.FindElement(By.XPath($"(//input[@name='selected[]'])[{number}]")).Click();
            return this;
        }

        public ContactHelper InitContactEditIcon(int indexOfContact)
        {
            Driver.FindElement(By.XPath($"(//img[@title='Edit'])[{indexOfContact}]")).Click();
            return this;
        }

        public bool VerifyContactExists(int indexOfContact, ContactData contactInfoForCreation)
        {
            Manager.Navigator.GoToHomePage();
            while (!IsElementPresent(By.XPath($"(//tr[@name='entry'])[{indexOfContact}]")))
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
                foreach (IWebElement element in elements)
                {
                    string lastName = element.FindElement(By.CssSelector("[name=entry] td:nth-of-type(2)")).Text;
                    string firstName = element.FindElement(By.CssSelector("[name=entry] td:nth-of-type(3)")).Text;
                    contactCache.Add(new ContactData(firstName, lastName)
                    {
                        Id =element.FindElement(By.Name("selected[]")).GetAttribute("value")
                    });
                }
            }
            return new List<ContactData>(contactCache);
        }

        public int GetContactCount()
        {
            return SearchCollection(By.CssSelector("[name=entry]")).Count;
        }
    }
}
