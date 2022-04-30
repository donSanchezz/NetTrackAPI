using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace NetTrackAPI.Models
{
    public class User : IdentityUser
    {

        public string FirstName { get; set; }

        public string LastName { get; set; }
        public int Day { get; set; }

        public string Month { get; set; }

        public int Year { get; set; }

        public string Body { get; set; }

        public string Skin { get; set; }

        public string Eye { get; set; }

        public List<Contact> contacts { get; set; }

    }
}
