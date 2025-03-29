using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystem.Entities
{
    public class Librarian : User
    {
        // Property.
        private string StaffID;

        // This constructor is used to add a new librarian, properties are inherited from the abstract 'Person' class.
        public Librarian(string staffID, string userName, string userEmail, string userLibraryNumber)
            : base(userName, userEmail, userLibraryNumber)
        {
            StaffID = staffID;
        }

        // This getter are used to facilitate access to the properties.
        public string AccessLibrarianStaffId
        {
            get { return StaffID; }
        }
    }
}