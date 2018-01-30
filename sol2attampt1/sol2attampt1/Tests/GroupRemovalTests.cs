﻿using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;

namespace WebAddressBookTests
{
    [TestFixture]
    public class GroupRemovalTests: TestBase
    {
        [Test]
        public void VerifyGroupRemoval()
        {
            const int numberOfItemToDelete = 1;
            app.Group.Remove(numberOfItemToDelete);
        }
    }
}

