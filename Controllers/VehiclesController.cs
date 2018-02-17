using angular_udemy_demo.Models;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using AutoMapper;
using angular_udemy_demo.ViewModels;
using System;
using Microsoft.Extensions.Logging;
using angular_udemy_demo.Repositories;

namespace angular_udemy_demo.Controllers
{       
    public class VehiclesController:Controller
    {   
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly ILogger<VehiclesController> logger;
        private readonly IVehicleRepository vehicleRepository;

        public VehiclesController(IUnitOfWork unitOfWork,IMapper mapper,ILogger<VehiclesController> logger, IVehicleRepository vehicleRepository)
        {           
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.logger = logger;
            this.vehicleRepository = vehicleRepository;
        }

        [HttpGet("api/makes")]
        public async Task<IEnumerable<MakesViewModel>> GetMakes()
        {
            logger.LogInformation("api/makes");
            var result = await vehicleRepository.GetAllMakes();
             var obj=mapper.Map<IEnumerable<Make>,IEnumerable<MakesViewModel>>(result);
             return obj;
        }
         [HttpGet("api/models")]
        public async Task<IEnumerable<ModelsViewModel>> GetModels()
        {
            logger.LogInformation("api/models");
            var result = await vehicleRepository.GetAllModels();
             var obj=mapper.Map<IEnumerable<Model>,IEnumerable<ModelsViewModel>>(result);
             return obj;
        }

        [HttpGet("api/features")]        
        public async Task<IEnumerable<FeaturesViewModel>> GetFeatures(){
            var result = await vehicleRepository.GetAllFeatures();
            return mapper.Map<IEnumerable<Feature>,IEnumerable<FeaturesViewModel>>(result);

        }
        // [HttpGet("api/vehicles")]
        // public async Task<IActionResult> GetAllVehicles()
        // {
        //     var vehicle= await vehicleRepository.GetAllVehicles();
        //     return Ok(mapper.Map<IEnumerable<Vehicle>, IEnumerable<VehiclesViewModel>>(vehicle));
        // }
         [HttpGet("api/vehicles")]
        public async Task<IActionResult> GetAllVehicles(FilterViewModel filter)
        {
            var vehicle= await vehicleRepository.GetAllVehicles(filter);
            return Ok(mapper.Map<IEnumerable<Vehicle>, IEnumerable<VehiclesViewModel>>(vehicle));
        }

        [HttpGet("api/vehicles/{id}")]
        public async Task<IActionResult> GetOneVehicle(int id)
        {
            var vehicle= await vehicleRepository.GetOneVehicle(id);
            return Ok(mapper.Map<Vehicle, VehiclesViewModel>(vehicle));
        }

        [HttpPost("api/vehicles")]
        public async Task<IActionResult> CreateVehicle([FromBody]VehiclesViewModel vehicle)
        {
            //throw new Exception();
            if(!ModelState.IsValid) 
                return BadRequest(vehicle);
            //var model=await vehicleRepository.GetOneVehicle(vehicle.ModelId);
            //if(model==null){
            //    ModelState.AddModelError("ModelId","Invalid Modelid");
            //    return BadRequest(ModelState);
            //}
            var obj=mapper.Map<Vehicle>(vehicle);
            obj.LastUpdate=DateTime.Now;
            await vehicleRepository.CreateVehicle(obj);
            await unitOfWork.Commit();
            var vm=mapper.Map<VehiclesViewModel>(obj);
            return Ok(vm);
        }
        [HttpPut("api/vehicles/{id}")]
        public async Task<IActionResult> UpdateVehicle(int id, [FromBody]VehiclesViewModel vehicleVM)
        {
            //logger.LogInformation("UpdateVehicle:"+id);                        
            if(!ModelState.IsValid) 
                return BadRequest(vehicleVM);

            var vehicleToUpdate=await vehicleRepository.GetOneVehicle(id);
            //logger.LogInformation("vehicleToUpdate:"+vehicleToUpdate.Id);

            if(vehicleToUpdate==null){
                ModelState.AddModelError("VehicleId","Invalid VehicleId");
                return NotFound(ModelState);
            }
            mapper.Map<VehiclesViewModel,Vehicle>(vehicleVM,vehicleToUpdate);
            vehicleToUpdate.LastUpdate=DateTime.Now;

            if (await TryUpdateModelAsync<Vehicle>(vehicleToUpdate,"",s => s.ContactName,s=>s.ContactEmail,s=>s.ContactPhone))
            {
                try
                {
                    await unitOfWork.Commit();
                    //return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException /* ex */)
                {
                    //Log the error (uncomment ex variable name and write a log.)
                    ModelState.AddModelError("", "Unable to save changes. " +
                        "Try again, and if the problem persists, " +
                        "see your system administrator.");
                }
            }
            //await vehicleRepository.UpdateVehicle(vehicleToUpdate);
            //await vehicleRepository.SaveChangesAsync();
            var vm=mapper.Map<VehiclesViewModel>(vehicleToUpdate);
            return Ok(vm);            
        }
        [HttpDelete("api/vehicles/{id}")]
        public async Task<IActionResult> DeleteVehicle(int id)
        {
            if(!ModelState.IsValid) 
                return BadRequest(ModelState);         
             var vehicleToDelete=await vehicleRepository.GetOneVehicle(id);
            if (vehicleToDelete==null){
                ModelState.AddModelError("VehicleId","Invalid VehicleId");
                return NotFound(ModelState);
            }
            await vehicleRepository.DeleteVehicle(vehicleToDelete);
            await unitOfWork.Commit();
            return Ok(id);
        }
    }
}