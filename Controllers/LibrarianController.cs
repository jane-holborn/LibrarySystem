using LibrarySystem.Entities;

namespace LibrarySystem.Controllers
{
    public class LibrarianController
    {
        // Properties.
        private Librarian currentLibrarian;
        private List<Librarian> allLibrarians = new List<Librarian>();

        // This method is used to prepoluate the list of librarians when the application is launched.
        public void PrePopulateLibrarians()
        {
            allLibrarians.Add(new Librarian("L93482617", "Jane Doe", "janedow@email.com"));
            allLibrarians.Add(new Librarian("L56128394", "John Doe", "johndow@email.com"));
            allLibrarians.Add(new Librarian("L74290568", "Michael Anderson", "michaelanderson@email.com"));
            allLibrarians.Add(new Librarian("L38275149", "Olivia Taylor", "oliviataylor@email.com"));
            allLibrarians.Add(new Librarian("L89562310", "Sophia Harrison", "sophiaharrison@email.com"));
            allLibrarians.Add(new Librarian("L20458736", "James Richardson", "jamesrichardson@email.com"));
        }

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
    }
}