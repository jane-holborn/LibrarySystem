using LibrarySystem.Controllers;
using LibrarySystem.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace LibrarySystem
{
    // The library class is the central hub through which everything is managed. 
    public class Library
    {
        // Properties
        private static Library LibraryInstance;
        private BookController BookController;
        private UserController UserController;
        private LibrarianController LibrarianController;


        // This constructor is used to create an instance of a library. Each instance of a library generates an instance of each controller required to access all the functionality.
        public Library()
        {
            BookController = new BookController();
            UserController = new UserController();
            LibrarianController = new LibrarianController();
        }

        // Method to access the book controller from the main window.
        public BookController GetBookController()
        {
            return BookController;
        }

        // Method to access the user controller from the main window.
        public UserController GetUserController()
        {
            return UserController;
        }

        // Method to access the book controller from the main window.
        public LibrarianController GetLibrarianController()
        {
            return LibrarianController;
        }

        // Static method to get the instance of the library when navigating between windows.
        public static Library GetInstance()
        {
            if (LibraryInstance == null)
            {
                LibraryInstance = new Library();
            }
            return LibraryInstance;
        }

        // This method is used to borrow a book.
        public string BorrowBook(Book bookToBorrow, User user)
        {
            int numberOfOverdueBooks = 0;

            foreach (Book book in user.GetBorrowedBooks())
            {
                if (book.AccessToAvailabilityStatus == Book.BookState.Overdue)
                {
                    numberOfOverdueBooks++;                        
                } 
            }

            if (numberOfOverdueBooks > 0)
            {
                double fineAmount = (double)numberOfOverdueBooks * 2.00;
                user.setFine(fineAmount);
                return "You currently have overdue books. Please see a librarian to pay your late fees before borrowing another book.";
            }

            else
            {
                if (bookToBorrow.AccessToAvailabilityStatus == Book.BookState.Available)
                {
                    if (UserController.CanBorrowMoreBooks(user))
                    {
                        bookToBorrow.SetBookStateToBorrowed();
                        bookToBorrow.SetDueDate();
                        user.GetBorrowedBooks().Add(bookToBorrow);
                        if (bookToBorrow.AccessToBorrowedBy == null)
                        {
                            bookToBorrow.SetBorrowedBy();
                        }
                        bookToBorrow.AddUserToBorrowedBy(user);
                        user.IncreaseNumberOfBorrowedBooks();
                        return $"You have successfully borrowed {bookToBorrow.AccessToTitle}";
                    }
                    else
                    {
                        return "You currently have 3 books on loan. To borrow another one, you'll need to return one first.";
                    }
                }
                else
                {
                    return "This book is currently unavailable. Please choose one that is available.";
                }
            }
        }

        // This method is used to return a book.
        public string ReturnBook(Book book, User user)
        {
            if (user.AccessToBorrowedBooks.Contains(book))
            {
                switch (book.AccessToAvailabilityStatus)
                {
                    case Book.BookState.Borrowed:
                        book.SetBookStateToAvailable();
                        book.ClearDueDate();
                        user.GetBorrowedBooks().Remove(book);
                        user.DecreaseNumberOfBorrowedBooks();
                        return $"You have successfully returned {book.AccessToTitle}";
                    case Book.BookState.Overdue:
                        return "Please see a librarian to pay your late fees before returning the book.";
                    case Book.BookState.Lost:
                        return "This book is listed as 'Lost'. Please see a librarian to resolve this issue.";
                    case Book.BookState.Available:
                        return "This book is not currently being borrowed. Please verify the details and try again. " +
                            "Alternatively, you can click the 'Borrowed Books' button and select a book directly from your list if you haven't done so already.";
                    default:
                        return "There is an issue with the book status. Please see a librarian to resolve the issue";
                }
            }
            else
            {
                return "The book you are trying to return is not in your list of borrowed books. Please verify the details and try again. " +
                    "Alternatively, you can click the 'Borrowed Books' button and select a book directly from your list if you haven't done so already.";
            }
        }

        // This method is used to get users who are borrowing a book.
        public List<User> GetUsersBorrowingBook(Book book)
        {
            List<User> usersBorrowing = new List<User>();

            foreach( User user in UserController.GetListOfAllUsersWithBorrowedBooks())
            {
                if (user.AccessToBorrowedBooks.Contains(book))
                {
                    usersBorrowing.Add(user);
                }
            }
            return usersBorrowing;
        }

        // This method is used to add fine amounts to user accounts.
        public void AddFine(User user, double fineAmount)
        {
            double Fine = user.AccessToFine;
            Fine += fineAmount;
            user.setFine(Fine);
        }

        // This method is used to pay off fine. Can be paid off in installments.
        public bool PayFine(User user, double amountToPay)
        {
            double Fine = user.AccessToFine;
            Fine -= (double)amountToPay;
            user.setFine(Fine);
            return (Fine == 0) ? true : false;
        }
    }
}