using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAddressBookTests
{
    public class GroupData
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
        
            
    }
}
