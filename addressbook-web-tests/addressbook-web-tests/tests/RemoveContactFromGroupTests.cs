using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace WebAddressbookTests
{    
    public class RemoveContactFromGroupTests : AuthTestBase
    {
        [Test]
        public void RemoveContactFromGroup()
        {
            GroupData group = GroupData.GetAll()[0];           
            List<ContactData> oldList = group.GetContacts();
            if (!oldList.Any())
            {
                ContactData contact = ContactData.GetAll()[0];
                app.Contacts.AddContactToGroup(contact, group);
                oldList = group.GetContacts();
            }
            ContactData contactToRemove = oldList[0];

            app.Contacts.RemoveContactFromGroup(contactToRemove, group);

            List<ContactData> newList = group.GetContacts();
            oldList.Remove(contactToRemove);
            newList.Sort();
            oldList.Sort();

            Assert.AreEqual(oldList, newList);
        }

    }
}
