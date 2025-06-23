using LibrarySystem.Entities;
using System.Collections.ObjectModel;

namespace LibrarySystem.Controllers
{
    public class LibrarianController
    {
        // Properties.
        private Librarian currentLibrarian;
        private ObservableCollection<Librarian> allLibrarians = new ObservableCollection<Librarian>();

        // This method is used to confirm if librarian staff Id exists in the system.
        public bool CheckLibrarianStaffId(string staffId)
        {
            foreach (Librarian librarian in allLibrarians)
            {
                if (staffId == librarian.AccessLibrarianStaffId)
                {
                    return true;
                }
            }
            return false;
        }
        
        // This method is used to find a librarian in the list of librarians by comparing Staff IDs.
        public Librarian GetLibrarianByStaffId(string staffId)
        {
            foreach (Librarian librarian in allLibrarians)
            {
                if (staffId == librarian.AccessLibrarianStaffId)
                {
                    return librarian;
                }
            }
            return null;
        }
        
        // This method is used to set the details of the current librarian.
        public void SetCurrentLibrarian(Librarian librarian)
        {
            currentLibrarian = librarian;
        }
        
        // This method is used to get the details of the current librarian.
        public Librarian GetCurrentLibrarian()
        {
            return currentLibrarian;
        }

        // This method is used to get the list of all librarians.
        public ObservableCollection<Librarian> GetAllLibrarians()
        {
            return allLibrarians;
        }

        // This method is used to add a librarian to the list of all librarians.
        public void AddLibrarian(string staffIdNumber, string librarianName, string LibrarianEmail)
        {
            allLibrarians.Add(new Librarian(staffIdNumber, librarianName, LibrarianEmail));
        }
    }
}