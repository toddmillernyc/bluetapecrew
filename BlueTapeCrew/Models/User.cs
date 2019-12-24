namespace BlueTapeCrew.Models
{
    public class User
    {
        public string Email { get; set; }
        public string UserName { get; set; }
        public bool EmailIsConfirmed { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string  State { get; set; }
        public string PostalCode { get; set; }
    }
}