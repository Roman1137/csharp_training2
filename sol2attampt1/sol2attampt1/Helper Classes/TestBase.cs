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
    public class TestBase
    {
        
        public IWebDriver Driver { get; set ; }
        public StringBuilder VerificationErrors { get; set ; }
        public string BaseURL { get; set; }

        public LoginHelper loginHelper;
        public NavigationHelper navigationHelper;
        public GroupHelper groupHelper;
        public ContactHelper contactHelper;

        [SetUp]
        public void SetupTest()
        {
            FirefoxOptions options = new FirefoxOptions();
            options.UseLegacyImplementation = true;
            options.BrowserExecutableLocation = @"X:FirefoxESR\firefox.exe";
            Driver = new FirefoxDriver(options);
            BaseURL = "http://localhost/";
            VerificationErrors = new StringBuilder();

            loginHelper = new LoginHelper(Driver);
            navigationHelper = new NavigationHelper(Driver,BaseURL);
            groupHelper = new GroupHelper(Driver);
            contactHelper = new ContactHelper(Driver);
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
    }
}
