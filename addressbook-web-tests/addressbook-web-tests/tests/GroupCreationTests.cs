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
            app.Navigator.GoToGroupPage();
            app.Groups
                .InitGroupCreation()
                .FillGroupForm(new GroupData("qa1", "qa2", "qa3"))
                .SubmitGroupCreation()
                .ReturnToGroupsPage();
            app.Exit.Logout();
        }
                
    }
}
