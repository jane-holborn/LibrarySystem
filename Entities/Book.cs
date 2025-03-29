using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        // This constructor is used to add a new book to the library.
        public Book(string title, string author, string publicationDate, string libraryReferenceNumber)
        {
            Title = title;
            Author = author;
            PublicationDate = publicationDate;
            LibraryReferenceNumber = libraryReferenceNumber;
            State = BookState.Available;
        }

        // These getters are used to facilitate access to the properties in particular for the Data Template.
        public string AccessToTitle
        {
            get { return Title; }
        }
        public string AccessToAuthor
        {
            get { return Author; }
        }

        // Do not remove. This property is being accessed in the LibraryDashboard Data Template despite the indication of 0 references.
        public string AccessToLibraryReferenceNumber
        {
            get { return LibraryReferenceNumber; }
        }

        // Do not remove. This property is being accessed in the UserDashboard Data Template despite the indication of 0 references.
        public string AccessToPublicationDate
        {
            get { return PublicationDate; }
        }

        // Do not remove. This property is being accessed in the UserDashboard Data Template despite the indication of 0 references.
        public BookState AccessToAvailabilityStatus
        {
            get { return State; }
        }

        // Do not remove. This property is being accessed in the LibraryDashboard Data Template despite the indication of 0 references.
        public DateTime? AccessToDueDate
        {
            get { return DueDate; }
        }
    }
}