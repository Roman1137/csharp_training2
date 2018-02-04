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
            if(IsLoggedIn())
            {
                if (IsLoggedIn(account))
                {
                    return;
                }
                Logout();
            }
            Type(By.Name("user"),account.UserName);
            Type(By.Name("pass"),account.Password);
            Driver.FindElement(By.CssSelector("input[type=\"submit\"]")).Click();
        }

        public void Logout()
        {
            if (IsLoggedIn())
            {
                Driver.FindElement(By.LinkText("Logout")).Click();
            } 
        }
        public bool IsLoggedIn()
        {
            return IsElementPresent(By.Name("logout"));
        }
        public bool IsLoggedIn(AccountData account)
        {
            return IsLoggedIn()
                && Driver.FindElement(By.Name("logout")).FindElement(By.TagName("b")).Text 
                == "(" + account.UserName + ")"; 
        }
    }
}
