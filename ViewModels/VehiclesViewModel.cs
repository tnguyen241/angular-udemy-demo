using System;
using System.Collections.Generic;
using angular_udemy_demo.Models;

namespace angular_udemy_demo.ViewModels
{
    public class VehiclesViewModel
    {
        public int Id { get; set; }
        //public int ModelId { get; set; }  
        public ModelsViewModel Model { get; set; }
        public MakesViewModel Make { get; set; }
        public bool IsRegistered { get; set; }
        public ContactsViewModel Contact { get; set; }                
        public ICollection<VehicleFeature> Features { get; set; }
    }
}