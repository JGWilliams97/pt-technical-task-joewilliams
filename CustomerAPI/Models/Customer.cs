using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace CustomerAPI.Models
{
    public class Customer
    {
        [JsonPropertyName("Id")]
        public int Id { get; set; }
        [JsonPropertyName("Name")]
        public string? Name { get; set; }
        [JsonPropertyName("Email")]
        public string? Email { get; set; }
    }
}
