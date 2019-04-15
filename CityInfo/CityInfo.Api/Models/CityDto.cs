using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CityInfo.Api.Models
{
    public class CityDto
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }

        public int NumberOfPointsInterest => PointsOfInterest.Count;
        
        public ICollection<PointOfInterestDto> PointsOfInterest { get; set; }
            = new List<PointOfInterestDto>();
    }
}
