using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LinqToDB.Mapping;

namespace WebAddressBookTests
{
    [Table(Name = "group_list")]
    public class GroupData : IEquatable<GroupData>, IComparable<GroupData>
    {
        [Column(Name = "group_name")]
        public string Name { get; set; }

        [Column(Name ="group_header")]
        public string Header { get; set; } = "";

        [Column(Name = "group_footer")]
        public string Footer { get; set; } = "";

        [Column(Name = "group_id"),PrimaryKey,Identity]
        public string Id { get; set; }

        public GroupData()
        {
        }

        public GroupData(string name)
        {
            this.Name = name;
        }
        public GroupData(string name, string header, string footer)
        {
            this.Name = name;
            this.Header = header;
            this.Footer = footer;
        }
        public bool Equals(GroupData other)
        {
            if (ReferenceEquals(other, null))
                return false;
            if (ReferenceEquals(other, this))
                return true;
            return Name == other.Name;
        }

        public override int GetHashCode() => Name.GetHashCode();

        public int CompareTo(GroupData other)
        {
            if (ReferenceEquals(other, null))
                return 1;
            return String.Compare(Name, other.Name, StringComparison.Ordinal);
        }

        public override string ToString()
        {
            return "Name=" + Name + "\nHeader=" + Header + "\nFooter=" + Footer;
        }

        public static List<GroupData> GetAll()
        {
            using (var db = new AddressBookDb())
            {
                return (from g in db.Groups select g).ToList();
            }
        }

        public List<ContactData> GetContacts()
        {
            using (var db = new AddressBookDb())
            {
                return (from c in db.Contacts
                    from gcr in db.GCR.Where(p => p.GroupId == Id && p.ContactId == c.Id)
                    select c).Distinct().ToList();
            }
        }
    }
}
