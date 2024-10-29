
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BeerInventory.Models
{
    public class Bar
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Address { get; set; }

        [JsonIgnore]
        public ICollection<Beer> Beers { get; set; } = new List<Beer>();
    }
}
