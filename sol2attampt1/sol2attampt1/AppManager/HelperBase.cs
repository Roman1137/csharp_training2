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
    }
}