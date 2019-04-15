using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CityInfo.Api.Entities
{
    public class PointOfInterest
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        [MaxLength(200)]
        public string Description { get; set; }
        [ForeignKey("{CityId")]
        public City City { get; set; }
        public int CityId { get; set; }
    }
}