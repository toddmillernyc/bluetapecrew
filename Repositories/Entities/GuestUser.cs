using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Repositories.Entities
{
    [Table("GuestUsers")]
    public class GuestUser
    {
        public int Id { get; set; }

        [MaxLength(24)]
        public string SessionId { get; set; }
        
        [MaxLength(60)]
        public string FirstName { get; set; }

        [MaxLength(60)]
        public string LastName { get; set; }

        public string PhoneNumber { get; set; }

        [MaxLength(256)]
        public string Email { get; set; }

        [MaxLength(255)]
        public string Address { get; set; }

        [MaxLength(75)]
        public string City { get; set; }

        [MaxLength(50)]
        public string State { get; set; }

        [MaxLength(20)]
        public string PostalCode { get; set; }
    }
}