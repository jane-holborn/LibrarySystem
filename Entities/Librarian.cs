namespace LibrarySystem.Entities
{
    public class Librarian : Person
    {
        // Property.
        private string staffId;

        // This constructor is used to add a new librarian, properties are inherited from the abstract 'Person' class.
        public Librarian(string staffIdNumber, string personName, string personEmail)
            : base(personName, personEmail)
        {
            staffId = staffIdNumber;
        }

        // This getter is used to facilitate access to the properties.
        public string AccessLibrarianStaffId
        {
            get { return staffId; }
        }
    }
}