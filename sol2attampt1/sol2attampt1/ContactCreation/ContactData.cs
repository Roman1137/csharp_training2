﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAddressBookTests
{
    class ContactData
    {
        public string FirstName { get; set; }
        public string MiddleName { get; set; } = "MiddleName";
        public string LastName { get; set; }
        public string NickName { get; set; } = "NickName";
        public string Tittle { get; set; } = "Tittle";
        public string Company { get; set; } = "Company";
        public string Address { get; set; } = "Address";
        public string Home { get; set; } = "Home";
        public string Mobile { get; set; } = "Mobile";
        public string Work { get; set; } = "Work";
        public string Fax { get; set; } = "Fax";
        public string Email { get; set; } = "E-mail";
        public string EmailSecondField { get; set; } = "E-mail2";
        public string EmailThirdField { get; set; } = "E-mail3";
        public string Homepage { get; set; } = "Homepage";
        public string AddressSecondField { get; set; } = "Address SecondField";
        public string HomeSecondField { get; set; } = "Home SecondField";
        public string Notes { get; set; } = "Notes";

        public ContactData(string firstName, string lastName)
        {
            this.FirstName = firstName;
            this.LastName = lastName;
        }
    }
}
