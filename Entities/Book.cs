using LibrarySystem.Entities;

namespace LibrarySystem
{
    public class Book
    {
        // This enum is used for the state management for each book instance.
        public enum BookState
        {
            Available,
            Borrowed,
            Overdue,
            Lost
        }

        // Properties.
        private string title;
        private string author;
        private string libraryReferenceNumber;
        private string publicationDate;
        private BookState state;
        private DateTime? dueDate;
        private List<User>? borrowedBy;

        // This constructor is used to add a new book to the library.
        public Book(string bookTitle, string bookAuthor, string publishDate, string referenceNumber)
        {
            title = bookTitle;
            author = bookAuthor;
            publicationDate = publishDate;
            libraryReferenceNumber = referenceNumber;
            state = BookState.Available;
        }

        // These getters are used to facilitate access to the properties, in particular for the Data Template.
        public string AccessToTitle
        {
            get { return title; }
        }
        public string AccessToAuthor
        {
            get { return author; }
        }
        public string AccessToLibraryReferenceNumber
        {
            get { return libraryReferenceNumber; }
        }
        
        // Do not remove. This property is being accessed in the UserDashboard Data Template despite the indication of 0 references.
        public string AccessToPublicationDate
        {
            get { return publicationDate; }
        }
        public BookState AccessToAvailabilityStatus
        {
            get { return state; }
        }
        public DateTime? AccessToDueDate
        {
            get { return dueDate; }
        }
        public List<User>? AccessToBorrowedBy
        {
            get { return borrowedBy;  }
        }

        // Setter methods used to set the state of the book and the due date.
        public void SetBookStateToAvailable()
        {
            state = BookState.Available;
        }
        public void SetBookStateToBorrowed()
        {
            state = BookState.Borrowed;
        }
        public void SetBookStateToOverdue()
        {
            state = BookState.Overdue;
        }
        public void SetBookStateToLost()
        {
            state = BookState.Lost;
        }
        public void SetDueDate()
        { 
            dueDate = DateTime.Now.AddDays(14);
        }
        public void SetBorrowedBy()
        {
            borrowedBy = new List<User>();
        }

        // Methods used to add and remove users from the borrowedBy list.
        public void AddUserToBorrowedBy(User user)
        {
            borrowedBy.Add(user);
        }
        public void RemoveUserFromBorrowedBy(User user)
        {
            borrowedBy.Remove(user);
        }

        // This method is used to clear the due date when returning a book.
        public void ClearDueDate()
        {
            dueDate = null;
        }

        // This method is used to get the book state.
        public BookState GetBookState()
        {
            return state;
        }

        // This method is used to test and demonstrate the Overdue functionality in the application. It is not used during normal operation.
        public void SetDueDateToPast()
        {
            dueDate = DateTime.Now.AddDays(-14);
        }
    }
}