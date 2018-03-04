using System;
using System.Collections.Generic;
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
            Manager.Navigator.GoToGroupsPage();
            InitGroupCreation().
                FillGroupForm(groupInfoForCreation).
                SubmitGroupCreation();
            Manager.Navigator.GoToGroupsPage();
            return this;
        }

        public GroupHelper Modify(int numberOfItemToEdit, GroupData groupInfoForUpdate)
        {
            Manager.Navigator.GoToGroupsPage();
            SelectGroup(numberOfItemToEdit);
            InitGroupModification();
            FillGroupForm(groupInfoForUpdate);
            SubmitGroupModification();
            Manager.Navigator.GoToGroupsPage();
            return this;
        }

        internal GroupHelper Modify(GroupData groupToBeModified, GroupData groupInfoForUpdate)
        {
            Manager.Navigator.GoToGroupsPage();
            SelectGroup(groupToBeModified.Id);
            InitGroupModification();
            FillGroupForm(groupInfoForUpdate);
            SubmitGroupModification();
            Manager.Navigator.GoToGroupsPage();
            return this;
        }

        public GroupHelper Remove(int numberOfItemToDelete)
        {
            Manager.Navigator.GoToGroupsPage();
            SelectGroup(numberOfItemToDelete).
            InitGroupRemoval();
            Manager.Navigator.GoToGroupsPage();
            return this;
        }

        public GroupHelper Remove(GroupData numberOfItemToDelete)
        {
            Manager.Navigator.GoToGroupsPage();
            SelectGroup(numberOfItemToDelete.Id).
                InitGroupRemoval();
            Manager.Navigator.GoToGroupsPage();
            return this;
        }

        public GroupHelper InitGroupCreation()
        {
            Driver.FindElement(By.Name("new")).Click();
            return this;
        }

        public GroupHelper InitGroupModification()
        {
            Driver.FindElement(By.Name("edit")).Click();
            return this;
        }

        public GroupHelper InitGroupRemoval()
        {
            Driver.FindElement(By.XPath("(//input[@name='delete'])[2]")).Click();
            groupCache = null;
            return this;
        }

        public GroupHelper SubmitGroupModification()
        {
            Driver.FindElement(By.Name("update")).Click();
            groupCache = null;
            return this;
        }
        public GroupHelper SubmitGroupCreation()
        {
            Driver.FindElement(By.Name("submit")).Click();
            groupCache = null;
            return this;
        }


        public GroupHelper FillGroupForm(GroupData group)
        {
            Type(By.Name("group_name"), group.Name);
            Type(By.Name("group_header"), group.Header);
            Type(By.Name("group_footer"), group.Footer);
            return this;
        }
        public bool VerifyGroupExists(int index, GroupData infoForCreation)
        {
            Manager.Navigator.GoToGroupsPage();
            while (!IsElementPresent(By.XPath($"(//input[@name='selected[]'])[{index+1}]")))
            {
                infoForCreation = new GroupData{Name = AuthTestBase.RandomString(10)};
                Create(infoForCreation);
            }
            return true;
        }

        public GroupHelper SelectGroup(int index)
        {
            Driver.FindElement(By.XPath($"(//input[@name='selected[]'])[{index+1}]")).Click();
            return this;
        }

        public GroupHelper SelectGroup(string id)
        {
            Driver.FindElement(By.XPath($"(//input[@name='selected[]' and @value='{id}'])")).Click();
            return this;
        }

        public List<GroupData> groupCache { get; private set; }

        public List<GroupData> GetGroupsList()
        {
            if (groupCache == null)
            {
                groupCache = new List<GroupData>();
                Manager.Navigator.GoToGroupsPage();
                ICollection<IWebElement> elements = SearchCollection(By.CssSelector("span.group"));
                foreach (IWebElement element in elements)
                {
                    groupCache.Add(new GroupData
                    {
                        Id = element.FindElement(By.TagName("input")).GetAttribute("value")
                    });
                }

                string allGroupsNames = Driver.FindElement(By.CssSelector("div#content form")).Text;
                string[] parts = allGroupsNames.Split('\n');
                int shift = groupCache.Count - parts.Length;
                for (int i = 0; i < groupCache.Count; i++)
                {
                    if (i < shift)
                    {
                        groupCache[i].Name = "";
                    }
                    else
                    {
                        groupCache[i].Name = parts[i-shift].Trim();
                    }

                }
            }
            return new List<GroupData>(groupCache);
        }

        public int GetGroupCount()
        {
            return Driver.FindElements(By.CssSelector("span.group")).Count;
        }
    }
}
