using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace WebAddressBookTests
{
    public class ContactTestBase: AuthTestBase
    {
        [TearDown]
        public void CopareGroupsUI_DB()
        {
            if (PERFORM_LONG_UI_TESTS)
            {
                var fromUI = App.Contact.GetContactsList();
                var fromDB = ContactData.GetAll();
                fromUI.Sort();
                fromDB.Sort();
                Assert.AreEqual(fromUI, fromDB);
            }
        }
    }
}
