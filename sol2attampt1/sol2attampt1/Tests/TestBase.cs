using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;

namespace WebAddressBookTests
{
    [TestFixture]
    public class TestBase
    {
        public ApplicationManager App { get; set; }
        [SetUp]
        public void SetupApplicationManager()
        {
            App = ApplicationManager.GetInstance();
        }
    }
}
