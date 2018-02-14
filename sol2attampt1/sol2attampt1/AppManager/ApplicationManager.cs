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
using OpenQA.Selenium.Chrome;

namespace WebAddressBookTests
{
    public class ApplicationManager
    {
        public IWebDriver Driver { get; set; }
        public string BaseUrl { get; set; }

        public LoginHelper Auth { get; set; }
        public NavigationHelper Navigator { get; set; }
        public GroupHelper Group { get; set; }
        public ContactHelper Contact { get; set; }
        public static ThreadLocal<ApplicationManager> app = new ThreadLocal<ApplicationManager>();

        private ApplicationManager ()
        {
            Driver = new ChromeDriver(); 
            BaseUrl = "http://localhost/";
            Driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            //initialize helper classes
            Auth = new LoginHelper(this);
            Navigator = new NavigationHelper(this, BaseUrl);
            Group = new GroupHelper(this);
            Contact = new ContactHelper(this);
        }
        public static ApplicationManager GetInstance()
        {
            if (!app.IsValueCreated)
            {
                ApplicationManager newInstance = new ApplicationManager();
                newInstance.Navigator.OpenHomePage();
                app.Value = newInstance;
                
            }   
            return app.Value;
        }
        ~ApplicationManager()
        {
            Auth.Logout();
            try
            {
                Driver.Close();
                Driver.Quit();
                Driver.Dispose();
                Driver = null;
            }
            catch (Exception)
            {
            }
        }
    }
}
