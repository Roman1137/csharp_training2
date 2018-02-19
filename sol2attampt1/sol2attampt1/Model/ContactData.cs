using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace WebAddressBookTests
{
    public class ContactData : IEquatable<ContactData>, IComparable<ContactData>
    {
        public string allInfo;
        private string allPhones;
        private string allEmails;
        public string FirstName { get; set; }
        public string MiddleName { get; set; } = "MiddleName";
        public string LastName { get; set; }
        public string NickName { get; set; } = "NickName";
        public string Tittle { get; set; } = "Title";
        public string Company { get; set; } = "Company";
        public string Address { get; set; } = "Address";
        public string HomePhone { get; set; } = "+3 (096) 1 2 3 4 5";
        public string MobilePhone { get; set; } = "+3 (096) 5 4 3 2 1";
        public string WorkPhone { get; set; } = "+3 (096) 9 8 7 6 5 4";
        public string Fax { get; set; } = "Fax";
        public string Email { get; set; } = "Email";
        public string EmailSecondField { get; set; } = "Email2";
        public string EmailThirdField { get; set; } = "Email3";
        public string Homepage { get; set; } = "Homepage";
        public string AddressSecondField { get; set; } = "Address SecondField";
        public string HomeSecondField { get; set; } = "+3 (245) 5 7 4 22";
        public string Notes { get; set; } = "Notes";
        public string Id { get; set; }

        public string AllInfo
        {
            get
            {
                if (allInfo != null)
                {
                    return allInfo;
                }
                else
                {
                    return $"{CleanUp(FirstName)}{CleanUp(MiddleName)}{CleanUp(LastName)}" +
                           $"{CleanUp(NickName)}" +
                           $"{CleanUp(Tittle)}" +
                           $"{CleanUp(Company)}" +
                           $"{CleanUp(Address)}" +
                           $"{CleanUp(HomePhone)}" +
                           $"{CleanUp(MobilePhone)}" +
                           $"{CleanUp(WorkPhone)}" +
                           $"{CleanUp(Fax)}" +
                           $"{CleanUp(Email)}" +
                           $"{CleanUp(EmailSecondField)}" +
                           $"{CleanUp(EmailThirdField)}" +
                           $"{CleanUp(Homepage)}" +
                           $"{CleanUp(AddressSecondField)}" +
                           $"{CleanUp(HomeSecondField)}" +
                           $"{CleanUp(Notes)}";
                }
            }
            set => allInfo = CleanUp(value);
        }

        public string AllPhones
        {
            get
            {
                if (allPhones != null)
                {
                    return allPhones;
                }

                else
                {
                    return (CleanUp(HomePhone) + CleanUp(MobilePhone) + CleanUp(WorkPhone) + CleanUp(HomeSecondField))
                        .Trim();
                }
            }
            set => allPhones = CleanUp(value);
        }

        public string AllEmails
        {
            get
            {
                if (allEmails != null || allEmails == "")
                {
                    return allEmails;
                }
                else
                {
                    return (CleanUp(Email) + CleanUp(EmailSecondField) + CleanUp(EmailThirdField)).Trim();
                }
            }
            set => allEmails = CleanUp(value);
        }

        private string CleanUp(string value)
        {
            if (value == null)
            {
                return "";
            }
            else
            {
                return Regex.Replace(value, @"[ \-(\n\r)]", "");
            }
        }

        public ContactData(string firstName, string lastName)
        {
            this.FirstName = firstName;
            this.LastName = lastName;
        }

        public ContactData()
        {
        }

        public bool Equals(ContactData other)
        {
            if (ReferenceEquals(other, null))
                return false;
            if (ReferenceEquals(other, this))
                return true;
            return FirstName == other.FirstName && LastName == other.LastName;
        }

        public int CompareTo(ContactData other)
        {
            if (ReferenceEquals(other, null))
                return 1;
            return String.Compare((FirstName + "," + LastName), other.FirstName + "," + other.LastName,
                StringComparison.Ordinal);
        }

        public override string ToString()
        {
           return "FirstName ="+FirstName + "\nLastName="+ LastName;
        } 
        public override int GetHashCode() => Tuple.Create(FirstName, LastName).GetHashCode();

    }
}
