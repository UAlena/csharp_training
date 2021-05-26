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
            OpenHomePage();
            Login(new AccountData("Admin", "secret"));
            InitNewContactCreation();
            ContactData contact = new ContactData("first");
            contact.Middlename = "middle";
            contact.Lastname = "last";
            FillContactForm(contact);
            SubmitContact();
            Logout();
        }
                 
     }
}
