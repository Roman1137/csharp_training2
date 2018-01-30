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
    public class NavigationHelper : HelperBase
    {
        public string BaseURL { get; set; }
        public NavigationHelper(ApplicationManager manager, string BaseURL)
              : base(manager)
        {
            this.BaseURL = BaseURL;
        }
        public void OpenHomePage() => Driver.Navigate().GoToUrl(BaseURL + "addressbook/");
        public void GoToGroupsPage() => Driver.FindElement(By.LinkText("groups")).Click();
        public void ReturnToHomePage() => Driver.FindElement(By.LinkText("home")).Click();
        public void GoToContactsPage() => Driver.FindElement(By.LinkText("add new")).Click();
    }
}
