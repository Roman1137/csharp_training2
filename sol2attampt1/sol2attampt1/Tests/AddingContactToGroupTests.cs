using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LinqToDB.Mapping;
using NUnit.Framework;

namespace WebAddressBookTests
{
    public class AddingContactToGroupTests : AuthTestBase
    {
        GroupData groupInfoForCreation = new GroupData(RandomString(10), RandomString(10), RandomString(10));
        [Test]
        public void VerifyAddingContactToGroup()
        {
            App.Group.VerifyGroupExists(0, groupInfoForCreation);
            var allGroups = GroupData.GetAll();
            var group = allGroups[new Random().Next(0, allGroups.Count - 1)];
            
            var info = App.Contact.VerifyContactFromNotThisGroupExists(group);
            App.Contact.AddContactToGroup(info.contactToSelect, group);

            var newList = group.GetContacts();
            info.oldList.Add(info.contactToSelect);
            newList.Sort();
            info.oldList.Sort();

            Assert.AreEqual(info.oldList,newList);
        }
    }
}
