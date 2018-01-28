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
   public class GroupHelper:HelperBase
    {
        public GroupHelper(IWebDriver Driver) :base(Driver)
        { }
        public void SubmitGroupCreation() => Driver.FindElement(By.Name("submit")).Click();

        public void FillGroupForm(GroupData group)
        {
            Driver.FindElement(By.Name("group_name")).Clear();
            Driver.FindElement(By.Name("group_name")).SendKeys(group.Name);
            Driver.FindElement(By.Name("group_header")).Clear();
            Driver.FindElement(By.Name("group_header")).SendKeys(group.Header);
            Driver.FindElement(By.Name("group_footer")).Clear();
            Driver.FindElement(By.Name("group_footer")).SendKeys(group.Footer);
        }

        public void InitGroupCreation() => Driver.FindElement(By.Name("new")).Click();
        public void InitGroupRemoval() => Driver.FindElement(By.XPath("(//input[@name='delete'])[2]")).Click();

        public void SelectGroupToDelete(int index) => Driver.FindElement(By.XPath("(//input[@name='selected[]'])[" + index + "]")).Click();
    }
}
