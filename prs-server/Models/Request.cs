using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace prs_server.Models
{
    public class Request
    {
        public int Id { get; set; }

        [Required, MaxLength(80)]
        public string Description { get; set; }

        [Required, MaxLength(80)]
        public string Justification { get; set; }

        [MaxLength(80)]
        public string RejectionReason { get; set; }

        [Required, MaxLength(20)]
        public string DeliveryMode { get; set; } = "Pickup";

        [Required, MaxLength(10),]
        public string Status { get; set; } = "NEW";

        [Required, Column(TypeName = "decimal(11,2)")]
        public decimal Total { get; set; } = 0;

        public int UserId { get; set; }
        public virtual User User { get; set; }

        public List<RequestLine> RequestLines = new();
    }
}