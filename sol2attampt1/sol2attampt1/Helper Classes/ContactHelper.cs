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
        public ContactHelper(IWebDriver Driver) :base(Driver)
        { }
        public void SubmitContactCreations() => Driver.FindElement(By.Name("submit")).Click();

        public void FillAllContactForms(ContactData contact)
        {
            //Filling fields in the first block
            Driver.FindElement(By.Name("firstname")).SendKeys(contact.FirstName);
            Driver.FindElement(By.Name("middlename")).SendKeys(contact.MiddleName);
            Driver.FindElement(By.Name("lastname")).SendKeys(contact.LastName);
            Driver.FindElement(By.Name("nickname")).SendKeys(contact.NickName);
            Driver.FindElement(By.Name("title")).SendKeys(contact.Tittle);
            Driver.FindElement(By.Name("company")).SendKeys(contact.Company);
            Driver.FindElement(By.Name("address")).SendKeys(contact.Address);
            //Filling fields in the second block
            Driver.FindElement(By.Name("home")).SendKeys(contact.Home);
            Driver.FindElement(By.Name("mobile")).SendKeys(contact.Mobile);
            Driver.FindElement(By.Name("work")).SendKeys(contact.Work);
            Driver.FindElement(By.Name("fax")).SendKeys(contact.Fax);
            //Filling fields in the third block
            Driver.FindElement(By.Name("email")).SendKeys(contact.Email);
            Driver.FindElement(By.Name("email2")).SendKeys(contact.EmailSecondField);
            Driver.FindElement(By.Name("email3")).SendKeys(contact.EmailThirdField);
            Driver.FindElement(By.Name("homepage")).SendKeys(contact.Homepage);
            //Filling fields in the "Secondary" block
            Driver.FindElement(By.Name("address2")).SendKeys(contact.AddressSecondField);
            Driver.FindElement(By.Name("phone2")).SendKeys(contact.Home);
            Driver.FindElement(By.Name("notes")).SendKeys(contact.Notes);
        }
    }
}
