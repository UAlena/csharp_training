using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;


namespace WebAddressbookTests
{
    [TestFixture]
    public class GroupCreationTests : TestBase
    {
        
        [Test]
        public void GroupCreationTest()
        {
            OpenHomePage();
            Login(new AccountData("Admin", "secret"));
            GoToGroupPage();
            InitGroupCreation();
            FillGroupForm(new GroupData("qa1", "qa2", "qa3"));
            SubmitGroupCreation();
            ReturnToGroupsPage();
            Logout();
        }
                
    }
}
