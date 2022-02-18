using System.ComponentModel.DataAnnotations;

namespace prs_server.Models
{
    public class Vendor
    {
        public int Id { get; set; }

        [Required, MaxLength(30)]
        public string Code { get; set; }

        [Required, MaxLength(30)]
        public string Name { get; set; }

        [Required, MaxLength(30)]
        public string Address { get; set; }

        [Required, MaxLength(30)]
        public string City { get; set; }

        [Required, MaxLength(2)]
        public string State { get; set; }

        [Required, MaxLength(5)]
        public string Zip { get; set; }

        [MaxLength(12)]
        public string Phone { get; set; }

        [MaxLength(255)]
        public string Email { get; set; }
    }
}