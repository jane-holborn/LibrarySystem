namespace LibrarySystem.Entities
{
    public class User : Person
    {
        // Property.
        private string userLibraryNumber;
        private int numberOfBorrowedBooks;
        private List<Book>? borrowedBooks;
        private double fine;

        // This constructor is used to add a new user, properties are inherited from the abstract 'Person' class.
        public User(string libraryNumber, string personName, string personEmail)
            : base(personName, personEmail)
        {
            userLibraryNumber = libraryNumber;
            fine = 0.00;
            numberOfBorrowedBooks = 0;
            borrowedBooks = new List<Book>();
        }

        // These getters are used to facilitate access to the properties.
        public string UserLibraryNumber
        {
            get { return userLibraryNumber; }
        }
        public int NumberOfBorrowedBooks
        {
            get { return numberOfBorrowedBooks; }
        }
        public List<Book> BorrowedBooks
        {
            get { return borrowedBooks; }
        }

        // Do not remove. This property is being accessed in the UserDashboard Data Template despite the indication of 0 references.
        public string FineForDatatemplate
        {
            get { return "$" + fine.ToString("F2"); }
        }
        public double Fine
        {
            get { return fine; }
        }
        public void setFine(double fineAmount)
        {
            fine = fineAmount;
        }
        public List<Book>? GetBorrowedBooks()
        {
            return borrowedBooks;
        }

        // Setter methods used to set update the users borrowed book list and the number of books on loan.
        public void IncreaseNumberOfBorrowedBooks()
        {
            numberOfBorrowedBooks++;
        }
        public void DecreaseNumberOfBorrowedBooks()
        {
            numberOfBorrowedBooks--;
        }
    }
}