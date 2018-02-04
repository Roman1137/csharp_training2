using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using System.Collections.Generic;

namespace WebAddressBookTests
{
    public class HelperBase
    {
        public ApplicationManager manager { get; set; }
        public IWebDriver Driver { get; set; }
        public HelperBase(ApplicationManager manager)
        {
            this.manager = manager;
            this.Driver = manager.Driver;
        }
        public void Type(By locator,string text)
        {
            if(text!=null)
            {
                Driver.FindElement(locator).Clear();
                Driver.FindElement(locator).SendKeys(text);
            }
        }
        public bool IsElementPresent(By locator)
        {
            Driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(0);
            IReadOnlyCollection<IWebElement> collection = Driver.FindElements(locator);
            Driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            if (collection.Count == 0)
                return false;
            else
                return true;
        }
    }
}