using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace NetTrackAPI.Models
{
    public class Image
    {
        [JsonIgnore]
        [Key]
        public int Id { get; set; }

        [JsonIgnore]
        public int AlertId { get; set; }

        [JsonIgnore]
        public string Name { get; set; }


    }
}
