using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Collections.Generic;
using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class ContactInformationTests : ContactTestBase
    {
        [Test]
        public void TestContactInformation()
        {
            ContactData fromTable = app.Contacts.GetContactInformationFromTable(0);
            ContactData fromForm = app.Contacts.GetContactInformationFromForm(0);

            //verification
            Assert.AreEqual(fromTable, fromForm);
            Assert.AreEqual(fromTable.Address, fromForm.Address);
            Assert.AreEqual(fromTable.AllEmails, fromForm.AllEmails);
            Assert.AreEqual(fromTable.AllPhones, fromForm.AllPhones);

        }

        [Test]
        public void TestDetailedContactInformation()
        {
            string fromDetailedForm = app.Contacts.GetContactInformationFromDetailedForm(0);
            ContactData fromForm = app.Contacts.GetContactInformationFromForm(0);

            //verification
            Assert.AreEqual(fromDetailedForm, fromForm.AllDetailedInfo);

        }
    }
}
