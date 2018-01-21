using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAddressBookTests
{
    class ContactData
    {
        private string firstName;
        private string middleName = "MiddleName";
        private string lastName;
        private string nickName = "NickName";
        private string tittle = "Tittle";
        private string company = "Company";
        private string address = "Address";
        private string home = "Home";
        private string mobile = "Mobile";
        private string work = "Work";
        private string fax = "Fax";
        private string email = "E-mail";
        private string emailSecondField = "E-mail2";
        private string emailThirdField = "E-mail3";
        private string homepage = "Homepage";
        private string addressSecondField = "Address SecondField";
        private string homeSecondField = "Home SecondField";
        private string notes = "Notes";

        public ContactData(string firstName, string lastName)
        {
            this.FirstName = firstName;
            this.LastName = lastName;
        }

        public string FirstName { get => firstName; set => firstName = value; }
        public string MiddleName { get => middleName; set => middleName = value; }
        public string LastName { get => lastName; set => lastName = value; }
        public string NickName { get => nickName; set => nickName = value; }
        public string Tittle { get => tittle; set => tittle = value; }
        public string Company { get => company; set => company = value; }
        public string Address { get => address; set => address = value; }
        public string Home { get => home; set => home = value; }
        public string Mobile { get => mobile; set => mobile = value; }
        public string Work { get => work; set => work = value; }
        public string Fax { get => fax; set => fax = value; }
        public string Email { get => email; set => email = value; }
        public string EmailSecondField { get => emailSecondField; set => emailSecondField = value; }
        public string EmailThirdField { get => emailThirdField; set => emailThirdField = value; }
        public string Homepage { get => homepage; set => homepage = value; }
        public string AddressSecondField { get => addressSecondField; set => addressSecondField = value; }
        public string HomeSecondField { get => homeSecondField; set => homeSecondField = value; }
        public string Notes { get => notes; set => notes = value; }
    }
}
