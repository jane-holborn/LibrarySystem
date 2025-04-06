using LibrarySystem.Controllers;
using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using System.Windows.Media.Animation;

namespace LibrarySystem.Entities
{
    public class User : Person
    {
        // Property.
        private decimal Fine;

        // This constructor is used to add a new user, properties are inherited from the abstract 'Person' class.
        public User(string userLibraryNumber, string userName, string userEmail)
            : base(userLibraryNumber, userName, userEmail)
        {
            Fine = 0;
        }

        // These getters are used to facilitate access to the properties.
        public string AccessToUserLibraryNumber
        {
            get { return UserLibraryNumber; }
        }
        public string AccessToUserName
        {
            get { return UserName; }
        }
        public string AccessToUserEmail
        {
            get { return UserEmail; }
        }
        public int AccessToNumberOfBorrowedBooks
        {
            get { return NumberOfBorrowedBooks; }
        }
        public List<Book> AccessToBorrowedBooks
        {
            get { return BorrowedBooks; }
        }
        public decimal AccessToFine
        {
            get { return Fine; }
        }
        public List<Book>? GetBorrowedBooks()
        {
            return BorrowedBooks;
        }

        // Setter methods used to set update the users borrowed book list and the number of books on loan.
        public void IncreaseNumberOfBorrowedBooks()
        {
            NumberOfBorrowedBooks++;
        }
        public void DecreaseNumberOfBorrowedBooks()
        {
            NumberOfBorrowedBooks--;
        }
    }
}