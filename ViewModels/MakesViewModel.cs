using System.Collections.Generic;

namespace angular_udemy_demo.ViewModels
{
    public class MakesViewModel
    {
        public int Id { get; set; }      
        public string Name { get; set; }
        public virtual IEnumerable<ModelsViewModel> Models { get; set; }
    }
}