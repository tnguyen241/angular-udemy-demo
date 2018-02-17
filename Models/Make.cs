using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace angular_udemy_demo.Models
{
    public class Make
    {
        public int Id { get; set; }
        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        public virtual IEnumerable<Model> Models { get; set; }
    }
}