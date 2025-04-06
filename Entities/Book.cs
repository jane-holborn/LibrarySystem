using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        private string Title;
        private string Author;
        private string LibraryReferenceNumber;
        private string PublicationDate;
        private BookState State;
        private DateTime? DueDate;
        private List<User>? BorrowedBy;

        // This constructor is used to add a new book to the library.
        public Book(string title, string author, string publicationDate, string libraryReferenceNumber)
        {
            Title = title;
            Author = author;
            PublicationDate = publicationDate;
            LibraryReferenceNumber = libraryReferenceNumber;
            State = BookState.Available;
        }

        // These getters are used to facilitate access to the properties, in particular for the Data Template.
        public string AccessToTitle
        {
            get { return Title; }
        }
        public string AccessToAuthor
        {
            get { return Author; }
        }
        public string AccessToLibraryReferenceNumber
        {
            get { return LibraryReferenceNumber; }
        }
        
        // Do not remove. This property is being accessed in the UserDashboard Data Template despite the indication of 0 references.
        public string AccessToPublicationDate
        {
            get { return PublicationDate; }
        }
        public BookState AccessToAvailabilityStatus
        {
            get { return State; }
        }
        public DateTime? AccessToDueDate
        {
            get { return DueDate; }
        }
        public List<User>? AccessToBorrowedBy
        {
            get { return BorrowedBy;  }
        }

        // Setter methods used to set the state of the book and the due date.
        public void SetBookStateToAvailable()
        {
            State = BookState.Available;
        }
        public void SetBookStateToBorrowed()
        {
            State = BookState.Borrowed;
        }
        public void SetBookStateToOverdue()
        {
            State = BookState.Overdue;
        }
        public void SetBookStateToLost()
        {
            State = BookState.Lost;
        }
        public void SetDueDate()
        { 
            DueDate = DateTime.Now.AddDays(14);
        }
        public void SetBorrowedBy()
        {
            BorrowedBy = new List<User>();
        }
        public void AddUserToBorrowedBy(User user)
        {
            BorrowedBy.Add(user);
        }
        
        // This method is used to clear the due date when returning a book.
        public void ClearDueDate()
        {
            DueDate = null;
        }

        // This method is used to get the book state.
        public BookState GetBookState()
        {
            return State;
        }

        // This method is used to test and demonstrate the Overdue functionality in the application. It is not used during normal operation.
        public void SetDueDateToPast()
        {
            DueDate = DateTime.Now.AddDays(-14);
        }
    }
}