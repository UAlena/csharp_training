using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class ContactModificationTests : AuthTestBase
    {
        [Test]
        public void ContactModificationTest()
        {
            app.Contacts.CheckContactExistence();

            ContactData newData = new ContactData("new first");
            newData.Middlename = "new middle";
            newData.Lastname = "new last";

            app.Contacts.Modify(1, newData);

        }

    }
}
