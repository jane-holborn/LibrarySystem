using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using LibrarySystem.Entities;

namespace LibrarySystem.Controllers
{
    public class UserController
    {
        private List<User> ListOfAllUsers {  get; set; } = new List<User>();

        // This method is used to prepoluate the list of users when the application is launched.
        public void PrePopulateUsers()
        {
            ListOfAllUsers.Add(new User("U644268849","James Doe", "jamesdoe@email.com"));
            ListOfAllUsers.Add(new User("U779541212", "Charlie Evans", "charlie@email.com"));
        }

        // This method is used to confirm user library number
        public bool CheckUserLibraryNumber(string idNumber)
        {
            foreach (User user in ListOfAllUsers)
            {
                if (idNumber == user.AccessToUserLibraryNumber)
                {
                    return true;
                }
            }
            return false;
        }
    }
}