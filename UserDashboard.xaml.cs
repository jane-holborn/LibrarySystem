using LibrarySystem.Controllers;
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
        // This constructor is used to create a new instance of the User dashboard.
        public UserDashboard(Library libraryInstance)
        {
            InitializeComponent();
        }

        // This method is used to carry out a search by title. It calls a method held in the BookController class.
        private void ButtonSearchByTitle_Click(object sender, RoutedEventArgs e)
        {
            string titleSearchTerm = TextBoxSearch.Text;
            List<Book> booksFilteredByTitle = Library.getInstance().getBookController().SearchBooksByTitle(titleSearchTerm);
            ListBoxBooks.ItemsSource = booksFilteredByTitle;
            TextBlockStatus.Text = "Search complete. Results are displayed above. If no books appear, none matched your search. To improve your search, try a different title, use a keyword search, or try fewer words to broaden your search.";
        }

        // This method is used to carry out a search by author. It calls a method held in the BookController class.
        private void ButtonSearchByAuthor_Click(object sender, RoutedEventArgs e)
        {
            string authorSearchTerm = TextBoxSearch.Text;
            List<Book> booksFilteredByAuthor = Library.getInstance().getBookController().SearchBooksByAuthor(authorSearchTerm);
            ListBoxBooks.ItemsSource = booksFilteredByAuthor;
            TextBlockStatus.Text = "Search complete. Results are displayed above. If no books appear, none matched your search. To improve your search, try a keyword search, or try fewer letters or words to broaden your search.";
        }

        // This method is used to carry out a search by keyword. It calls a method held in the BookController class.
        private void ButtonSearchByKeyword_Click(object sender, RoutedEventArgs e)
        {
            string keywordSearchTerm = TextBoxSearch.Text;
            List<Book> booksFilteredByKeyword = Library.getInstance().getBookController().SearchBooksByKeyword(keywordSearchTerm);
            ListBoxBooks.ItemsSource= booksFilteredByKeyword;
            TextBlockStatus.Text = "Search complete. Results are displayed above. If no books appear, none matched your search. To improve your search, try fewer characters or words to broaden your search.";
        }

        // This method is used to clear the list box contents by setting the items source back to null.
        private void ButtonClearListBox_Click(object sender, RoutedEventArgs e)
        {
            ListBoxBooks.ItemsSource = null;
            TextBlockStatus.Text = "List box successfully cleared.";
        }
    }
}