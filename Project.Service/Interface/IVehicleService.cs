using System;
using System.Collections.Generic;
using Project.Service.Models;
using Project.Service.DAL;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Service.Interface
{
    public interface IVehicleService
    {
        VehicleContext dbContext { get; set; }
        IEnumerable<VehicleModelVM> GetAllVehicleModels();
        VehicleModelVM FindModelById(int id);
        string AddModel(VehicleModelVM item);
        string EditModel(VehicleModelVM item);
        string DeleteModel(int id);

        IEnumerable<VehicleMakeVM> GetAllVehicleMakes();
        VehicleMakeVM FindMakeById(int id);
        string AddMake(VehicleMakeVM item);
        string EditMake(VehicleMakeVM item);
        string DeleteMake(int id);
    }
}
