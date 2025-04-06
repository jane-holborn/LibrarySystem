using LibrarySystem.Controllers;
using LibrarySystem.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace LibrarySystem
{
    public partial class UserDashboard : Window
    {
        private MainWindow mainWindow;

        // This constructor is used to create a new instance of the User dashboard.
        public UserDashboard(Library libraryInstance, MainWindow mainWindowInstance)
        {
            InitializeComponent();
            mainWindow = mainWindowInstance;
        }

        private Book? selectedBook;

        // This button click event is used to carry out a search by title. It calls a method held in the BookController class.
        private void ButtonSearchByTitle_Click(object sender, RoutedEventArgs e)
        {
            Library.GetInstance().GetBookController().CheckForOverdueBooks();
            string titleSearchTerm = TextBoxSearch.Text;
            List<Book> booksFilteredByTitle = Library.GetInstance().GetBookController().SearchBooksByTitle(titleSearchTerm);
            ListBoxBooks.ItemsSource = booksFilteredByTitle;
            TextBlockStatus.Text = "Search complete. Results are displayed above. If no books appear, none matched your search. " +
                "To improve your search, try a different title, use a keyword search, or try fewer words to broaden your search.";
        }

        // This button click event is used to carry out a search by author. It calls a method held in the BookController class.
        private void ButtonSearchByAuthor_Click(object sender, RoutedEventArgs e)
        {
            Library.GetInstance().GetBookController().CheckForOverdueBooks();
            string authorSearchTerm = TextBoxSearch.Text;
            List<Book> booksFilteredByAuthor = Library.GetInstance().GetBookController().SearchBooksByAuthor(authorSearchTerm);
            ListBoxBooks.ItemsSource = booksFilteredByAuthor;
            TextBlockStatus.Text = "Search complete. Results are displayed above. If no books appear, none matched your search. To improve your search, try a keyword search, or try fewer letters or words to broaden your search.";
        }

        // This button click event is used to carry out a search by keyword. It calls a method held in the BookController class.
        private void ButtonSearchByKeyword_Click(object sender, RoutedEventArgs e)
        {
            Library.GetInstance().GetBookController().CheckForOverdueBooks();
            string keywordSearchTerm = TextBoxSearch.Text;
            List<Book> booksFilteredByKeyword = Library.GetInstance().GetBookController().SearchBooksByKeyword(keywordSearchTerm);
            ListBoxBooks.ItemsSource = booksFilteredByKeyword;
            TextBlockStatus.Text = "Search complete. Results are displayed above. If no books appear, none matched your search. To improve your search, try fewer characters or words to broaden your search.";
        }

        // This button click event is used to clear the list box contents by setting the items source back to null.
        private void ButtonClearListBox_Click(object sender, RoutedEventArgs e)
        {
            ListBoxBooks.ItemsSource = null;
            TextBoxSearch.Text = null;
            TextBlockStatus.Text = "Dashboard successfully cleared.";
        }

        // This button click event is used to borrow a book.
        private void ButtonBorrowBook_Click(object sender, RoutedEventArgs e)
        {
            // If a book is selected from the list.
            if (ListBoxBooks.SelectedItem != null)
            {
                Book selectedBookFromSearchResults = (Book)ListBoxBooks.SelectedItem;
                User currentUser = Library.GetInstance().GetUserController().GetCurrentUser();
                Book bookInLibrarySystem = Library.GetInstance().GetBookController().FindBookInAllBooks(selectedBookFromSearchResults.AccessToLibraryReferenceNumber);

                // And if the book exists in the library system.
                if (bookInLibrarySystem != null)
                {
                    string message = "This book is currently unavailable. Please choose one that is available.";
                    string borrowMessage = Library.GetInstance().BorrowBook(bookInLibrarySystem, currentUser);
                    if (message == borrowMessage)
                    {
                        TextBlockStatus.Text = borrowMessage;
                    }
                    else
                    {
                        TextBlockStatus.Text = borrowMessage;
                        ListBoxBooks.ItemsSource = null;
                        TextBoxSearch.Text = null;
                    }
                }
                else
                {
                    TextBlockStatus.Text = "The selected book is unavailable in the system. Please select another book or try again later.";
                }

            }
            else
            {
                TextBlockStatus.Text = "To borrow a book, please select one from the search results.";
            }
        }

        // This method is used to set the selected book by the user. The selected item is cast from 'obj' (default) to type 'Book' to allow access to the book properties.
        private void ListBoxBooks_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedBook = (Book)ListBoxBooks.SelectedItem;
        }

        // This button click event is used to view the list of books currently on loan.
        private void ButtonViewBorrowedBooks_Click(object sender, RoutedEventArgs e)
        {
            User currentUser = Library.GetInstance().GetUserController().GetCurrentUser();
            List<Book> BorrowedBooks = currentUser.GetBorrowedBooks();

            if (BorrowedBooks.Count > 0)
            {
                ListBoxBooks.ItemsSource = BorrowedBooks;
                TextBoxSearch.Text = null;
                TextBlockStatus.Text = "The books you currently have on loan, and their due dates, are listed above";
            }
            else
            {
                TextBoxSearch.Text = null;
                TextBlockStatus.Text = "You currently have no borrowed books.";
            }
        }
        
        // This button click event is used to return a book.
        private void ButtonReturnBook_Click(object sender, RoutedEventArgs e)
        {
            if (ListBoxBooks.SelectedItem != null)
            {
                Book selectedBookFromSearchResults = (Book)ListBoxBooks.SelectedItem;
                User currentUser = Library.GetInstance().GetUserController().GetCurrentUser();
                Book bookInUsersBorrowedList = Library.GetInstance().GetBookController().FindBookInUsersBorrowedlist(selectedBookFromSearchResults.AccessToLibraryReferenceNumber, currentUser);

                if (bookInUsersBorrowedList != null)
                {
                    string borrowMessage = Library.GetInstance().ReturnBook(bookInUsersBorrowedList, currentUser);
                    TextBlockStatus.Text = borrowMessage;
                    ListBoxBooks.ItemsSource = null;
                    TextBoxSearch.Text = null;

                }
                else
                {
                    TextBoxSearch.Text = null;
                    TextBlockStatus.Text = "The selected book is unavailable in the system. Please select another book or try again later.";
                }
            }
            else
            {
                TextBoxSearch.Text = null;
                TextBlockStatus.Text = "To return a book, click 'View click 'Borrowed Books' and select a book you wish to return.";
            }
        }

        // This button click even is used to logout of the user dashboard and return to the log in page.
        private void ButtonLogout_Click(object sender, RoutedEventArgs e)
        {
            Close();

            if (mainWindow != null)
            {
                mainWindow.Show();
            }
        }
    }
}