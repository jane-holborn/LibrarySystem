using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using LibrarySystem.Entities;
using static LibrarySystem.Book;

namespace LibrarySystem.Controllers
{
    public class UserController
    {
        private User currentUser;
        private List<User> listOfAllUsers = new List<User>();
        private List<User> listOfAllUsersWithBorrowedBooks = new List<User>();

        // This method is used to prepoluate the list of users when the application is launched.
        public void PrePopulateUsers()
        {
            listOfAllUsers.Add(new User("U295684130", "James Doe", "jamesdoe@email.com"));
            listOfAllUsers.Add(new User("U804736591", "Charlie Evans", "charlie@email.com"));
            listOfAllUsers.Add(new User("U612905738", "Alice Johnson", "alicejohnson@email.com"));
            listOfAllUsers.Add(new User("U731259846", "Bob Smith", "bobsmith@email.com"));
            listOfAllUsers.Add(new User("U854970312", "Diana Lee", "dianalee@email.com"));
            listOfAllUsers.Add(new User("U417263890", "Ethan Brown", "ethanbrown@email.com"));
        }

        // This method is used to confirm user library number.
        public bool CheckUserLibraryNumber(string idNumber)
        {
            foreach (User user in listOfAllUsers)
            {
                if (idNumber == user.AccessToUserLibraryNumber)
                {
                    return true;
                }
            }
            return false;
        }

        // This method is used to find a user in the list of users by comparing user library numbers.
        public User GetUserByLibraryNumber(string userLibraryNumber)
        {
            foreach (User user in listOfAllUsers)
            {
                if (userLibraryNumber == user.AccessToUserLibraryNumber)
                {
                    return user;
                }
            }
            return null;
        }

        // This method is used to set the details of the current user.
        public void SetCurrentUser(User user)
        {
            currentUser = user;
        }

        // This method is used to get the details of the current librarian.
        public User GetCurrentUser()
        {
            return currentUser;
        }
        
        // This method is used to check how many books a user has on loan to manage the borrow limit for each user.
        public bool CanBorrowMoreBooks(User user)

        {
            if (user.AccessToNumberOfBorrowedBooks >= 3)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        
        // this method is used to return a list of all the users in the library system.
        public List<User> GetListOfAllUsers()
        {
            return listOfAllUsers;
        }

        // This method is update and return a list of all users with borrowed books.
        public List<User> GetListOfAllUsersWithBorrowedBooks()
        {
            foreach(User user in listOfAllUsers)
            {
                if(user.AccessToNumberOfBorrowedBooks > 0)
                {
                    listOfAllUsersWithBorrowedBooks.Add(user);
                }
            }
            return listOfAllUsersWithBorrowedBooks;
        }
    }
}