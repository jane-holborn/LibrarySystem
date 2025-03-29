using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using LibrarySystem.Entities;

namespace LibrarySystem.Controllers
{
    public class BookController
    {
        private List<Book> ListOfAllBooks = new List<Book>();
        private List<Book> ListOfBorrowedBooks = new List<Book>();

        // This method is used to prepoluate the list of books when the application is launched.
        public void PrePopulateBooks()
        {
            ListOfAllBooks.Add(new Book("Harry Potter and the Philosopher's Stone", "J.K.Rowling", "1997", "LRN2653675141"));
            ListOfAllBooks.Add(new Book("Harry Potter and the Chamber of Secrets", "J.K.Rowling", "1998", "LRN1948375620"));
            ListOfAllBooks.Add(new Book("Harry Potter and the Prisoner of Azkaban", "J.K.Rowling", "1999", "LRN1948375620"));
            ListOfAllBooks.Add(new Book("Harry Potter and the Goblet of Fire", "J.K.Rowling", "2000", "LRN4827361950"));
            ListOfAllBooks.Add(new Book("Harry Potter and the Order of the Phoenix", "J.K.Rowling", "2003", "LRN9273846102"));
            ListOfAllBooks.Add(new Book("Harry Potter and the Half-Blood Prince", "J.K.Rowling", "2005", "LRN4927835610"));
            ListOfAllBooks.Add(new Book("Harry Potter and the Deathly Hallows", "J.K.Rowling", "2007", "LRN6273846102"));
            ListOfAllBooks.Add(new Book("Catcher in the Rye", "J.D. Salinger", "1951", "LRN7543443912"));
            ListOfAllBooks.Add(new Book("The Hunger Games", "Suzanne Collins", "2008", "LRN4335678217"));
            ListOfAllBooks.Add(new Book("CatchingFire", "Suzanne Collins", "2009", "LRN8354976210"));
            ListOfAllBooks.Add(new Book("Mockingjay", "Suzanne Collins", "2010", "LRN6728401953"));
            ListOfAllBooks.Add(new Book("Wool", "Hugh Howey", "2011", "LRN1364521907"));
            ListOfAllBooks.Add(new Book("Shift", "Hugh Howey", "2013", "LRN8492037482"));
            ListOfAllBooks.Add(new Book("Dust", "Hugh Howey", "2013", "LRN5079128436"));
            ListOfAllBooks.Add(new Book("One Flew Over the Cuckoo's Nest", "Ken Kesey", "1962", "LRN3569871204"));
        }

        // This method is used to filter books by a title and stores them in a new instance of a book list.
        public List<Book> SearchBooksByTitle(string title)
        {
            List<Book> booksFilteredByTitle = new List<Book>();

            foreach(Book book in ListOfAllBooks)
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

            foreach (Book book in ListOfAllBooks)
            {
                if (book.AccessToAuthor.Contains(author, StringComparison.OrdinalIgnoreCase))
                {
                    booksFilteredByAuthor.Add(book);
                }
            }
            return booksFilteredByAuthor;
        }

        // This method is used to filter books by an keywords and stores them in a new instance of a book list.
        public List<Book> SearchBooksByKeyword(string keyword)
        {
            List<Book> booksFilteredByKeyword = new List<Book>();

            foreach (Book book in ListOfAllBooks)
            {
                if (book.AccessToTitle.Contains(keyword, StringComparison.OrdinalIgnoreCase) || book.AccessToAuthor.Contains(keyword, StringComparison.OrdinalIgnoreCase))
                {
                    booksFilteredByKeyword.Add(book);
                }
            }
            return booksFilteredByKeyword;
        }
    }
}