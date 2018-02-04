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
    public class LoginHelper:HelperBase
    { 
        public LoginHelper(ApplicationManager manager) :base(manager)
        { }
        public void Login(AccountData account)
        {
            Type(By.Name("user"),account.UserName);
            Type(By.Name("pass"),account.Password);
            Driver.FindElement(By.CssSelector("input[type=\"submit\"]")).Click();
        }
        public void Logout() => Driver.FindElement(By.LinkText("Logout")).Click();
    }
}
