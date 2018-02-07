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
    public class GroupHelper : HelperBase
    {
        public GroupHelper(ApplicationManager manager) : base(manager)
        { }
        public GroupHelper Create(GroupData groupInfoForCreation)
        {
            manager.Navigator.GoToGroupsPage();
            InitGroupCreation().
                FillGroupForm(groupInfoForCreation).
                SubmitGroupCreation();
            manager.Navigator.GoToGroupsPage();
            return this;
        }

        public GroupHelper Modify(int numberOfItemToEdit, GroupData infoForUpdate, GroupData infoForCreation)
        {
            manager.Navigator.GoToGroupsPage();
            SelectGroup(numberOfItemToEdit, infoForCreation);
            InitGroupModification();
            FillGroupForm(infoForUpdate);
            SubmitGroupModification();
            manager.Navigator.GoToGroupsPage();
            return this;
        }

        public GroupHelper Remove(int numberOfItemToDelete, GroupData infoForCreation)
        {
            manager.Navigator.GoToGroupsPage();
            SelectGroup(numberOfItemToDelete, infoForCreation).
            InitGroupRemoval();
            manager.Navigator.GoToGroupsPage();
            return this;
        }
        public GroupHelper SubmitGroupCreation()
        {
            Driver.FindElement(By.Name("submit")).Click();
            return this;
        }

        public GroupHelper FillGroupForm(GroupData group)
        {
            Type(By.Name("group_name"), group.Name);
            Type(By.Name("group_header"), group.Header);
            Type(By.Name("group_footer"), group.Footer);
            return this;
        }

        public GroupHelper InitGroupCreation()
        {
            Driver.FindElement(By.Name("new")).Click();
            return this;
        }

        public GroupHelper InitGroupRemoval()
        {
            Driver.FindElement(By.XPath("(//input[@name='delete'])[2]")).Click();
            return this;
        }

        public GroupHelper SelectGroup(int index, GroupData infoForCreation)
        {
            while(!IsElementPresent(By.XPath("(//input[@name='selected[]'])[" + index + "]")))
            {
                Create(infoForCreation);
            }
            Driver.FindElement(By.XPath("(//input[@name='selected[]'])[" + index + "]")).Click();
            return this;
        }

        public GroupHelper SubmitGroupModification()
        {
            Driver.FindElement(By.Name("update")).Click();
            return this;
        }
        public GroupHelper InitGroupModification()
        {
            Driver.FindElement(By.Name("edit")).Click();
            return this;
        }

    }
}
