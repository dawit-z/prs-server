using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace prs_server.Models
{
    public class RequestLine
    {
        public int Id { get; set; }

        [Required]
        public int RequestId { get; set; }
        [JsonIgnore]
        public virtual Request Request { get; set; }

        [Required]
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }

        [Required]
        public int Quantity { get; set; } = 1;

        public RequestLine() { }
    }
}