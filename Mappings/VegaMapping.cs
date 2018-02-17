using System.Linq;
using angular_udemy_demo.Models;
using angular_udemy_demo.ViewModels;
using AutoMapper;

namespace angular_udemy_demo.Mappings
{
    public class VegaMapping: Profile
    {
        public VegaMapping()
        {
            CreateMap<Make,MakesViewModel>().ReverseMap();
            CreateMap<Model,ModelsViewModel>().ReverseMap();
            CreateMap<Feature,FeaturesViewModel>().ReverseMap();
            CreateMap<Vehicle,VehiclesViewModel>()
            .ForMember(vm => vm.Make,opt => opt.MapFrom(v => v.Model.Make))
            //.ForMember(vm => vm.Id, opt => opt.MapFrom(v => v.Id))
            .ForPath(vm => vm.Contact.Name,opt => opt.MapFrom(v => v.ContactName))
            .ForPath(vm => vm.Contact.Phone,opt => opt.MapFrom(v => v.ContactPhone))
            .ForPath(vm => vm.Contact.Email,opt => opt.MapFrom(v => v.ContactEmail))
            .ForPath(vm => vm.Features,opt => opt.MapFrom(v => v.Features))
            //.ForPath(vm => vm.Features,opt => opt.Ignore())
            .ReverseMap()
            //.ForMember(v => v.Id, opt => opt.Ignore())
            .ForMember(v => v.Features,opt => opt.Ignore())
            .AfterMap((vm,v) => {
                if (v.Features != null && vm.Features != null)
                {
                    //Remove unselected features
                    var removedFeatures = v.Features.Where(f => !vm.Features.Contains(f));
                    foreach (var rf in removedFeatures.ToList())
                        vm.Features.Remove(rf);
                    //Add newly selected features
                    var addedFeatures = vm.Features.Where(f => !v.Features.Contains(f));
                    foreach (var af in addedFeatures.ToList())
                        vm.Features.Add(af);
                }
            });

            // CreateMap<VehiclesViewModel,Vehicle>()
            // .ForPath(v => v.ContactEmail,opt => opt.MapFrom(vm => vm.Contact.Email))
            // .ForPath(v => v.ContactName,opt => opt.MapFrom(vm => vm.Contact.Name))
            // .ForPath(v => v.ContactPhone,opt => opt.MapFrom(vm => vm.Contact.Phone));            
            
        }
    }
}