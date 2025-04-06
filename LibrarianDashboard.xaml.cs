using System;
using System.Collections.Generic;
using System.Configuration;
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
using LibrarySystem.Entities;
using LibrarySystem;

namespace LibrarySystem
{
    public partial class LibrarianDashboard : Window
    {
        private MainWindow mainWindow;

        // This constructor is used to create a new instance of the Librarian dashboard.
        public LibrarianDashboard(Library libraryInstance, MainWindow mainWindowInstance)
        {
            InitializeComponent();
            mainWindow = mainWindowInstance;
        }

        // This button click event is used to logout of the user dashboard and return to the log in page.
        private void ButtonLogout_Click(object sender, RoutedEventArgs e)
        {
            Close();

            if (mainWindow != null)
            {
                mainWindow.Show();
            }
        }

        // This button click event is used to return the list of all books in the library system.
        private void ButtonAllBooks_Click(object sender, RoutedEventArgs e)
        {
            List<Book> listOfAllBooks = Library.GetInstance().GetBookController().GetListOfAllBooks();
            if (listOfAllBooks.Count > 0)
            {
                ListBoxBooks.ItemsSource = listOfAllBooks;
            }
        }

        // This button click event is used to return a list of all borrowed books. It checks the book state before returning to look for any overdue books.
        private void ButtonAllBorrowedBooks_Click(object sender, RoutedEventArgs e)
        {
            List<Book> listOfBorrowedBooks = Library.GetInstance().GetBookController().GetListOfAllBorrowedBooks();
            ListBoxBooks.ItemsSource = listOfBorrowedBooks;
            TextBlockStatus.Text = "List of borrowed books are displayed to the right. If no books appear, there are no books on loan.";
        }

        // This button click event is used to return a list of all overdue books. It checks the book state before returning to look for any additional overdue books.
        private void ButtonAllOverdueBooks_Click(object sender, RoutedEventArgs e)
        {
            List<Book> listOfOverdueBooks = Library.GetInstance().GetBookController().GetListOfAllOverdueBooks();
            ListBoxBooks.ItemsSource = listOfOverdueBooks;
            TextBlockStatus.Text = "List of overdue books are displayed to the right. If no books appear, there are none overdue.";
        }

        // This button click event is used to return a list of all lost books. 
        private void ButtonAllLostBooks_Click(object sender, RoutedEventArgs e)
        {
            List<Book> listOfLostBooks = Library.GetInstance().GetBookController().GetlistOfAllLostBooks();
            ListBoxBooks.ItemsSource= listOfLostBooks;
            TextBlockStatus.Text = "List of lost books are displayed to the right. If no books appear, there are no lost books.";
        }
        // This button click event is used to return a list of all registered users.
        private void ButtonAllUsers_Click(object sender, RoutedEventArgs e)
        {
            int numberOfOverdueBooks = 0;
            List<User> listOfAllUsers = Library.GetInstance().GetUserController().GetListOfAllUsers();
            foreach (User user in listOfAllUsers)
            {
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
                }
            }

            if (listOfAllUsers.Count > 0)
            {
                ListBoxUsers.ItemsSource = listOfAllUsers;
            }
        }

        // This button click event is used to display a list of all users with borrowed books in the User listbox.
        private void ButtonAllUserBorrowingBooks_Click(object sender, RoutedEventArgs e)
        {
            List<User> listOfAllusersWithBorrowedBooks = Library.GetInstance().GetUserController().GetListOfAllUsersWithBorrowedBooks();
            ListBoxUsers.ItemsSource= listOfAllusersWithBorrowedBooks;
        }

        // This method is used to find books in the library system.
        private void ButtonSearchBook_Click(object sender, RoutedEventArgs e)
        {
            Library.GetInstance().GetBookController().CheckForOverdueBooks();
            string keywordSearchTerm = TextBoxSearchBooks.Text;
            List<Book> booksFilteredByKeyword = Library.GetInstance().GetBookController().SearchBooksByKeyword(keywordSearchTerm);
            ListBoxBooks.ItemsSource = booksFilteredByKeyword;
            TextBlockStatus.Text = "Search complete. Results are displayed above. If no books appear, none matched your search. To improve your search, try fewer characters or words to broaden your search.";
            TextBoxSearchBooks.Clear();
        }

        // This method allows the librarian to mark a book as lost. It also updates the associated user's book list.
        private void ButtonMarkBookAsLost_Click(object sender, RoutedEventArgs e)
        {
            if (ListBoxBooks.SelectedItem != null)
            {
                Book selectedBookFromSearchResults = (Book)ListBoxBooks.SelectedItem;
                Book bookInLibrarySystem = Library.GetInstance().GetBookController().FindBookInAllBooks(selectedBookFromSearchResults.AccessToLibraryReferenceNumber);

                // And if the book exists in the library system.
                if (bookInLibrarySystem != null)
                {
                    // If the book is in a users borrowed book list, will need to remove from that list.
                    bookInLibrarySystem.SetBookStateToLost();
                    TextBlockStatus.Text = "The selected book has been set to lost in the system.";
                }
                else
                {
                    TextBlockStatus.Text = "The selected book is unavailable in the system. Please select another book or try again later.";
                }
            }
            else
            {
                TextBlockStatus.Text = "To set a books status to lost, please search for the book first, and select it from the search results.";
            }
        }

        // This method allows the librarian to mark a book as available. It also updates the associated user's book list.
        private void ButtonMarkBookAsAvailable_Click(object sender, RoutedEventArgs e)
        {
            if (ListBoxBooks.SelectedItem != null)
            {
                Book selectedBookFromSearchResults = (Book)ListBoxBooks.SelectedItem;
                Book bookInLibrarySystem = Library.GetInstance().GetBookController().FindBookInAllBooks(selectedBookFromSearchResults.AccessToLibraryReferenceNumber);

                // And if the book exists in the library system.
                if (bookInLibrarySystem != null)
                {
                    // If the book is in a users borrowed book list, will need to remove from that list.
                    bookInLibrarySystem.SetBookStateToAvailable();
                    TextBlockStatus.Text = "The selected book has been set to back to available in the system.";
                }
                else
                {
                    TextBlockStatus.Text = "The selected book is unavailable in the system. Please select another book or try again later.";
                }
            }
            else
            {
                TextBlockStatus.Text = "To set a books status to available, please search for the book first, and select it from the search results.";
            }
        }

        // This method adds a book to the library system.
        private void ButtonAddBook_Click(object sender, RoutedEventArgs e)
        {
            string title = TextBoxTitle.Text;
            string author = TextBoxAuthor.Text;
            string publicationDate = TextBoxPublicationDate.Text;
            string libraryReferenceNumber = TextBoxLibraryReferenceNumber.Text;

            if (title != string.Empty && author != string.Empty && publicationDate != string.Empty && libraryReferenceNumber != string.Empty)
            {
                Library.GetInstance().GetBookController().AddBook(title, author, publicationDate, libraryReferenceNumber);
                TextBlockStatus.Text = "The book has been successfully added";
                TextBoxTitle.Clear();
                TextBoxAuthor.Clear();
                TextBoxPublicationDate.Clear();
                TextBoxLibraryReferenceNumber.Clear();
                ListBoxBooks.ItemsSource = null;
            }
            else
            {
                TextBlockStatus.Text = "One or more fields are empty, please ensure you provide a title, author, publication date and " +
                    "library reference number when adding a book. The library reference number should start with LRN and have 10 digits.";
            }
        }

        // This method deletes a book from the library system, it has additional confirmation checks.
        private void ButtonRemoveBook_Click(object sender, RoutedEventArgs e)
        {
            if (ListBoxBooks.SelectedItem != null)
            {
                Book selectedBookFromSearchResults = (Book)ListBoxBooks.SelectedItem;
                Book bookInLibrarySystem = Library.GetInstance().GetBookController().FindBookInAllBooks(selectedBookFromSearchResults.AccessToLibraryReferenceNumber);

                // And if the book exists in the library system.
                if (bookInLibrarySystem != null)
                {
                    MessageBoxResult result = MessageBox.Show("This action will delete the selected book from the Library system. This action cannot be undone, do you wish to proceed? Click 'Yes' to delete or 'No' to cancel.", "Warning", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                    if (result == MessageBoxResult.Yes)
                    {
                        Library.GetInstance().GetBookController().DeleteBook(bookInLibrarySystem);
                        TextBlockStatus.Text = "The selected book has been permanently deleted from the system.";
                    }
                    else
                    {
                        TextBlockStatus.Text = "This book has not been deleted. ";
                    }
                }
                else
                {
                    TextBlockStatus.Text = "The selected book is unavailable in the system. Please select another book or try again later.";
                }
            }
            else
            {
                TextBlockStatus.Text = "To remove a book from the library system, please search for the book first, and select it from the search results.";
            }
        }

        // This method is used to find a user in the library system.
        private void ButtonSearchUser_Click(object sender, RoutedEventArgs e)
        {
            string keywordSearchTerm = TextBoxSearchUsers.Text;
            List<User> usersFilteredByKeyword = Library.GetInstance().GetUserController().SearchUsersByKeyword(keywordSearchTerm);
            ListBoxUsers.ItemsSource = usersFilteredByKeyword;
            TextBlockStatus.Text = "Search complete. Results are displayed above. If no users appear, none matched your search. To improve your search, try fewer characters or words to broaden your search.";
            TextBoxSearchUsers.Clear();
        }

        // This method adds a user to the library system.
        private void ButtonAddUser_Click(object sender, RoutedEventArgs e)
        {
            string userLibraryNumber = TextBoxUserLibraryNumber.Text;
            string userName = TextBoxUserName.Text;
            string userEmail = TextBoxUserEmail.Text;

            if (userLibraryNumber != string.Empty && userName != string.Empty && userEmail != string.Empty)
            {
                Library.GetInstance().GetUserController().AddUser(userLibraryNumber, userName, userEmail);
                TextBlockStatus.Text = "The user has been successfully added";
                TextBoxUserLibraryNumber.Clear();
                TextBoxUserName.Clear();
                TextBoxUserEmail.Clear();
                ListBoxUsers.ItemsSource = null;
            }
            else
            {
                TextBlockStatus.Text = "One or more fields are empty, please ensure you provide a user library number, user name and " +
                    "user email when adding a user. The user library number should start with U and have 9 digits.";
            }
        }

        // This method deletes a user from the library system, it has additional confirmation checks.
        private void ButtonRemoveUser_Click(object sender, RoutedEventArgs e)
        {
            if (ListBoxUsers.SelectedItem != null)
            {
                User selectedUserFromSearchResults = (User)ListBoxUsers.SelectedItem;
                User userInLibrarySystem = Library.GetInstance().GetUserController().FindUserInAllUsers(selectedUserFromSearchResults.AccessToUserLibraryNumber);

                // And if the book exists in the library system.
                if (userInLibrarySystem != null)
                {
                    MessageBoxResult result = MessageBox.Show("This action will delete the selected user from the Library system. This action cannot be undone, do you wish to proceed? Click 'Yes' to delete or 'No' to cancel.", "Warning", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                    if (result == MessageBoxResult.Yes)
                    {
                        Library.GetInstance().GetUserController().DeleteUser(userInLibrarySystem);
                        TextBlockStatus.Text = "The selected user has been permanently deleted from the system.";
                    }
                    else
                    {
                        TextBlockStatus.Text = "This book has not been deleted. ";
                    }
                }
                else
                {
                    TextBlockStatus.Text = "The selected user is unavailable in the system. Please select another user or try again later.";
                }
            }
            else
            {
                TextBlockStatus.Text = "To remove a user from the library system, please search for the user first, and select them from the search results.";
            }
        }

        // This method is used to add a fine manually to users.
        private void ButtonAddFine_Click(object sender, RoutedEventArgs e)
        {
            if (ListBoxUsers.SelectedItem != null && TextBoxAddFine.Text != null)
            {
                string convertedFineAmount = TextBoxAddFine.Text;
                if (!double.TryParse(convertedFineAmount, out double fineAmount))
                {
                    TextBlockStatus.Text = "You did not enter a valid number for the fine amount. Please try again.";
                    TextBoxAddFine.Clear();
                }
                else
                {
                    User selectedUserFromSearchResults = (User)ListBoxUsers.SelectedItem;
                    User userInLibrarySystem = Library.GetInstance().GetUserController().FindUserInAllUsers(selectedUserFromSearchResults.AccessToUserLibraryNumber);

                    if (userInLibrarySystem != null)
                    {
                        Library.GetInstance().AddFine(userInLibrarySystem, fineAmount);
                        TextBlockStatus.Text = $"The amount of ${fineAmount} has been added to {userInLibrarySystem.AccessToUserName}.";
                        TextBoxAddFine.Clear();
                        ListBoxUsers.ItemsSource = null;
                    }
                    else
                    {
                        TextBlockStatus.Text = "The selected user is unavailable in the system. Please select another user or try again later.";
                    }
                }
            }
            else
            {
                TextBlockStatus.Text = "To add a fine to a user, please search for the user first, and select them from the search results. And ensure you have added an amount for the fine.";
            }
        }

        // This method is used to pay a fine on a users library account.
        private void ButtonPayFine_Click(object sender, RoutedEventArgs e)
        {
            if (ListBoxUsers.SelectedItem != null && TextBoxAddFine.Text != null)
            {
                string convertedAmountToPay = TextBoxPayFine.Text;
                if (!double.TryParse(convertedAmountToPay, out double amountToPay))
                {
                    TextBlockStatus.Text = "You did not enter a valid number for the number for the amount to pay. Please try again.";
                    TextBoxPayFine.Clear();
                }
                else
                {
                    User selectedUserFromSearchResults = (User)ListBoxUsers.SelectedItem;
                    User userInLibrarySystem = Library.GetInstance().GetUserController().FindUserInAllUsers(selectedUserFromSearchResults.AccessToUserLibraryNumber);

                    if (userInLibrarySystem != null)
                    {
                        bool result = Library.GetInstance().PayFine(userInLibrarySystem, amountToPay);
                        if (result)
                        {
                            TextBlockStatus.Text = $"The amount of ${amountToPay} has been paid. The fines for {userInLibrarySystem.AccessToUserName} have been paid in full. ";
                            TextBoxPayFine.Clear();
                            ListBoxUsers.ItemsSource = null;
                        }
                        else
                        {
                            TextBlockStatus.Text = $"The amount of ${amountToPay} has been paid off the fines for {userInLibrarySystem.AccessToUserName}. There are still outstandings fines for this account. ";
                            TextBoxPayFine.Clear();
                            ListBoxUsers.ItemsSource = null;
                        }
                            
                    }
                    else
                    {
                        TextBlockStatus.Text = "The selected user is unavailable in the system. Please select another user or try again later.";
                    }
                }
            }
            else
            {
                TextBlockStatus.Text = "To pay a fine to a user, please search for the user first, and select them from the search results. And ensure you have added an amount to pay.";
            }
        }

        // This method is used to clear the libraian book list box.
        private void ButtonClearBookListBox_Click(object sender, RoutedEventArgs e)
        {
            ListBoxBooks.ItemsSource = null;
            TextBoxSearchBooks.Clear();
        }

        // This method is used to clear the libraian user list box.
        private void ButtonClearUserListBox_Click(object sender, RoutedEventArgs e)
        {
            ListBoxUsers.ItemsSource= null;
            TextBoxSearchUsers.Clear();
        }
    }
}