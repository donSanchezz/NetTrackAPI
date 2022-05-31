using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace NetTrackAPI.Models
{
    public class Contact
    {

        public int Id { get; set; }

        [JsonPropertyName("firstname")]
        public string FirstName { get; set; }

        [JsonPropertyName("lastname")]
        public string LastName { get; set; }

        [JsonPropertyName("email")]
        public string Email { get; set; }

        [JsonPropertyName("phone")]
        public string Phone { get; set; }

        [JsonPropertyName("message")]
        public string Message { get; set; }

        [JsonPropertyName("primary")]
        public bool Primary { get; set; }

        
        public string UserId { get; set; }

        [JsonIgnore]
        public User User { get; set; }

        [NotMapped]
        public bool Active { get; set; }
        
        [NotMapped]
        public List<string> images { get; set; } = new List<string>();
     }
}
