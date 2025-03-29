using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystem
{
    // This class is abstract as it will never need to be instantiated and is a base class for other derived classes.
    public abstract class Person
    {
        // Properties. Set to protected so that derived classes can still access the properties as needed.
        protected string UserLibraryNumber;
        protected string UserName;
        protected string UserEmail;
        protected int NumberOfBorrowedBooks;
        protected List<Book>? BorrowedBooks;

        // This constructor is used as a base blueprint for the constructor of a derived classes.
        public Person(string userLibraryNumber, string userName, string userEmail)
        {
            UserLibraryNumber = userLibraryNumber;
            UserName = userName;
            UserEmail = userEmail; 
            NumberOfBorrowedBooks = 0;
            BorrowedBooks = new List<Book>();
        }
    }
}