﻿using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using Newtonsoft.Json;
using Excel = Microsoft.Office.Interop.Excel;
using WebAddressbookTests;

namespace addressbook_test_data_generators
{
    class Program
    {
        static void Main(string[] args)
        {
            int count = Convert.ToInt32(args[0]);
            string sourcetype = args[1];
            string filename = args[2];
            string format = args[3];

            StreamWriter writer = new StreamWriter(filename);


            if (sourcetype == "group")
            {
                List<GroupData> groups = new List<GroupData>();
                for (int i = 0; i < count; i++)
                {
                    groups.Add(new GroupData(TestBase.GenerateRandomString(10))
                    {
                        Header = TestBase.GenerateRandomString(100),
                        Footer = TestBase.GenerateRandomString(100)
                    });
                }

                if (format == "xml")
                {
                    writeGroupsToXmlFile(groups, writer);
                }
                else if (format == "json")
                {
                    writeToJsonFile(groups, writer);
                }
                else
                {
                    System.Console.Out.Write("Unrecognized format: " + format);
                }
            }

            else if (sourcetype == "contact")
            {                
                List<ContactData> contacts = new List<ContactData>();
                for (int i = 0; i < count; i++)
                {
                    contacts.Add(new ContactData(TestBase.GenerateRandomString(10))
                    {
                        Middlename = TestBase.GenerateRandomString(100),
                        Lastname = TestBase.GenerateRandomString(100)
                    });
                }
                if (format == "xml")
                {
                    writeContactsToXmlFile(contacts, writer);
                }
                else if (format == "json")
                {
                    writeToJsonFile(contacts, writer);
                }
                else
                {
                    System.Console.Out.Write("Unrecognized format: " + format);
                }
            }

            else
            {
                System.Console.Out.Write("Unrecognized sourcetype: " + sourcetype);
            }
                writer.Close();
                       
        }
        
        private static void writeGroupsToExcelFile(List<GroupData> groups, string filename)
        {
            Excel.Application app = new Excel.Application();
            app.Visible = true;
            Excel.Workbook wb = app.Workbooks.Add();
            Excel.Worksheet sheet = wb.ActiveSheet;            
            int row = 1;

            foreach (GroupData group in groups)
            {
                sheet.Cells[row, 1] = group.Name;
                sheet.Cells[row, 2] = group.Header;
                sheet.Cells[row, 3] = group.Footer;
                row++;
            }
            string fullPath = Path.Combine(Directory.GetCurrentDirectory(), filename);
            File.Delete(fullPath);
            wb.SaveAs(fullPath);

            wb.Close();
            app.Visible = false;
            app.Quit();
        }

        static void writeGroupsToCsvFile(List<GroupData> groups, StreamWriter writer)
        {
            foreach (GroupData group in groups)
            {
                writer.WriteLine(String.Format("${0},${1},${2}",
                    group.Name, group.Header, group.Footer));
            }
        }

        static void writeGroupsToXmlFile(List<GroupData> groups, StreamWriter writer)
        {
            new XmlSerializer(typeof(List<GroupData>)).Serialize(writer, groups);
        }

        //static void writeGroupsToJsonFile(List<GroupData> groups, StreamWriter writer)
        //{
        //    writer.Write(JsonConvert.SerializeObject(groups, Newtonsoft.Json.Formatting.Indented));

        //}        
        static void writeContactsToXmlFile(List<ContactData> contacts, StreamWriter writer)
        {
            new XmlSerializer(typeof(List<ContactData>)).Serialize(writer, contacts);
        }

        //static void writeContactsToJsonFile(List<ContactData> contacts, StreamWriter writer)
        //{
        //    writer.Write(JsonConvert.SerializeObject(contacts, Newtonsoft.Json.Formatting.Indented));
        //}

        static void writeToJsonFile<T>(T data, StreamWriter writer)
        {
            writer.Write(JsonConvert.SerializeObject(data, Newtonsoft.Json.Formatting.Indented));
        }

    }
}