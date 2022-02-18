using System.ComponentModel.DataAnnotations;

namespace prs_server.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required, MaxLength(30)]
        public string Username { get; set; }

        [Required, MaxLength(30)]
        public string Password { get; set; }

        [Required, MaxLength(30)]
        public string Firstname { get; set; }

        [Required, MaxLength(30)]
        public string Lastname { get; set; }

        [MaxLength(12)]
        public string Phone { get; set; }

        [MaxLength(255)]
        public string Email { get; set; }

        [Required]
        public bool IsReviewer { get; set; }

        [Required]
        public bool IsAdmin { get; set; }
    }
}