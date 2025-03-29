using LibrarySystem.Controllers;
using LibrarySystem.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public BookController getBookController()
        {
            return BookController;
        }

        // Method to access the user controller from the main window.
        public UserController getUserController()
        {
            return UserController;
        }

        // Method to access the book controller from the main window.
        public LibrarianController getLibrarianController()
        {
            return LibrarianController;
        }

        // Static method to get the instance of the library when navigating between windows.
        public static Library getInstance()
        {
            if (LibraryInstance == null)
            {
                LibraryInstance = new Library();
            }
            return LibraryInstance;
        }
    }
}