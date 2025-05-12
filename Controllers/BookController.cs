using LibrarySystem.Entities;

namespace LibrarySystem.Controllers
{
    public class BookController
    {
        // Properties.
        private List<Book> allBooks = new List<Book>();
        private List<Book> borrowedBooks = new List<Book>();
        private List<Book> overdueBooks = new List<Book>();
        private List<Book> lostBooks = new List<Book>();

        // This method is used to prepoluate the list of books when the application is launched.
        public void PrePopulateBooks()
        {
            allBooks.Add(new Book("Harry Potter and the Philosopher's Stone", "J.K.Rowling", "1997", "LRN2653675141"));
            allBooks.Add(new Book("Harry Potter and the Chamber of Secrets", "J.K.Rowling", "1998", "LRN1948375620"));
            allBooks.Add(new Book("Harry Potter and the Prisoner of Azkaban", "J.K.Rowling", "1999", "LRN1948375620"));
            allBooks.Add(new Book("Harry Potter and the Goblet of Fire", "J.K.Rowling", "2000", "LRN4827361950"));
            allBooks.Add(new Book("Harry Potter and the Order of the Phoenix", "J.K.Rowling", "2003", "LRN9273846102"));
            allBooks.Add(new Book("Harry Potter and the Half-Blood Prince", "J.K.Rowling", "2005", "LRN4927835610"));
            allBooks.Add(new Book("Harry Potter and the Deathly Hallows", "J.K.Rowling", "2007", "LRN6273846102"));
            allBooks.Add(new Book("Catcher in the Rye", "J.D. Salinger", "1951", "LRN7543443912"));
            allBooks.Add(new Book("The Hunger Games", "Suzanne Collins", "2008", "LRN4335678217"));
            allBooks.Add(new Book("CatchingFire", "Suzanne Collins", "2009", "LRN8354976210"));
            allBooks.Add(new Book("Mockingjay", "Suzanne Collins", "2010", "LRN6728401953"));
            allBooks.Add(new Book("Wool", "Hugh Howey", "2011", "LRN1364521907"));
            allBooks.Add(new Book("Shift", "Hugh Howey", "2013", "LRN8492037482"));
            allBooks.Add(new Book("Dust", "Hugh Howey", "2013", "LRN5079128436"));
            allBooks.Add(new Book("One Flew Over the Cuckoo's Nest", "Ken Kesey", "1962", "LRN3569871204"));
        }

        // This method is used to filter books by a title and stores them in a new instance of a book list.
        public List<Book> SearchBooksByTitle(string title)
        {
            List<Book> booksFilteredByTitle = new List<Book>();

            foreach(Book book in allBooks)
                {
                    if(book.AccessToTitle.Contains(title, StringComparison.OrdinalIgnoreCase))
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
                if (book.AccessToAuthor.Contains(author, StringComparison.OrdinalIgnoreCase))
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
                if (book.AccessToTitle.Contains(keyword, StringComparison.OrdinalIgnoreCase) || book.AccessToAuthor.Contains(keyword, StringComparison.OrdinalIgnoreCase))
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
                if (book.AccessToLibraryReferenceNumber == libraryReferenceNumber)
                {
                    return book;
                }
            }
            return null;
        }

        // This method is used to return the list of all books.
        public List<Book> GetAllBooks()
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
                if (book.AccessToAvailabilityStatus == Book.BookState.Borrowed)
                {
                    borrowedBooks.Add(book);
                }
            }
            return borrowedBooks;
        }

        public List<User>? GetAccessToBorrowedBy(Book book)
        {
            return book.AccessToBorrowedBy;
        }

        // This method is used to get a list of all the overdue books.
        public List<Book> GetAllOverdueBooks()
        {
            overdueBooks.Clear();
            CheckForOverdueBooks();
            foreach (Book book in allBooks)
            {
                if (book.AccessToAvailabilityStatus == Book.BookState.Overdue)
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
                if (book.AccessToAvailabilityStatus == Book.BookState.Lost)
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
                if (book.AccessToDueDate < DateTime.Now && book.GetBookState() == Book.BookState.Borrowed)
                {
                    book.SetBookStateToOverdue();
                }
            }
        }
    }
}