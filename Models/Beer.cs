using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace BeerInventory.Models
{
    public class Beer
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        public string Name { get; set; }

        [Required]
        public Decimal PercentageAlcoholByVolume { get; set; }

        [ForeignKey("Brewery")]
        public int BreweryId { get; set; }

        [JsonIgnore]
        public Brewery? Brewery { get; set; }

        [JsonIgnore]
        public ICollection<Bar> Bars { get; set; } = new List<Bar>();
    }
}
