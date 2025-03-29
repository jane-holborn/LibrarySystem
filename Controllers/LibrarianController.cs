using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using LibrarySystem.Entities;

namespace LibrarySystem.Controllers
{
    public class LibrarianController
    {
        private List<Librarian> ListOfAllLibrarians = new List<Librarian>();

        // This method is used to prepoluate the list of librarians when the application is launched.
        public void PrePopulateLibrarians()
        {
            ListOfAllLibrarians.Add(new Librarian("L19823713", "Jane Doe", "janedow@email.com", "U234982734"));
            ListOfAllLibrarians.Add(new Librarian("L23408323", "John Doe", "johndow@email.com", "U654983212"));
        }

        // This method is used to confirm if librarian staff Id exists in the system.
        public bool CheckLibrarianStaffId(string idNumber)
        {
            foreach (Librarian librarian in ListOfAllLibrarians)
            {
                if (idNumber == librarian.AccessLibrarianStaffId)
                {
                    return true;
                }
            }
            return false;
        }
        
        // This method is used to add fine amounts to user accounts.
        public void AddFine(User user, decimal fineAmount)
        {
            decimal Fine = user.AccessToFine; 
            Fine += fineAmount;
        }

        // This method is used to pay off fine. Can be paid off in installments.
        public bool PayFine(User user, decimal amountToPay)
        {
            decimal Fine = user.AccessToFine;
            Fine -= amountToPay;
            return (Fine == 0) ? true : false;
        }
    }
}