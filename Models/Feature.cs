using System.ComponentModel.DataAnnotations;

namespace angular_udemy_demo.Models
{
    public class Feature
    {       
        public int Id { get; set; }
        [Required]
        [StringLength(255)]
        public string Name { get; set; }

    }
}