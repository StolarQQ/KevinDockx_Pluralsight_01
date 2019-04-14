using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CityInfo.Api.Models
{
    public class PointOfInterestUpdateDto
    {
        [Required(ErrorMessage = "You have to provide name value")]
        [MinLength(3)]
        [MaxLength(25)]
        public string Name { get; set; }
        [MaxLength(250)]
        public string Description { get; set; }
    }
}
