using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LinqToDB;

namespace WebAddressBookTests
{
    public class AddressBookDb: LinqToDB.Data.DataConnection
    {
        public AddressBookDb(): base("AddressBook") { }

        public ITable<GroupData> Groups => GetTable<GroupData>();
        public ITable<ContactData> Contacts => GetTable<ContactData>();


    }
}
