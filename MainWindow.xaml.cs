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

        Library rockinghamLibrary = Library.getInstance();

        public MainWindow()
        {
            InitializeComponent();
            rockinghamLibrary.getLibrarianController().PrePopulateLibrarians();
            rockinghamLibrary.getUserController().PrePopulateUsers();
            rockinghamLibrary.getBookController().PrePopulateBooks();
        }

        // This method checks for a valid number of a user or librarian already in the system and redirects to the appropriate dashboard.
        private void OnEnterButtonClick(object sender, RoutedEventArgs e)
        {
            string enteredId = TextBoxHome.Text;

            if (string.IsNullOrEmpty(enteredId))
            {
                MessageBox.Show("Please enter a valid ID number.", "Login Unsucessful", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            else if(Library.getInstance().getLibrarianController().CheckLibrarianStaffId(enteredId))
            {
                MessageBox.Show("Login successful, press Ok to continue.", "Login Successful", MessageBoxButton.OK, MessageBoxImage.Information);
                NavigateToLibrarianDashboard();
            }
            else if (Library.getInstance().getUserController().CheckUserLibraryNumber(enteredId))
            {
                MessageBox.Show("Login successful, press Ok to continue.", "Login Successful", MessageBoxButton.OK, MessageBoxImage.Information);
                NavigateToUserDashboard();
            }
            else
            {
                MessageBox.Show("Login unsuccessful, press Ok to exit.", "Login Unsuccessful", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        // This method navigates to the Librarian dashboard if as Librarian Staff ID is detected
        public void NavigateToLibrarianDashboard()
        {
            LibrarianDashboard librarianDashboard = new LibrarianDashboard(Library.getInstance());
            librarianDashboard.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            librarianDashboard.Show();
           
            this.Hide(); // https://stackoverflow.com/questions/21376552/close-window-after-open-another-window ask Blake about using the 'this' keyword here.
        }

        // This method navigates to the User dashboard if as User Library number is detected
        public void NavigateToUserDashboard()
        {
            UserDashboard userDashboard = new UserDashboard(Library.getInstance());
            userDashboard.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            userDashboard.Show();

            this.Hide(); // https://stackoverflow.com/questions/21376552/close-window-after-open-another-window ask Blake about using the 'this' keyword here.
        }
    }
}