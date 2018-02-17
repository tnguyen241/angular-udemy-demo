using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using angular_udemy_demo.extensions;
using angular_udemy_demo.Models;
using angular_udemy_demo.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace angular_udemy_demo.Repositories
{
    public class VehicleRepository : IVehicleRepository
    {
        private readonly VegaDBContext context;

        public VehicleRepository(VegaDBContext context)
        {
            this.context = context;
        }
        public async Task CreateVehicle(Vehicle vehicle)
        {
            await context.Vehicles.AddAsync(vehicle);             
        }

        public async Task DeleteVehicle(Vehicle vehicle)
        {
            await Task.FromResult(context.Vehicles.Remove(vehicle));
        }

        public async Task<IEnumerable<Vehicle>> GetAllVehicles()
        {
            return await context.Vehicles
            .Include(v => v.Model)
                .ThenInclude(v => v.Make)
            .Include(v => v.Features)
            .ToListAsync();
        }
        public async Task<IEnumerable<Vehicle>> GetAllVehicles(FilterViewModel filter)
        {
            var query= context.Vehicles
            .Include(v => v.Model)
                .ThenInclude(v => v.Make)
            .Include(v => v.Features)
            .AsQueryable();

            if(filter.MakeId.HasValue)
            {
                query=query.Where(v => v.Model.MakeId==filter.MakeId);
            }
            if(filter.ModelId.HasValue)
            {
                query=query.Where(v => v.Model.Id==filter.ModelId);
            } 
            var columnMap=new Dictionary<string,Expression<Func<Vehicle,object>>>{
                ["make"] = v => v.Model.Make.Name,
                ["model"] = v => v.Model.Name,
                ["contactName"] = v => v.ContactName                
            };
            query=query.ApplySorting(filter,columnMap);           
            // if(filter.SortBy=="make"){
            //     query=(filter.IsSortAscending)? query.OrderBy(q => q.Model.Make.Name):query.OrderByDescending(q => q.Model.Make.Name);                
            // }
            //query=query.Skip((filter.Page-1)*filter.PageSize).Take(filter.PageSize);

            query=query.ApplyPaging(filter);
            return await query.ToListAsync();
        }
        
        public async Task<Vehicle> GetOneVehicle(int id)
        {
            return await context.Vehicles                      
            .Include(v => v.Features)
            .Include(v => v.Model)
            .ThenInclude(m=>m.Make) 
            .FirstOrDefaultAsync(v=>v.Id==id);
        }

        public async Task UpdateVehicle(Vehicle vehicle)
        {
            await Task.FromResult(context.Vehicles.Update(vehicle));            
        }
        public async Task<bool> SaveChangesAsync()
        {
            var status= await context.SaveChangesAsync();
            return status > 0;
        }

        public async Task<IEnumerable<Make>> GetAllMakes()
        {
            return await context.Makes.Include(m=>m.Models).ToListAsync();
        }

          public async Task<IEnumerable<Model>> GetAllModels()
        {
            return await context.Models.ToListAsync();
        }

        public async Task<IEnumerable<Feature>> GetAllFeatures()
        {
            return await context.Features.ToListAsync();
        }
    }
}
