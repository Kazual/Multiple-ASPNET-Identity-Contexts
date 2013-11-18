using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AccountUsers.Identity.Models;
using AccountUsers.Identity;
using PlatformUsers.Identity;
using PlatformUsers.Identity.Models;

namespace ConsoleApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            #region PLATFORM USERS

            Console.WriteLine("Startng PLATFORM USER methods...");
            Console.WriteLine("");

            Console.WriteLine("Creating Platform Roles...");
            Console.WriteLine("");

            ManagePlatformUsers.CreateRole("SuperAdmin").Wait();
            ManagePlatformUsers.CreateRole("Admin").Wait();
            ManagePlatformUsers.CreateRole("Observer").Wait();


            Console.WriteLine("Creating Platform Users...");
            Console.WriteLine("");

            CreatePlatformUser("Jack",  "Morrison", "Jack M.",  "Sector 12",    "jmorrison@theplatform.com",    "platformPassword123!").Wait();
            CreatePlatformUser("Jim",   "Morrison", "Jim M.",   "Sector 13",    "lizardking@theplatform.com",   "platformPassword123!").Wait();
            CreatePlatformUser("Jane",  "McClure", "Jane M.",   "Sector 12",    "jmcclure@theplatform.com",     "platformPassword123!").Wait();

            Console.WriteLine("");
            Console.WriteLine("****************************");
            Console.WriteLine("");

            Console.WriteLine("Assigning Roles to Platform Users...");
            Console.WriteLine("");

            var jack = ManagePlatformUsers.GetUser("jmorrison@theplatform.com");
            var jim = ManagePlatformUsers.GetUser("lizardking@theplatform.com");
            var jane = ManagePlatformUsers.GetUser("jmcclure@theplatform.com");

            ManagePlatformUsers.AddUserToRole(jack.Id, "SuperAdmin").Wait();
            ManagePlatformUsers.AddUserToRole(jim.Id, "Admin").Wait();
            ManagePlatformUsers.AddUserToRole(jane.Id, "Observer").Wait();

            Console.WriteLine("");
            Console.WriteLine("****************************");
            Console.WriteLine("");

            Console.WriteLine("Getting Platform Users...");
            Console.WriteLine("");

            foreach (PlatformUser platformUser in ManagePlatformUsers.GetUsers())
            {
                Console.WriteLine("* " + platformUser.FirstName + " " + platformUser.LastName + " (" + platformUser.UserName + ")");
            }

            #endregion

            Console.WriteLine("");
            Console.WriteLine("****************************");
            Console.WriteLine("****************************");
            Console.WriteLine("****************************");
            Console.WriteLine("");

            #region ACCOUNT USERS

            Console.WriteLine("Startng ACCOUNT USER methods...");
            Console.WriteLine("");

            Console.WriteLine("Creating Account User Roles...");
            Console.WriteLine("");

            ManageAccountUsers.CreateRole("Admin").Wait();
            ManageAccountUsers.CreateRole("Approver").Wait();
            ManageAccountUsers.CreateRole("Creative Director").Wait();


            Console.WriteLine("Creating Account Users...");
            Console.WriteLine("");

            CreateAccountUser("John",   "Smith",    "JSmith",   "jsmith@demo.com",  "passowrd123!").Wait();
            CreateAccountUser("June",   "Smith",    "JaneS",    "june@demo.com",    "passowrd123!").Wait();
            CreateAccountUser("Bob",    "Dylan",    "BDylan",   "bdylan@demo.com",  "passowrd123!").Wait();

            Console.WriteLine("");
            Console.WriteLine("****************************");
            Console.WriteLine("");


            Console.WriteLine("Assigning Roles to Acount Users...");
            Console.WriteLine("");

            var john = ManageAccountUsers.GetUser("jsmith@demo.com");
            var june = ManageAccountUsers.GetUser("june@demo.com");
            var bob = ManageAccountUsers.GetUser("bdylan@demo.com");

            ManageAccountUsers.AddUserToRole(john.Id, "Admin").Wait();
            ManageAccountUsers.AddUserToRole(june.Id, "Approver").Wait();
            ManageAccountUsers.AddUserToRole(bob.Id, "Creative Director").Wait();

            Console.WriteLine("");
            Console.WriteLine("****************************");
            Console.WriteLine("");


            Console.WriteLine("Getting Client Users...");
            Console.WriteLine("");

            foreach (AccountUser accountUser in ManageAccountUsers.GetUsers())
            {
                Console.WriteLine("* " + accountUser.FirstName + " " + accountUser.LastName + " (" + accountUser.UserName +")");
            }

            #endregion

            Console.ReadLine();
        }


        public async static Task<bool> CreatePlatformUser(string firstName, string lastName, string displayName, string location, string email, string password)
        {
            var result = await ManagePlatformUsers.CreateUser(
                new PlatformUser { FirstName = firstName, LastName = lastName, DisplayName = displayName, UserName = email, Location = location },
                password);

            if (result.Succeeded)
            {
                Console.WriteLine(firstName + " " + lastName + " Created!");
            }
            else
            {
                foreach (string error in result.Errors)
                {
                    Console.WriteLine(error);
                }
            }

            return result.Succeeded;
        }


        public async static Task<bool> CreateAccountUser(string firstName, string lastName, string displayName, string email, string password)
        {
            var result = await ManageAccountUsers.CreateUser(
                new AccountUser { FirstName = firstName, LastName = lastName, DisplayName = displayName, UserName = email },
                password);

            if (result.Succeeded)
            {
                Console.WriteLine(firstName + " " + lastName + " Created!");

            }
            else
            {
                foreach (string error in result.Errors)
                {
                    Console.WriteLine(error);
                }
            }

            return result.Succeeded;
        }
    }
}
