using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace WebAddressbookTests
{
    public class AddingContactToGroupTests : AuthTestBase
    {
        [Test]       
        public void TestAddingContactToGroup()
        {
            app.Groups.CheckGroupExistence();
            app.Contacts.CheckContactExistence();

            List<ContactData> oldList = null;
            //List<ContactData> allContacts = null;
            ContactData contact = null;
            GroupData group = null;
            List <GroupData> groups = GroupData.GetAll();
            foreach (GroupData grouploop in groups)
            {
                //GroupData group = GroupData.GetAll()[0];
                oldList = grouploop.GetContacts();
                List<ContactData> allContacts = ContactData.GetAll().Except(oldList).ToList();
                if (allContacts.Any())
                {
                    contact = allContacts.First();
                    group = grouploop;
                    break;
                }

            }
            if (contact == null)
            {
                group = GroupData.GetAll()[0];
                app.Contacts.Create(new ContactData("first", "last"));
                oldList = group.GetContacts();
                contact = ContactData.GetAll().Except(oldList).First();
            }
            app.Contacts.AddContactToGroup(contact, group);

            List<ContactData> newList = group.GetContacts();
            oldList.Add(contact);
            newList.Sort();
            oldList.Sort();

            Assert.AreEqual(oldList, newList);
        }

    }
}
