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
        const string endPointForGroupsPage = "addressbook/group.php";
        const string endPointForHomePage = "addressbook/";
        const string endPointForContactsPage = "addressbook/edit.php";

        public string BaseURL { get; set; }
        public NavigationHelper(ApplicationManager manager, string BaseURL)
              : base(manager)
        {
            this.BaseURL = BaseURL;
        }
        public void OpenHomePage()
        {
            Driver.Navigate().GoToUrl(BaseURL + endPointForHomePage);
        }

        public void GoToGroupsPage()
        {
            if (!IsThisPageOpened(BaseURL +endPointForGroupsPage, By.Name("new")))
            Driver.FindElement(By.LinkText("groups")).Click();
        }

        public void GoToHomePage()
        {
            if(!IsThisPageOpened(BaseURL+ endPointForHomePage,By.Id("search_count")))
            Driver.FindElement(By.LinkText("home")).Click();
        }

        public void GoToContactsPage()
        {
            if(!IsThisPageOpened(BaseURL+endPointForContactsPage, By.Name("submit")))
            Driver.FindElement(By.LinkText("add new")).Click();
        }
        private bool IsThisPageOpened(string Url, By locator)
        {
            Driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(0);
            if ((Driver.Url == Url) && (IsElementPresent(locator)))
            {
                Driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
                return true;
            }
            else
            {
                Driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
                return false;
            }
        }
    }
}
