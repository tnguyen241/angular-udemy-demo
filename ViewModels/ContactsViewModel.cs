using System.ComponentModel.DataAnnotations;

namespace angular_udemy_demo.ViewModels
{
    public class ContactsViewModel{
        [Required]
        [StringLength(255)]
        public string Name { get; set; }
        
        [StringLength(255)]
        public string Email { get; set; }
        [Required]
        [StringLength(255)]
        public string Phone { get; set; }
    }
}