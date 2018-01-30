using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;

namespace WebAddressBookTests
{
    public class ApplicationManager
    {
        public IWebDriver Driver { get; set; }
        public string BaseURL { get; set; }

        public LoginHelper Auth { get; set; }
        public NavigationHelper Navigator { get; set; }
        public GroupHelper Group { get; set; }
        public ContactHelper contactHelper { get; set; }

        public ApplicationManager ()
        {
            FirefoxOptions options = new FirefoxOptions();
            options.UseLegacyImplementation = true;
            options.BrowserExecutableLocation = @"X:FirefoxESR\firefox.exe";
            Driver = new FirefoxDriver(options);
            BaseURL = "http://localhost/";
            Auth = new LoginHelper(Driver);
            Navigator = new NavigationHelper(Driver, BaseURL);
            Group = new GroupHelper(Driver);
            contactHelper = new ContactHelper(Driver);
        }
        public void Stop()
        {

        }
    }
}
