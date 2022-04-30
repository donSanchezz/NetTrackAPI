using NetTrackAPI.Models;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;


namespace NetTrackAPI.ViewModels
{
    public class LoginModel
    {


        [JsonPropertyName("email")]
        public string Email { get; set; }

        [JsonPropertyName("password")]
        public string Password { get; set; }
    }


}

