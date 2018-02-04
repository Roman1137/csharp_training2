using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
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
        public ContactHelper Contact { get; set; }
        public static ThreadLocal<ApplicationManager> app = new ThreadLocal<ApplicationManager>();

        private ApplicationManager ()
        {
            FirefoxOptions options = new FirefoxOptions();
            options.UseLegacyImplementation = true;
            options.BrowserExecutableLocation = @"X:FirefoxESR\firefox.exe";
            Driver = new FirefoxDriver(options);
            BaseURL = "http://localhost/";
            Driver.Manage().Timeouts().ImplicitWait = new TimeSpan(10);
            //initialize helper classes
            Auth = new LoginHelper(this);
            Navigator = new NavigationHelper(this, BaseURL);
            Group = new GroupHelper(this);
            Contact = new ContactHelper(this);
        }
        public static ApplicationManager GetInstance()
        {
            if (!app.IsValueCreated)
                app.Value = new ApplicationManager();
            return app.Value;
        }
        public void Stop()
        {
            try
            {
                Driver.Close();
                Driver.Quit();
                Driver.Dispose();
            }
            catch(Exception)
            {
            }
        }
    }
}
