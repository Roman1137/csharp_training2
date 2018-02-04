using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;

namespace WebAddressBookTests
{
   public class ContactHelper:HelperBase
    {
        public ContactHelper(ApplicationManager manager) :base(manager)
        { }
        public ContactHelper Create(ContactData contact)
        {
            manager.Navigator.GoToContactsPage();
            FillAllContactForms(contact).
            SubmitContactCreations();
            manager.Navigator.GoToHomePage();
            return this;
        }
        public ContactHelper Modify(int numberOfItemTModify, ContactData infoForUpdate)
        {
            manager.Navigator.GoToHomePage();
            InitContactEditIcon(numberOfItemTModify);
            FillAllContactForms(infoForUpdate);
            SubmitContectEdition();
            manager.Navigator.GoToHomePage();
            return this;
        }
        public ContactHelper Delete(int number)
        {
            manager.Navigator.GoToHomePage();
            SelectCheckBox(number).
            SubmitContactRemoval();
            manager.Navigator.GoToHomePage();
            return this;
        }
        public ContactHelper SubmitContectEdition()
        {
            Driver.FindElement(By.Name("update")).Click();
            return this;
        }
        public ContactHelper InitContactEditIcon(int numberOfItemTModify)
        {
            Driver.FindElement(By.CssSelector($"tbody tr:nth-child({numberOfItemTModify + 1}) [title='Edit']")).Click();
            return this;
        }
        
        public ContactHelper SubmitContactCreations()
        {
            Driver.FindElement(By.Name("submit")).Click();
            return this;
        }

        public ContactHelper FillAllContactForms(ContactData contact)
        {
            //Filling fields in the first block

            Type(By.Name("firstname"),contact.FirstName);
            Type(By.Name("middlename"),contact.MiddleName);
            Type(By.Name("lastname"),contact.LastName);
            Type(By.Name("nickname"),contact.NickName);
            Type(By.Name("title"),contact.Tittle);
            Type(By.Name("company"),contact.Company);
            Type(By.Name("address"),contact.Address);
            //Filling fields in the second block
            Type(By.Name("home"),contact.Home);
            Type(By.Name("mobile"),contact.Mobile);
            Type(By.Name("work"),contact.Work);
            Type(By.Name("fax"),contact.Fax);
            //Filling fields in the third block
            Type(By.Name("email"),contact.Email);
            Type(By.Name("email2"),contact.EmailSecondField);
            Type(By.Name("email3"),contact.EmailThirdField);
            Type(By.Name("homepage"),contact.Homepage);
            //Filling fields in the "Secondary" block
            Type(By.Name("address2"),contact.AddressSecondField);
            Type(By.Name("phone2"),contact.Home);
            Type(By.Name("notes"),contact.Notes);
            return this;
        }
        public ContactHelper SelectCheckBox(int number)
        {
            Driver.FindElement(By.CssSelector($"tbody tr:nth-child({number +1}) [type='checkbox']")).Click();
            return this;
        }
        public ContactHelper SubmitContactRemoval()
        {
            Driver.FindElement(By.XPath("//input[@value='Delete']")).Click();
            Driver.SwitchTo().Alert().Accept(); // it will click on OK button
            return this;
        }
    }
}
