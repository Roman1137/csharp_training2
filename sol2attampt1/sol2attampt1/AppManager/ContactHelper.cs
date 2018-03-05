using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;

namespace WebAddressBookTests
{
    public class ContactHelper : HelperBase
    {
        public ContactHelper(ApplicationManager manager) : base(manager)
        { }
        public ContactHelper Create(ContactData contact)
        {
            Manager.Navigator.GoToContactsPage();
            FillAllContactForms(contact).
            SubmitContactCreations();
            Manager.Navigator.GoToHomePage();
            return this;
        }

        public ContactHelper Modify(int numberOfItemTModify, ContactData contactInfoForUpdate)
        {
            Manager.Navigator.GoToHomePage();
            InitContactModification(numberOfItemTModify);
            FillAllContactForms(contactInfoForUpdate);
            SubmitContactModification();
            Manager.Navigator.GoToHomePage();
            return this;
        }

        internal ContactHelper Modify(ContactData contactToBeModified, ContactData contactInfoForUpdate)
        {
            Manager.Navigator.GoToHomePage();
            InitContactModification(contactToBeModified.Id);
            FillAllContactForms(contactInfoForUpdate);
            SubmitContactModification();
            Manager.Navigator.GoToHomePage();
            return this;
        }

        public ContactHelper Delete(ContactData contactToBeRemoved)
        {
            Manager.Navigator.GoToHomePage();
            SelectCheckBox(contactToBeRemoved.Id).
                SubmitContactRemoval();
            Manager.Navigator.GoToHomePage();
            return this;
        }

        public ContactHelper Delete(int numberOfElementToDelete)
        {
            Manager.Navigator.GoToHomePage();
            SelectCheckBox(numberOfElementToDelete).
            SubmitContactRemoval();
            Manager.Navigator.GoToHomePage();
            return this;
        }

        public ContactHelper SubmitContactCreations()
        {
            Driver.FindElement(By.Name("submit")).Click();
            contactCache = null;
            return this;
        }
        public ContactHelper SubmitContactModification()
        {
            Driver.FindElement(By.Name("update")).Click();
            contactCache = null;
            return this;
        }

        public ContactHelper SubmitContactRemoval()
        {
            Driver.FindElement(By.XPath("//input[@value='Delete']")).Click();
            Driver.SwitchTo().Alert().Accept(); // it will click on OK button
            contactCache = null;
            return this;
        }

        public ContactHelper FillAllContactForms(ContactData contact)
        {
            //Filling fields in the first block

            Type(By.Name("firstname"), contact.FirstName);
            Type(By.Name("middlename"), contact.MiddleName);
            Type(By.Name("lastname"), contact.LastName);
            Type(By.Name("nickname"), contact.NickName);
            Type(By.Name("title"), contact.Tittle);
            Type(By.Name("company"), contact.Company);
            Type(By.Name("address"), contact.Address);
            //Filling fields in the second block
            Type(By.Name("home"), contact.HomePhone);
            Type(By.Name("mobile"), contact.MobilePhone);
            Type(By.Name("work"), contact.WorkPhone);
            Type(By.Name("fax"), contact.Fax);
            //Filling fields in the third block
            Type(By.Name("email"), contact.Email);
            Type(By.Name("email2"), contact.EmailSecondField);
            Type(By.Name("email3"), contact.EmailThirdField);
            Type(By.Name("homepage"), contact.Homepage);
            //Filling fields in the "Secondary" block
            Type(By.Name("address2"), contact.AddressSecondField);
            Type(By.Name("phone2"), contact.HomeSecondField);
            Type(By.Name("notes"), contact.Notes);
            return this;
        }
        public ContactHelper SelectCheckBox(int index)
        {
            Driver.FindElement(By.XPath($"(//input[@name='selected[]'])[{index + 1}]")).Click();
            return this;
        }

        public ContactHelper SelectCheckBox(string index)
        {
            Driver.FindElement(By.XPath($"(//input[@name='selected[]' and @value='{index}'])")).Click();
            return this;
        }

        public ContactHelper InitContactModification(int indexOfContact)
        {
            Driver.FindElement(By.XPath($"(//img[@title='Edit'])[{indexOfContact + 1}]")).Click();
            return this;
        }

        private ContactHelper InitContactModification(string id)
        {
            var row = Driver.FindElement(By.XPath($".//input[@value='{id}']/../.."));
            var element = row.FindElement(By.XPath("(.//img[@title='Edit'])"));
            element.Click();
            return this;
        }

        public bool VerifyContactExists(int indexOfContact, ContactData contactInfoForCreation)
        {
            Manager.Navigator.GoToHomePage();
            while (!IsElementPresent(By.XPath($"(//tr[@name='entry'])[{indexOfContact + 1}]")))
            {
                contactInfoForCreation = new ContactData(AuthTestBase.RandomString(10), AuthTestBase.RandomString(10));
                Create(contactInfoForCreation);
            }
            return true;
        }

        public List<ContactData> contactCache { get; private set; }

        public List<ContactData> GetContactsList()
        {
            if (contactCache == null)
            {
                contactCache = new List<ContactData>();
                Manager.Navigator.GoToHomePage();
                ICollection<IWebElement> elements = SearchCollection(By.CssSelector("[name=entry]"));
                //string allText = Driver.FindElement(By.TagName("tbody")).Text;
                foreach (IWebElement element in elements)
                {
                    var lastName = element.FindElement(By.CssSelector("[name=entry] td:nth-of-type(2)")).Text;
                    var firstName = element.FindElement(By.CssSelector("[name=entry] td:nth-of-type(3)")).Text;
                    contactCache.Add(new ContactData(firstName, lastName)
                    {
                        Id = element.FindElement(By.Name("selected[]")).GetAttribute("value")
                    });
                }
            }
            return new List<ContactData>(contactCache);
        }

        public int GetContactCount()
        {
            return SearchCollection(By.CssSelector("[name=entry]")).Count;
        }

        public ContactData GetContactInfoFromTable(int contactIndex)
        {
            Manager.Navigator.GoToHomePage();
            IList<IWebElement> cells = SearchCollection(By.Name("entry"));
            IList<IWebElement> cellsOfIndex = SearchCollection(By.TagName("td"), cells[contactIndex]);

            var lastName = cellsOfIndex[1].Text;
            var firstName = cellsOfIndex[2].Text;
            var address = cellsOfIndex[3].Text;

            var allEmails = cellsOfIndex[4].Text;
            var allPhones = cellsOfIndex[5].Text;

            return new ContactData(firstName, lastName)
            {
                Address = address,
                AllEmails = allEmails,
                AllPhones = allPhones
            };
        }

        public ContactData GetContactInfoFromEditForm(int contactIndex, bool isItFordetailsPage = false)
        {
            Manager.Navigator.GoToHomePage();
            InitContactModification(contactIndex);
            var firstName = GetTextOfAttributeValue(By.Name("firstname"), "value");
            var lastName = GetTextOfAttributeValue(By.Name("lastname"), "value");
            var middleName = GetTextOfAttributeValue(By.Name("middlename"), "value");
            var nickName = GetTextOfAttributeValue(By.Name("nickname"), "value");

            var companyName = GetTextOfAttributeValue(By.Name("company"), "value");
            var title = GetTextOfAttributeValue(By.Name("title"), "value");
            var address = Driver.FindElement(By.Name("address")).Text;

            var homePhoneValue = GetTextOfAttributeValue(By.Name("home"), "value");
            var homePhone = SelectTheRightValue(homePhoneValue, "H:", isItFordetailsPage);

            var mobilePhoneValue = GetTextOfAttributeValue(By.Name("mobile"), "value");
            var mobilePhone= SelectTheRightValue(mobilePhoneValue, "M:", isItFordetailsPage);

            var workPhoneValue = GetTextOfAttributeValue(By.Name("work"), "value");
            var workPhone = SelectTheRightValue(workPhoneValue, "W:", isItFordetailsPage);

            var faxValue = GetTextOfAttributeValue(By.Name("fax"), "value");
            var fax = SelectTheRightValue(faxValue, "F:", isItFordetailsPage);

            var email = GetTextOfAttributeValue(By.Name("email"), "value");
            var emailSecond = GetTextOfAttributeValue(By.Name("email2"), "value");
            var emailThird = GetTextOfAttributeValue(By.Name("email3"), "value");

            var homepageValue = Driver.FindElement(By.Name("homepage")).GetAttribute("value");
            var homepage = SelectTheRightValue(homepageValue, "Homepage:", isItFordetailsPage);

            var addressSecond = Driver.FindElement(By.Name("address2")).Text;

            var homePhoneSecondValue = Driver.FindElement(By.Name("phone2")).GetAttribute("value");
            var homePhoneSecond = SelectTheRightValue(homePhoneSecondValue, "P:", isItFordetailsPage);

            var notes = Driver.FindElement(By.Name("notes")).Text;

            return new ContactData
            {
                FirstName = firstName,
                LastName = lastName,
                MiddleName = middleName,
                NickName = nickName,
                Company = companyName,
                Tittle = title,
                Address = address,
                HomePhone = homePhone,
                MobilePhone = mobilePhone,
                WorkPhone = workPhone,
                Fax = fax,
                Email = email,
                EmailSecondField = emailSecond,
                EmailThirdField = emailThird,
                Homepage = homepage,
                AddressSecondField = addressSecond,
                HomeSecondField = homePhoneSecond,
                Notes = notes
            };
        }

        public ContactData GetContactInfoFromDetailsForm(int numberOfContact)
        {
            Manager.Navigator.GoToHomePage();
            OpenDetailsForm(numberOfContact);
            var allInfo = (Driver.FindElement(By.CssSelector("#content")).Text);
            return new ContactData
            {
                AllInfo = allInfo
            };
        }

        public ContactHelper OpenDetailsForm(int numberOfContact)
        {
            Driver.FindElement(By.XPath($"(//img[@title='Details'])[{numberOfContact + 1}]")).Click();
            return this;
        }

        public string GetTextOfAttributeValue(By locator, string attributeValue)
        {
            return Driver.FindElement(locator).GetAttribute(attributeValue);
        }

        public string SelectTheRightValue(string fieldValue, string additionalSymbol, bool fordetailsPage = false)
        {
            if (fieldValue != "" && fordetailsPage)
            {
                return additionalSymbol + fieldValue;
            }
            else if (fieldValue != "")
            {
                return fieldValue;
            }
            else
            {
                return "";
            }
        }

        public void AddContactToGroup(ContactData contactToSelect, GroupData group)
        {
            Manager.Navigator.GoToHomePage();
            SetGroupInFilter("[all]");
            SelectCheckBox(contactToSelect.Id);
            SelectGroupToAdd(group.Name);
            CommitActionForContact();
        }

        public void CommitActionForContact(bool addToGroup = true)
        {
            if (addToGroup)
            {
                Driver.FindElement(By.Name("add")).Click();
            }
            else
            {
                Driver.FindElement(By.Name("remove")).Click();
            }
            var wait = new WebDriverWait(Driver,TimeSpan.FromSeconds(5));
            wait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector("div.msgbox")));
        }

        public void SelectGroupToAdd(string groupName)
        {
            new SelectElement(Driver.FindElement(By.Name("to_group"))).SelectByText(groupName);
        }

        public void SetGroupInFilter(string nameOfGroup)
        {
            new SelectElement(Driver.FindElement(By.Name("group"))).SelectByText(nameOfGroup);
        }

        public void RemoveContactFromGroup(ContactData contactToRemove, GroupData group)
        {
            Manager.Navigator.GoToHomePage();
            SetGroupInFilter(group.Name);
            SelectCheckBox(contactToRemove.Id);
            CommitActionForContact(addToGroup: false);
        }

        public (ContactData contactToSelect, List<ContactData> oldList) VerifyContactFromNotThisGroupExists(GroupData group)
        {
            var oldList = group.GetContacts();
            var contactsToSelect = ContactData.GetAll().Except(oldList);
            if (!contactsToSelect.Any())
            {
                var contactToBeRemoved = oldList.First();
                RemoveContactFromGroup(contactToBeRemoved, group);
                oldList = group.GetContacts();
                contactsToSelect = ContactData.GetAll().Except(oldList);
                if (!contactsToSelect.Any())
                {
                    throw new ArgumentException("Из-за того, что в приложении можна " +
                                                "создавать группы с одинаковыми именами этот тест упал, а Вы нашли багу.");
                }
                return (contactsToSelect.First(), oldList);
            }

            return (contactsToSelect.First(),oldList);
        }

        public (ContactData contactToRemove, List<ContactData> oldList) VerifyContactToDeleteExists(GroupData group)
        {
            var oldList = group.GetContacts();
            if (!oldList.Any())
            {
                var contactToAdded = ContactData.GetAll().First();
                AddContactToGroup(contactToAdded, group);
                oldList = group.GetContacts();
                if (oldList.Count == 0)
                {
                    throw  new ArgumentException("Из-за того, что в приложении можна " +
                                                 "создавать группы с одинаковыми именами этот тест упал, а Вы нашли багу.");
                }
                return (oldList.First(), oldList);
            }

            return (oldList.First(), oldList);
        }
    }
}
