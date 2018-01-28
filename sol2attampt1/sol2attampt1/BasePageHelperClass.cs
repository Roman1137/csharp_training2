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
    [TestFixture]
    public class BasePageHelperClass
    {
        
        public IWebDriver Driver { get; set ; }
        public StringBuilder VerificationErrors { get; set ; }
        public string BaseURL { get; set; }

        [SetUp]
        public void SetupTest()
        {
            FirefoxOptions options = new FirefoxOptions();
            options.UseLegacyImplementation = true;
            options.BrowserExecutableLocation = @"X:FirefoxESR\firefox.exe";
            Driver = new FirefoxDriver(options);
            BaseURL = "http://localhost/";
            VerificationErrors = new StringBuilder();
        }

        [TearDown]
        public void TeardownTest()
        {
            try
            {
                Driver.Quit();
            }
            catch (Exception)
            {
                // Ignore errors if unable to close the browser
            }
            Assert.AreEqual("", VerificationErrors.ToString());
        }


        //Helper methods which are used in every test
        protected void OpenHomePage()
        {
            Driver.Navigate().GoToUrl(BaseURL + "addressbook/");
        }
        protected void Login(AccountData account)
        {
            Driver.FindElement(By.Name("user")).SendKeys(account.UserName);
            Driver.FindElement(By.Name("pass")).SendKeys(account.Password);
            Driver.FindElement(By.CssSelector("input[type=\"submit\"]")).Click();
        }
        protected void Logout()
        {
            Driver.FindElement(By.LinkText("Logout")).Click();
        }
        protected void GoToGroupsPage()
        {
            Driver.FindElement(By.LinkText("groups")).Click();
        }
        protected void ReturnToGroupsPage()
        {
            Driver.FindElement(By.LinkText("group page")).Click();
        }

        protected void SubmitGroupCreation()
        {
            Driver.FindElement(By.Name("submit")).Click();
        }

        protected void FillGroupForm(GroupData group)
        {
            Driver.FindElement(By.Name("group_name")).Clear();
            Driver.FindElement(By.Name("group_name")).SendKeys(group.Name);
            Driver.FindElement(By.Name("group_header")).Clear();
            Driver.FindElement(By.Name("group_header")).SendKeys(group.Header);
            Driver.FindElement(By.Name("group_footer")).Clear();
            Driver.FindElement(By.Name("group_footer")).SendKeys(group.Footer);
        }

        protected void InitGroupCreation()
        {
            Driver.FindElement(By.Name("new")).Click();
        }
        protected void InitGroupRemoval()
        {
            Driver.FindElement(By.XPath("(//input[@name='delete'])[2]")).Click();
        }

        protected void SelectGroupToDelete(int index)
        {
            Driver.FindElement(By.XPath("(//input[@name='selected[]'])[" + index +"]")).Click();
        }
        protected void ReturnToHomePage()
        {
            Driver.FindElement(By.LinkText("home")).Click();
        }

        protected void SubmitContactCreations()
        {
            Driver.FindElement(By.Name("submit")).Click();
        }

        protected void FillAllContactForms(ContactData contact)
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

        protected void GoToContactsPage()
        {
            Driver.FindElement(By.LinkText("add new")).Click();
        }
    }
}
