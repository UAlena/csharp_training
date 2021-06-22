﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace WebAddressbookTests
{
    public class ContactData : IEquatable<ContactData>, IComparable<ContactData>
    {       
        private string allPhones;
        private string allEmails;

        public ContactData (string firstname)
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

        public string Firstname { get; set; }
        
     
        public string Middlename { get; set; }
       
        public string Lastname { get; set; }

        public string Address { get; set; }

        public string HomePhone { get; set; }

        public string MobilePhone { get; set; }

        public string WorkPhone { get; set; }
        public string Email1 { get; set; }

        public string Email2 { get; set; }

        public string Email3 { get; set; }

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
            return Regex.Replace(phone, "[-()]","") + "\r\n";
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

        private string PrepareEmails(string email)
        {
            if (email == null || email == "")
            {
                return "";
            }
            return email + "\r\n";
        }

        public string Id { get; set; }

    }
}
