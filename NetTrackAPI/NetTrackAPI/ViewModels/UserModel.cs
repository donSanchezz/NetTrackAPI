using NetTrackAPI.Models;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace NetTrackAPI.ViewModels
{
    public class UserModel
    {
        [Required]
        [JsonPropertyName("fname")]
        public string FirstName { get; set; }
        [Required]

        [JsonPropertyName("lname")]
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

        [Required]
        public List<Contact> contacts { get; set; }
    }
}
