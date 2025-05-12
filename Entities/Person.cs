namespace LibrarySystem
{
    // This class is abstract as it will never need to be instantiated and is a base class for other derived classes.
    public abstract class Person
    {
        // Properties. Set to protected so that derived classes can still access the properties as needed.
        private string name;
        private string email;

        // This constructor is used as a base blueprint for the constructor of a derived classes.
        public Person(string personName, string personEmail)
        {
            name = personName;
            email = personEmail;
        }
        // These getters are used to facilitate access to the properties.
        public string Name
        {
            get { return name; }
        }
        public string Email
        {
            get { return email; }
        }
    }
}