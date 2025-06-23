using LibrarySystem.Entities;
using System.Collections.ObjectModel;

namespace LibrarySystem.Controllers
{
    public class BookController
    {
        // Properties.
        private ObservableCollection<Book> allBooks = new ObservableCollection<Book>();
        private List<Book> borrowedBooks = new List<Book>();
        private List<Book> overdueBooks = new List<Book>();
        private List<Book> lostBooks = new List<Book>();

        // This method is used to filter books by a title and stores them in a new instance of a book list.
        public List<Book> SearchBooksByTitle(string title)
        {
            List<Book> booksFilteredByTitle = new List<Book>();

            foreach(Book book in allBooks)
                {
                    if(book.Title.Contains(title, StringComparison.OrdinalIgnoreCase))
                    {  
                        booksFilteredByTitle.Add(book);
                    }
                }
            return booksFilteredByTitle;
        }

        // This method is used to filter books by an Author and stores them in a new instance of a book list.
        public List<Book> SearchBooksByAuthor(string author)
        {
            List<Book> booksFilteredByAuthor = new List<Book>();

            foreach (Book book in allBooks)
            {
                if (book.Author.Contains(author, StringComparison.OrdinalIgnoreCase))
                {
                    booksFilteredByAuthor.Add(book);
                }
            }
            return booksFilteredByAuthor;
        }

        // This method is used to filter books by a keyword and store them in a new instance of a book list.
        public List<Book> SearchBooksByKeyword(string keyword)
        {
            List<Book> booksFilteredByKeyword = new List<Book>();

            foreach (Book book in allBooks)
            {
                if (book.Title.Contains(keyword, StringComparison.OrdinalIgnoreCase) || book.Author.Contains(keyword, StringComparison.OrdinalIgnoreCase))
                {
                    booksFilteredByKeyword.Add(book);
                }
            }
            return booksFilteredByKeyword;
        }

        // This method is used to find the selected book in the library system book list.
        public Book FindBookInAllBooks(string libraryReferenceNumber)
        {
            foreach(Book book in allBooks)
            {
                if (book.LibraryReferenceNumber == libraryReferenceNumber)
                {
                    return book;
                }
            }
            return null;
        }

        // This method is used to return the list of all books.
        public ObservableCollection<Book> GetAllBooks()
        {
            return allBooks;
        }

        // This method is used to get a list of all the borrowed books.
        public List<Book> GetAllBorrowedBooks()
        {
            borrowedBooks.Clear();
            CheckForOverdueBooks();
            foreach (Book book in allBooks)
            {
                if (book.AvailabilityStatus == Book.BookState.Borrowed)
                {
                    borrowedBooks.Add(book);
                }
            }
            return borrowedBooks;
        }

        // This method is used to get the user who has borrowed the selected book.
        public User GetAccessToBorrowedBy(Book book)
        {
            return book.BorrowedBy;
        }

        // This method is used to get a list of all the overdue books.
        public List<Book> GetAllOverdueBooks()
        {
            overdueBooks.Clear();
            CheckForOverdueBooks();
            foreach (Book book in allBooks)
            {
                if (book.AvailabilityStatus == Book.BookState.Overdue)
                {
                    overdueBooks.Add(book);
                }
            }
            return overdueBooks;
        }

        // This method is used to get a list of all the lost books.
        public List<Book> GetAllLostBooks()
        {
            lostBooks.Clear();
            foreach (Book book in allBooks)
            {
                if (book.AvailabilityStatus == Book.BookState.Lost)
                {
                    lostBooks.Add(book);
                }
            }
            return lostBooks;
        }

        // This method is used to delete the selected book from the list of lost books.
        public void DeleteBookFromLostBooks(Book book)
        {
            lostBooks.Remove(book);
        }

        // This method is used to delete the selected book from the list of all books.
        public void DeleteBook(Book book)
        {
            allBooks.Remove(book);
        }

        // This method is used to add a book to the list of all books.
        public void AddBook(string title, string author, string publicationDate, string libraryReferenceNumber)
        {
            allBooks.Add(new Book(title, author,publicationDate, libraryReferenceNumber));
        }

        // This method is used to check for overdue books and update the state of any books with a due date that has passed.
        public void CheckForOverdueBooks()
        {
            foreach (Book book in allBooks)
            {
                if (book.DueDate < DateTime.Now && book.GetBookState() == Book.BookState.Borrowed)
                {
                    book.SetBookStateToOverdue();
                }
            }
        }
    }
}