using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;

namespace WebAddressBookTests
{
    [TestFixture]
    public class TestBase
    {
        public static bool PERFORM_LONG_UI_TESTS = true;
        public ApplicationManager App { get; set; }
        [SetUp]
        public void SetupApplicationManager()
        {
            App = ApplicationManager.GetInstance();
        }

        public static Random rnd = new Random();

        public static string GenerateRandomString(int maxNumber)
        {
            int l = Convert.ToInt32(rnd.NextDouble() * maxNumber);
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < l; i++)
            {
                builder.Append(Convert.ToChar(32 + Convert.ToInt32(rnd.NextDouble() * 65)));
            }

            return builder.ToString();
        }
    }
}
