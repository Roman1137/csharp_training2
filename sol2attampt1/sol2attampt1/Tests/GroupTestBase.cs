using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using WebAddressBookTests;

namespace WebAddressBookTests
{
    public  class GroupTestBase:AuthTestBase
    {
        [TearDown]
        public void CopareGroupsUI_DB()
        {
            if (PERFORM_LONG_UI_TESTS)
            {
                var fromUI = App.Group.GetGroupsList();
                var fromDB = GroupData.GetAll();
                fromUI.Sort();
                fromDB.Sort();
                Assert.AreEqual(fromUI, fromDB);
            }
        }
    }
}
