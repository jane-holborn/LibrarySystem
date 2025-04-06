using LibrarySystem.Controllers;
using LibrarySystem.Entities;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LibrarySystem
{
    public partial class MainWindow : Window
    {        
        Library rockinghamLibrary = Library.GetInstance();

        public MainWindow()
        {
            InitializeComponent();
            rockinghamLibrary.GetLibrarianController().PrePopulateLibrarians();
            rockinghamLibrary.GetUserController().PrePopulateUsers();
            rockinghamLibrary.GetBookController().PrePopulateBooks();
        }

        // This method checks for a valid number of a user or librarian already in the system and redirects to the appropriate dashboard.
        private void OnEnterButtonClick(object sender, RoutedEventArgs e)
        {
            string enteredId = TextBoxHome.Text;
            TextBoxHome.Clear();

            if (string.IsNullOrEmpty(enteredId))
            {
                MessageBox.Show("Please enter a valid ID number.", "Login Unsucessful", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            else if(Library.GetInstance().GetLibrarianController().CheckLibrarianStaffId(enteredId))
            {
                Librarian loggedinLibrarian = Library.GetInstance().GetLibrarianController().GetLibrarianByStaffId(enteredId);
                Library.GetInstance().GetLibrarianController().SetCurrentLibrarian(loggedinLibrarian);
                MessageBox.Show("Login successful, press Ok to continue.", "Login Successful", MessageBoxButton.OK, MessageBoxImage.Information);
                NavigateToLibrarianDashboard();
            }
            else if (Library.GetInstance().GetUserController().CheckUserLibraryNumber(enteredId))
            {
                User loggedInUser = Library.GetInstance().GetUserController().GetUserByLibraryNumber(enteredId);
                Library.GetInstance().GetUserController().SetCurrentUser(loggedInUser);
                MessageBox.Show("Login successful, press Ok to continue.", "Login Successful", MessageBoxButton.OK, MessageBoxImage.Information);
                NavigateToUserDashboard();
            }
            else
            {
                MessageBox.Show("Login unsuccessful, press Ok to exit.", "Login Unsuccessful", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        // This method navigates to the Librarian dashboard if a Librarian Staff ID is detected.
        // It also keeps the current instance of the main windwow and hides it, to allow it to be accessed again when logging out.
        public void NavigateToLibrarianDashboard()
        {
            LibrarianDashboard librarianDashboard = new LibrarianDashboard(Library.GetInstance(), this);
            librarianDashboard.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            librarianDashboard.Show();
           
            Hide(); // https://stackoverflow.com/questions/21376552/close-window-after-open-another-window
        }

        // This method navigates to the User dashboard if a User Library number is detected.
        // It also keeps the current instance of the main windwow and hides it, to allow it to be accessed again when logging out.
        public void NavigateToUserDashboard()
        {
            UserDashboard userDashboard = new UserDashboard(Library.GetInstance(), this);
            userDashboard.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            userDashboard.Show();

            Hide(); // https://stackoverflow.com/questions/21376552/close-window-after-open-another-window
        }
    }
}