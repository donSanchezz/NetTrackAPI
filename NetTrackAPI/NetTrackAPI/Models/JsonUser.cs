using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace NetTrackAPI.Models
{
    [NotMapped]
    public class JsonUser
    {
        [Required]
        [JsonPropertyName("firstname")]
        public string FirstName { get; set; }
        [Required]

        [JsonPropertyName("lastname")]
        public string LastName { get; set; }
        [Required]

     
        [JsonPropertyName("email")]
        public string Email { get; set; }

        [Required]

        [JsonPropertyName("password")]
        public string Password { get; set; }

        [Required]
        [JsonPropertyName("phonenumber")]
        public string Phonenumber { get; set; }
        [Required]
        public string Country { get; set; }

        [Required]
        public int Day { get; set; }

        [Required]
        public string Month { get; set; }

        [Required]
        public int Year { get; set; }

        [Required]
        public string Body { get; set; }

        [Required]
        public string Skin { get; set; }

        [Required]
        public string Eye { get; set; }


        public List<Contact> contacts { get; set; }
    }
}
