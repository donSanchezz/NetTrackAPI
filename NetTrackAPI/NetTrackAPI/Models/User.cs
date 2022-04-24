using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace NetTrackAPI.Models
{
    public class User : IdentityUser
    {
        [Required]
        [JsonPropertyName("firstname")]
        public string FirstName { get; set; }
        [Required]

        [JsonPropertyName("lastname")]
        public string LastName { get; set; }
        [Required]

        [NotMapped]
        [JsonPropertyName("email")]
        public string Email { get; set; }
        
        [Required]
        [NotMapped]
        [JsonPropertyName("password")]
        public string Password { get; set; }

        [NotMapped]
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

        //public virtual TKey Id
        //{
        //    get;
        //    set;
        //}

        [JsonIgnore]
        [ProtectedPersonalData]
        public virtual string UserName
        {
            get;
            set;
        }

        [JsonIgnore]
        public virtual string NormalizedUserName
        {
            get;
            set;
        }

        //[JsonIgnore]
        //public virtual string Email
        //{
        //    get;
        //    set;
        //}

        [JsonIgnore]
        public virtual string NormalizedEmail
        {
            get;
            set;
        }

        [JsonIgnore]
        [PersonalData]
        public virtual bool EmailConfirmed
        {
            get;
            set;
        }

        [JsonIgnore]
        public virtual string PasswordHash
        {
            get;
            set;
        }

        [JsonIgnore]
        string SecurityStamp
        {
            get;
            set;
        }

        [JsonIgnore]
        public virtual string ConcurrencyStamp
        {
            get;
            set;
        } = Guid.NewGuid().ToString();


        [JsonIgnore]
        [ProtectedPersonalData]
        public virtual string PhoneNumber
        {
            get;
            set;
        }

        [JsonIgnore]
        [PersonalData]
        public virtual bool PhoneNumberConfirmed
        {
            get;
            set;
        }

        [JsonIgnore]
        [PersonalData]
        public virtual bool TwoFactorEnabled
        {
            get;
            set;
        }

        [JsonIgnore]
        public virtual DateTimeOffset? LockoutEnd
        {
            get;
            set;
        }

        [JsonIgnore]
        public virtual bool LockoutEnabled
        {
            get;
            set;
        }

        [JsonIgnore]
        public virtual int AccessFailedCount
        {
            get;
            set;
        }


        [Required]
        public List<Contact> contacts { get; set; }

    }
}
