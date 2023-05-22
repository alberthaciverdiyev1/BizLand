
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BizLand.Models
{
    public class TeamMember:Base
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Surname { get; set; }
        [Required]
        public string Profession { get; set; }
        [Required]
        public string ImageUrl { get; set; }
        public string? FacebookUrl { get; set; }
        public string? InstagramUrl { get; set; }
        public string? TwitterUrl { get; set; }
        public string? LinkdeInUrl { get; set; }
        [NotMapped]
        public IFormFile  Photo { get; set; }


    }
}
