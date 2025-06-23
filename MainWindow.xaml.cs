using LibrarySystem.Entities;
using System.Windows;

namespace LibrarySystem
{
    public partial class MainWindow : Window
    {        
        Library rockinghamLibrary = Library.GetInstance();

        public MainWindow()
        {
            InitializeComponent();
            PopulateLibrarySystemData();
        }

        // This method checks for a valid number of a user or librarian already in the system and redirects to the appropriate dashboard.
        private void OnEnterButtonClick(object sender, RoutedEventArgs e)
        {
            string enteredId = TextBoxHome.Text;
            TextBoxHome.Clear();

            if (string.IsNullOrEmpty(enteredId))
            {
                MessageBox.Show("Please enter a valid user ID or staff ID.", "Login Unsucessful", MessageBoxButton.OK, MessageBoxImage.Warning);
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
                TextBoxHome.Clear();
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

        // This method populates the library system with some initial data for users, books, and librarians.
        // Eventually this will be replaced with a database connection to retrieve data.
        public void PopulateLibrarySystemData()
        {
            // Prepopulate the allUsers list with some users.
            rockinghamLibrary.GetUserController().AddUser("U804736591", "Charlie Evans", "charlie@email.com");
            rockinghamLibrary.GetUserController().AddUser("U295684130", "James Doe", "jamesdoe@email.com");
            rockinghamLibrary.GetUserController().AddUser("U612905738", "Alice Johnson", "alicejohnson@email.com");
            rockinghamLibrary.GetUserController().AddUser("U731259846", "Bob Smith", "bobsmith@email.com");
            rockinghamLibrary.GetUserController().AddUser("U854970312", "Diana Lee", "dianalee@email.com");
            rockinghamLibrary.GetUserController().AddUser("U417263890", "Ethan Brown", "ethanbrown@email.com");

            // Prepopulate the allBooks list with some books.
            rockinghamLibrary.GetBookController().AddBook("Harry Potter and the Philosopher's Stone", "J.K.Rowling", "1997", "LRN2653675141");
            rockinghamLibrary.GetBookController().AddBook("Harry Potter and the Chamber of Secrets", "J.K.Rowling", "1998", "LRN1948375620");
            rockinghamLibrary.GetBookController().AddBook("Harry Potter and the Prisoner of Azkaban", "J.K.Rowling", "1999", "LRN2988245676");
            rockinghamLibrary.GetBookController().AddBook("Harry Potter and the Goblet of Fire", "J.K.Rowling", "2000", "LRN4827361950");
            rockinghamLibrary.GetBookController().AddBook("Harry Potter and the Order of the Phoenix", "J.K.Rowling", "2003", "LRN9273846102");
            rockinghamLibrary.GetBookController().AddBook("Harry Potter and the Half-Blood Prince", "J.K.Rowling", "2005", "LRN4927835610");
            rockinghamLibrary.GetBookController().AddBook("Harry Potter and the Deathly Hallows", "J.K.Rowling", "2007", "LRN6273846102");
            rockinghamLibrary.GetBookController().AddBook("Catcher in the Rye", "J.D. Salinger", "1951", "LRN7543443912");
            rockinghamLibrary.GetBookController().AddBook("The Hunger Games", "Suzanne Collins", "2008", "LRN4335678217");
            rockinghamLibrary.GetBookController().AddBook("CatchingFire", "Suzanne Collins", "2009", "LRN8354976210");
            rockinghamLibrary.GetBookController().AddBook("Mockingjay", "Suzanne Collins", "2010", "LRN6728401953");
            rockinghamLibrary.GetBookController().AddBook("Wool", "Hugh Howey", "2011", "LRN1364521907");
            rockinghamLibrary.GetBookController().AddBook("Shift", "Hugh Howey", "2013", "LRN8492037482");
            rockinghamLibrary.GetBookController().AddBook("Dust", "Hugh Howey", "2013", "LRN5079128436");
            rockinghamLibrary.GetBookController().AddBook("One Flew Over the Cuckoo's Nest", "Ken Kesey", "1962", "LRN3569871204");

            // Prepopulate the allLibrarians list with some librarians.
            rockinghamLibrary.GetLibrarianController().AddLibrarian("L93482617", "Jane Doe", "janedow@email.com");
            rockinghamLibrary.GetLibrarianController().AddLibrarian("L56128394", "John Doe", "johndow@email.com");
            rockinghamLibrary.GetLibrarianController().AddLibrarian("L74290568", "Michael Anderson", "michaelanderson@email.com");
            rockinghamLibrary.GetLibrarianController().AddLibrarian("L38275149", "Olivia Taylor", "oliviataylor@email.com");
            rockinghamLibrary.GetLibrarianController().AddLibrarian("L89562310", "Sophia Harrison", "sophiaharrison@email.com");
            rockinghamLibrary.GetLibrarianController().AddLibrarian("L20458736", "James Richardson", "jamesrichardson@email.com");
        }
    }
}