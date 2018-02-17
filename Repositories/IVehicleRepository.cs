using System.Collections.Generic;
using System.Threading.Tasks;
using angular_udemy_demo.Models;
using angular_udemy_demo.ViewModels;

namespace angular_udemy_demo.Repositories
{
    public interface IVehicleRepository
    {
        Task<IEnumerable<Make>> GetAllMakes();
        Task<IEnumerable<Model>> GetAllModels();
        Task<IEnumerable<Feature>> GetAllFeatures();
        Task<IEnumerable<Vehicle>> GetAllVehicles();
        Task<IEnumerable<Vehicle>> GetAllVehicles(FilterViewModel filter);
        Task<Vehicle> GetOneVehicle(int id);
        Task CreateVehicle(Vehicle vehicle);
        Task UpdateVehicle(Vehicle vehicle);
        Task DeleteVehicle(Vehicle vehicle);
        Task<bool> SaveChangesAsync();

    }
}