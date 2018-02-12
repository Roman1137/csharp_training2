using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAddressBookTests
{
    public class GroupData:IEquatable<GroupData>,IComparable<GroupData>
    {
        public string Name { get; set; }
        public string Header { get; set; }  = "";
        public string Footer { get; set; }  = "";

        public GroupData(string name)
        {
            this.Name = name;
        }
        public GroupData(string name,string header,string footer)
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
            return Name.CompareTo(other.Name);
        }

        public override string ToString() => Name;
    }
}
