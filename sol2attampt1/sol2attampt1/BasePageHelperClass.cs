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
        private IWebDriver driver;
        private StringBuilder verificationErrors;
        private string baseURL;

        public IWebDriver Driver { get => driver; set => driver = value; }
        public StringBuilder VerificationErrors { get => verificationErrors; set => verificationErrors = value; }
        public string BaseURL { get => baseURL; set => baseURL = value; }

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
        protected void OpenHomepage()
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
    }
}
