# Library Management System
This portfolio project is a WPF application designed to manage a library system. It includes functionality for both librarians and users.📖🐛

Functionality for librarians includes:
- add/remove users & books
- add/pay fines for users
- return a book for a user
- mark a book as lost
- basic search for books by title or author
- basic search for users by name, email or user library number
- view a list of all books in the library, including their state, the user borrowing (if on loan) & the due date
- view a list of all registered users, including their fines (if any), number of books on loan (if any) & the details of the books on loan (if any)
- view a list of all users borrowing books, including the details of the books on loan
- view a list of all borrowed books, including who has them on loan
- view a list of all overdue books, including who has them on loan
- view a list of all lost books

Functionality for users includes:

- borrow a book
- return a book
- view a list of their borrowed books
- basic search for books by title, author or keyword
  
## Technologies Used
- **C#:** for application logic
- **XAML:** for user interface layout and design
- **NUnit Testing Framework:** for unit testing individual methods and ensuring code quality
  
## Features
- **MVC & SoC (Separation of Concerns):** This was my first attempt at creating an application using the model-view-controller (MVC) design pattern to separate concerns and decouple data logic.
- **Multi-window Navigation:** Implemented using a singleton design pattern to maintain a persistent data state across multiple windows.
- **Data Templates:** Used to manage the presentation of data for both the books and users, ensuring a consistent display.
- **State Management:** Used to manage the state of the books. States include 'available', 'borrowed', 'overdue' & 'lost'.
- **Basic Authentication:** Used to verify the identity of the users logging in and retrieve their data.
- **Dynamic Updates:**  Implemented real-time updates to the views for librarians when adding/removing books & users.
  
## Stretch Goals
This application has a strong working foundation but with loads of room for growth and improvement:
- **Exit Application Button:** A simple addition to allow users to easily close the application.
- **Persistent Data:** Implement a backend database to ensure persistent data storage across sessions, even when the application is closed.
- **Improved search function:** Enhanced search functionality to allow books and authors to be found by unordered search terms.
- **Streamline Librarian Dashboard:** Improve the UI by adding a navigation menu to switch between functionality, making the current UI less crowded and more user friendly.
- **Input Validation & Management:** Add comprehensive input validation to handle character limitations, text wrapping, valid input checks. Currently the application only has empty field validation implemented.
- **Dynamic Updates:** Add dynamic updates to the user dashboard for borrowing and returning books. Add dynamic updates to the librarian dashboard when adding or paying user fines, marking books as lost and returning books.

## Testing
The tests for this project are maintained in a separate repository. If you would like to view or run the tests, they can be found in the librarysystem-testing repository here: https://github.com/jane-holborn/librarysystem-testing. Instructions for setting up and running the tests are provided in the README file.

## Author
Jane Holborn
