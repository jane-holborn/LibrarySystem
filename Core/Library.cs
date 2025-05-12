using LibrarySystem.Controllers;
using LibrarySystem.Entities;

namespace LibrarySystem
{
    // The library class is the central hub through which everything is managed. 
    public class Library
    {
        // Properties
        private static Library libraryInstance;
        private BookController bookController;
        private UserController userController;
        private LibrarianController LibrarianController;

        // This constructor is used to create an instance of a library. Each instance of a library generates an instance of each controller required to access all the functionality.
        public Library()
        {
            bookController = new BookController();
            userController = new UserController();
            LibrarianController = new LibrarianController();
        }

        // Method to access the book controller from the main window.
        public BookController GetBookController()
        {
            return bookController;
        }

        // Method to access the user controller from the main window.
        public UserController GetUserController()
        {
            return userController;
        }

        // Method to access the book controller from the main window.
        public LibrarianController GetLibrarianController()
        {
            return LibrarianController;
        }

        // Static method to get the instance of the library when navigating between windows.
        public static Library GetInstance()
        {
            if (libraryInstance == null)
            {
                libraryInstance = new Library();
            }
            return libraryInstance;
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
                return "You currently have overdue books. Please see a librarian to return your overdue books and pay your late fees before borrowing another book.";
            }
            else if(user.Fine > 0)
            {
                return "You currently have outstanding fines. Please see a librarian to pay your late fees before borrowing another book.";
            }

            else
            {
                if (bookToBorrow.AccessToAvailabilityStatus == Book.BookState.Available)
                {
                    if (userController.CanBorrowMoreBooks(user))
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
        public string ReturnBook(Book bookToReturn, User user)
        {
            if (user.BorrowedBooks.Contains(bookToReturn))
            {
                switch (bookToReturn.AccessToAvailabilityStatus)
                {
                    case Book.BookState.Borrowed:
                        bookToReturn.SetBookStateToAvailable();
                        bookToReturn.ClearDueDate();
                        user.GetBorrowedBooks().Remove(bookToReturn);
                        bookToReturn.RemoveUserFromBorrowedBy(user);
                        user.DecreaseNumberOfBorrowedBooks();
                        return $"You have successfully returned {bookToReturn.AccessToTitle}";
                    case Book.BookState.Overdue:
                        return "Please see a librarian to pay your late fees before returning the book.";
                    case Book.BookState.Lost:
                        return "This book is listed as 'Lost'. Please see a librarian to resolve this issue.";
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
        public List<User> GetUsersBorrowingBooks(Book book)
        {
            List<User> usersBorrowing = new List<User>();

            foreach (User user in userController.GetListOfAllUsersWithBorrowedBooks())
            {
                if (user.BorrowedBooks.Contains(book))
                {
                    usersBorrowing.Add(user);
                }
            }
            return usersBorrowing;
        }

        // This method is used to add fine amounts to user accounts.
        public double AddFine(User user, double fineAmount)
        {
            double Fine = user.Fine;
            if (fineAmount >= 0 && fineAmount <= 6)
            {
                Fine += fineAmount;
                user.setFine(Fine);
                return 0;
            }
            else if (fineAmount < 0)
            {
                return -1;
            }
            else
            {
                return -2;
            }
        }

        // This method is used to pay off fine. Can be paid off in installments.
        public double PayFine(User user, double amountToPay)
        {
            double Fine = user.Fine;
            if (amountToPay == Fine)
            {
                Fine -= amountToPay;
                user.setFine(Fine);
                foreach (Book book in user.GetBorrowedBooks())
                {
                    book.SetBookStateToAvailable();
                }
                return 0;
            }
            else if (amountToPay < 0)
            {
                return -1;
            }
            else if (amountToPay < Fine)
            {
                return -2;
            }
            else
            {
                return -3;
            }
        }

        // This method is used to find the users selected book in the users borrowed list.
        public Book FindBookInUsersBorrowedList(string libraryReferenceNubmer, User user)
        {
            foreach (Book book in user.GetBorrowedBooks())
            {
                if (book.AccessToLibraryReferenceNumber == libraryReferenceNubmer)
                {
                    return book;
                }
            }
            return null;
        }
    }
}