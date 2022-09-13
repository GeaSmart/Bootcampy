using System.ComponentModel.DataAnnotations;

namespace Bootcampy.Models
{
    public class Student : BaseEntity
    {
        [Required]
        [StringLength(75)]
        public string Name { get; set; }

        [Required]
        [Range(0, 10)]
        public int Grade { get; set; }

        [EmailAddress]
        [StringLength(50)]
        public string Email { get; set; }
    }
}
