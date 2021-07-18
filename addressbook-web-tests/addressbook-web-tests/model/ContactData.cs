using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using LinqToDB.Mapping;

namespace WebAddressbookTests
{
    [Table(Name = "addressbook")]
    public class ContactData : IEquatable<ContactData>, IComparable<ContactData>
    {
        private string allPhones;
        private string allEmails;
        private string allDetailedInfo;

        public ContactData()
        {        
        }

        public ContactData(string firstname)
        {
            Firstname = firstname;

        }
        public ContactData(string firstname, string lastname)
        {
            Firstname = firstname;
            Lastname = lastname;

        }
        public bool Equals(ContactData other)
        {
            if (Object.ReferenceEquals(other, null))
            {
                return false;
            }
            if (Object.ReferenceEquals(this, other))
            {
                return true;
            }
            return Lastname == other.Lastname && Firstname == other.Firstname;
        }
        public override int GetHashCode()
        {
            return (Firstname + Lastname).GetHashCode();
        }
        public override string ToString()
        {
            return "firstname= " + Firstname + "lastname = " + Lastname;
        }
        public int CompareTo(ContactData other)
        {
            if (Object.ReferenceEquals(other, null))
            {
                return 1;
            }
            if (this.Lastname == other.Lastname)
            {
                return Firstname.CompareTo(other.Firstname);
            }

            return Lastname.CompareTo(other.Lastname);
        }
        [Column(Name = "id"), PrimaryKey, Identity]
        public string Id { get; set; }

        [Column(Name ="firstname")]
        public string Firstname { get; set; }

        public string Middlename { get; set; }

        [Column(Name = "lastname")]
        public string Lastname { get; set; }

        public string Address { get; set; }

        public string HomePhone { get; set; }

        public string MobilePhone { get; set; }

        public string WorkPhone { get; set; }
        public string Email1 { get; set; }

        public string Email2 { get; set; }

        public string Email3 { get; set; }

        [Column(Name = "deprecated")]
        public string Deprecated { get; set; }

        public string AllPhones {
            get {
                if (allPhones != null)
                {
                    return allPhones;
                }
                else
                {
                    return (CleanUp(HomePhone) + CleanUp(MobilePhone) + CleanUp(WorkPhone)).Trim();
                }

            }
            set {
                allPhones = value;
            }
        }

        private string CleanUp(string phone)
        {
            if (phone == null || phone == "")
            {
                return "";
            }
            return Regex.Replace(phone, "[-()]", "") + "\r\n";
        }


        public string AllEmails
        {
            get
            {
                if (allEmails != null)
                {
                    return allEmails;
                }
                else
                {

                    return (PrepareEmails(Email1) + PrepareEmails(Email2) + PrepareEmails(Email3)).Trim();
                }

            }
            set
            {
                allEmails = value;
            }
        }
        public string AllDetailedInfo
        {
            get
            {
                allDetailedInfo = Firstname;
                if (Firstname != null && Lastname != null && Firstname != "" && Lastname != "")
                {
                    allDetailedInfo += (" " + Lastname);
                }
                else
                {
                    if (Lastname != null && Lastname != "")
                    {
                        allDetailedInfo += Lastname;
                    }
                }
                if (Address != null && Address != "")
                {
                    allDetailedInfo += ("\r\n" + Address);
                }
                if (HomePhone != null && HomePhone != "")
                {
                    allDetailedInfo += ("\r\n\r\nH: " + HomePhone);
                    if (MobilePhone != null && MobilePhone != "")
                    {
                        allDetailedInfo += ("\r\nM: " + MobilePhone);
                    }
                    if (WorkPhone != null && WorkPhone != "")
                    {
                        allDetailedInfo += ("\r\nW: " + WorkPhone);
                    }
                }
                else
                {
                    if (MobilePhone != null && MobilePhone != "")
                    {
                        allDetailedInfo += ("\r\n\r\nM: " + MobilePhone);
                        if (WorkPhone != null && WorkPhone != "")
                        {
                            allDetailedInfo += ("\r\nW: " + WorkPhone);
                        }
                    }
                    else
                    {
                        if (WorkPhone != null && WorkPhone != "")
                        {
                            allDetailedInfo += ("\r\n\r\nW: " + WorkPhone);
                        }
                    }
                    
                }
             
                if (Email1 != null && Email1 != "")
                {
                    allDetailedInfo += ("\r\n\r\n" + Email1);
                    if (Email2 != null && Email2 != "")
                    {
                        allDetailedInfo += ("\r\n" + Email2);
                    }
                    if (Email3 != null && Email3 != "")
                    {
                        allDetailedInfo += ("\r\n" + Email3);
                    }
                }
                else
                {
                    if (Email2 != null && Email2 != "")
                    {
                        allDetailedInfo += ("\r\n\r\n" + Email2);
                        if (Email3 != null && Email3 != "")
                        {
                            allDetailedInfo += ("\r\n" + Email3);
                        }
                    }
                    else
                    {
                        if (Email3 != null && Email3 != "")
                        {
                            allDetailedInfo += ("\r\n\r\n" + Email3);
                        }
                    }
                }

                return allDetailedInfo.Trim();
            }
            set
            {
                allDetailedInfo = value;
            }
        }

        private string PrepareEmails(string email)
        {
            if (email == null || email == "")
            {
                return "";
            }
            return email + "\r\n";
        }

        public static List<ContactData> GetAll()
        {
            using (AddressBookDB db = new AddressBookDB())
            {
                return (from c in db.Contacts.Where(x => x.Deprecated == "0000-00-00 00:00:00") select c).ToList();
            }
        }

    }
}
