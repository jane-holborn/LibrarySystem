using LibrarySystem.Entities;
using System.Collections.ObjectModel;

namespace LibrarySystem.Controllers
{
    public class UserController
    {
        // Properties.
        private User currentUser;
        private ObservableCollection<User> allUsers = new ObservableCollection<User>();
        private List<User> allUsersWithBorrowedBooks = new List<User>();

        // This method is used to confirm a user exists in the library system by their user library number.
        public bool CheckUserLibraryNumber(string idNumber)
        {
            foreach (User user in allUsers)
            {
                if (idNumber == user.UserLibraryNumber)
                {
                    return true;
                }
            }
            return false;
        }

        // This method is used to find a user in the list of users by comparing user library numbers.
        public User GetUserByLibraryNumber(string userLibraryNumber)
        {
            foreach (User user in allUsers)
            {
                if (userLibraryNumber == user.UserLibraryNumber)
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

        // This method is used to get the details of the current user.
        public User GetCurrentUser()
        {
            return currentUser;
        }
        
        // This method is used to check how many books a user has on loan to manage the borrowing limit for each user.
        public bool CanBorrowMoreBooks(User user)

        {
            if (user.NumberOfBorrowedBooks >= 3)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        
        // This method is used to return a list of all the users in the library system.
        public ObservableCollection<User> GetAllUsers()
        {
            return allUsers;
        }

        // This method is update and return a list of all users with borrowed books.
        public List<User> GetAllUsersWithBorrowedBooks()
        {
            foreach(User user in allUsers)
            {
                if(user.NumberOfBorrowedBooks > 0)
                {
                    allUsersWithBorrowedBooks.Add(user);
                }
            }
            return allUsersWithBorrowedBooks;
        }

        // This method is used to find the selected user in the library system user list.
        public User FindUserInAllUsers(string UserLibraryNumber)
        {
            foreach (User user in allUsers)
            {
                if (user.UserLibraryNumber == UserLibraryNumber)
                {
                    return user;
                }
            }
            return null;
        }

        // This method is used to delete the selected user from the list of all users.
        public void DeleteUser(User user)
        {
            allUsers.Remove(user);
        }

        // This method is used to add a user to the list of all users.
        public void AddUser(string userLibraryNumber, string userName, string userEmail)
        {
            allUsers.Add(new User(userLibraryNumber, userName, userEmail));
        }

        // This method is used to filter users by a keyword and store them in a new instance of a user list.
        public List<User> SearchUsersByKeyword(string keyword)
        {
            List<User> usersFilteredByKeyword = new List<User>();

            foreach (User user in allUsers)
            {
                if (user.Name.Contains(keyword, StringComparison.OrdinalIgnoreCase) || user.UserLibraryNumber.Contains(keyword, StringComparison.OrdinalIgnoreCase) || user.Email.Contains(keyword, StringComparison.OrdinalIgnoreCase))
                {
                    usersFilteredByKeyword.Add(user);
                }
            }
            return usersFilteredByKeyword;
        }
    }
}