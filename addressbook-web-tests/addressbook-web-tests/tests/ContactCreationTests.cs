using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;


namespace WebAddressbookTests
{
    [TestFixture]
    public class ContactCreationTests : TestBase
    {
        [Test]
        public void ContactCreationTest()
        {
            ContactData contact = new ContactData("first");
            contact.Middlename = "middle";
            contact.Lastname = "last";
            app.Contacts
                .InitNewContactCreation()            
                .FillContactForm(contact)
                .SubmitContact();
            app.Exit.Logout();
        }
                 
     }
}
