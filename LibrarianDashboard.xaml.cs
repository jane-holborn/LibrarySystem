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
    public partial class LibrarianDashboard : Window
    {
        // This constructor is used to create a new instance of the Librarian dashboard.
        public LibrarianDashboard(Library libraryInstance)
        {
            InitializeComponent();

        }
    }
}