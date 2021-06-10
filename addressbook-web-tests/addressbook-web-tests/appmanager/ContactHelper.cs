using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;

namespace WebAddressbookTests
{
    public class ContactHelper : HelperBase
    {

        public ContactHelper(ApplicationManager manager)
            : base(manager)
        {
        }
        public ContactHelper Create(ContactData contact)
        {
            manager.Navigator.OpenHomePage();
            InitNewContactCreation();
            FillContactForm(contact);
            SubmitContact();
            ReturnToHomePage();
            return this;
        }
        public ContactHelper Modify(int p, ContactData newData)
        {
            manager.Navigator.OpenHomePage();
            InitContactModification(p);
            FillContactForm(newData);
            SubmitContactModification();
            ReturnToHomePage();
            return this;
        }

        public ContactHelper Remove(int p)
        {
            manager.Navigator.OpenHomePage();          
            SelectContact(p);
            RemoveContact();
            ConfirmContactRemoval();
            return this;
        }

        public ContactHelper InitNewContactCreation()
        {
            driver.FindElement(By.LinkText("add new")).Click();
            return this;
        }
        public ContactHelper FillContactForm(ContactData contact)
        {
            Type(By.Name("firstname"), contact.Firstname);
            Type(By.Name("middlename"), contact.Middlename);
            Type(By.Name("lastname"), contact.Lastname);
            return this;
        }
        public ContactHelper SubmitContact()
        {
            driver.FindElement(By.XPath("(//input[@name='submit'])[2]")).Click();
            return this;
        }
        public ContactHelper InitContactModification(int p)
        {
            driver.FindElement(By.XPath("(//img[@alt='Edit'])["+ p +"]")).Click();
            return this;
        }
        public ContactHelper SubmitContactModification()
        {
            driver.FindElement(By.Name("update")).Click();
            return this;
        }
        public ContactHelper ReturnToHomePage()
        {
            driver.FindElement(By.LinkText("home page")).Click();
            return this;
        }
        public ContactHelper SelectContact(int p)
        {
            driver.FindElement(By.XPath("(//input[@name='selected[]'])[" + p + "]")).Click();
            return this;
        }
        public ContactHelper RemoveContact()
        {
            driver.FindElement(By.XPath("//input[@value='Delete']")).Click();
            return this;
        }
        public ContactHelper ConfirmContactRemoval()
        {
            driver.SwitchTo().Alert().Accept();
            return this;
        }
        public ContactHelper CheckContactExistence()
        {
            manager.Navigator.OpenHomePage();
            if (!IsElementPresent(By.Name("selected[]")))
            {
                ContactData contact = new ContactData("first name");
                contact.Middlename = "middle name";
                contact.Lastname = "last name";
                Create(contact);
            }
            return this;

        }

    }
}
