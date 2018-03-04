using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using LinqToDB.Mapping;

namespace WebAddressBookTests
{
    [Table(Name = "addressbook")]
    public class ContactData : IEquatable<ContactData>, IComparable<ContactData>
    {
        public string allInfo;
        private string allPhones;
        private string allEmails;
        [Column(Name = "firstname")]
        public string FirstName { get; set; }
        [Column(Name = "middlename")]
        public string MiddleName { get; set; } = "MiddleName";
        [Column(Name = "lastname")]
        public string LastName { get; set; }
        [Column(Name = "nickname")]
        public string NickName { get; set; } = "NickName";
        [Column(Name = "title")]
        public string Tittle { get; set; } = "Title";
        [Column(Name = "company")]
        public string Company { get; set; } = "Company";
        [Column(Name = "address")]
        public string Address { get; set; } = "Address";
        [Column(Name = "home")]
        public string HomePhone { get; set; } = "+3 (096) 1 2 3 4 5";
        [Column(Name = "mobile")]
        public string MobilePhone { get; set; } = "+3 (096) 5 4 3 2 1";
        [Column(Name = "work")]
        public string WorkPhone { get; set; } = "+3 (096) 9 8 7 6 5 4";
        [Column(Name = "fax")]
        public string Fax { get; set; } = "Fax";
        [Column(Name = "email")]
        public string Email { get; set; } = "Email";
        [Column(Name = "email2")]
        public string EmailSecondField { get; set; } = "Email2";
        [Column(Name = "email3")]
        public string EmailThirdField { get; set; } = "Email3";
        [Column(Name = "homepage")]
        public string Homepage { get; set; } = "Homepage";
        [Column(Name = "address2")]
        public string AddressSecondField { get; set; } = "Address SecondField";
        [Column(Name = "phone2")]
        public string HomeSecondField { get; set; } = "+3 (245) 5 7 4 22";
        [Column(Name = "notes")]
        public string Notes { get; set; } = "Notes";
        [Column(Name = "id"),PrimaryKey,Identity]
        public string Id { get; set; }
        [Column(Name = "deprecated")]
        public string Deprecated { get; set; }

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

        public static List<ContactData> GetAll()
        {
            using (var db = new AddressBookDb())
            {
                return (from c in db.Contacts.Where(x=>x.Deprecated == "0000-00-00 00:00:00") select c).ToList();
            }
        }
    }
}
