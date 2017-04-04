namespace BlueTapeCrew.Models
{
    using System.ComponentModel.DataAnnotations;

    public class GuestUser
    {
        public int Id { get; set; }

        [StringLength(24)]
        public string SessionId { get; set; }

        [StringLength(60)]
        public string FirstName { get; set; }

        [StringLength(60)]
        public string LastName { get; set; }

        public string PhoneNumber { get; set; }

        [StringLength(256)]
        public string Email { get; set; }

        [StringLength(255)]
        public string Address { get; set; }

        [StringLength(75)]
        public string City { get; set; }

        [StringLength(50)]
        public string State { get; set; }

        [StringLength(20)]
        public string PostalCode { get; set; }
    }
}
