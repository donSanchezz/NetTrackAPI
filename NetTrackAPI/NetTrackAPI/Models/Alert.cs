using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace NetTrackAPI.Models
{
    public class Alert
    {
        [JsonIgnore]
        [Key]
        public int Id { get; set; }
        public string UserId { get; set; }
    
        public double Latitude { get; set; }
        public double Longitude { get; set; }

        public bool status { get; set; }

        [JsonIgnore]
        public User User { get; set; }

        [JsonIgnore]
        public List<Image> images { get; set; }

    }
}
