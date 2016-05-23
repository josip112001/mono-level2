using Project.Service.DAL;
using Project.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project.Service.Models;
using AutoMapper;

namespace Project.Service.Infrastructure
{
   public class VehicleService : IVehicleService
    {
        public VehicleContext  dbContext { get; set; }

        public VehicleService()
        {
            dbContext = new VehicleContext();
        }

        public IEnumerable<VehicleModelVM> GetAllVehicleModels()
        {
            var result = from o in dbContext.Models
                         join k in dbContext.Makes on o.MakeId equals k.Id
                         select new VehicleModelVM { Name = o.Name, Abrv = o.Abrv, MakeName = k.Name, Id = o.Id };
            return result;
        }

        public VehicleModelVM FindModelById(int id)
        {
            try
            {
                Mapper.CreateMap<VehicleModel, VehicleModelVM>();
                var modelDetails = dbContext.Models.FirstOrDefault(x => x.Id == id);
                var m = Mapper.Map<VehicleModel, VehicleModelVM>(modelDetails);
                return m;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public string AddModel(VehicleModelVM item)
        {
            string result = string.Empty;
            if (item==null)
            {
                throw new ArgumentNullException("item");
            }

            try
            {
                Mapper.CreateMap<VehicleModelVM, VehicleModel>();
                var m = Mapper.Map<VehicleModelVM,VehicleModel>(item);
                dbContext.Models.Add(m);
                dbContext.SaveChanges();
                result = "Ok";
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            return result;
        }

        public string EditModel(VehicleModelVM item)
        {
            string result = string.Empty;
            try
            {
                var m = dbContext.Models.FirstOrDefault(x => x.Id == item.Id);
                if (m!= null)
                {
                    m.Name = item.Name;
                    m.MakeId = item.MakeId;
                    m.Abrv = item.Abrv;
                    dbContext.SaveChanges();
                    result = "Ok";
                }
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            return result;
        }

        public string DeleteModel(int id)
        {
            string result = string.Empty;
            try
            {
                var m = dbContext.Models.FirstOrDefault(x => x.Id == id);
                if (m!=null)
                {
                    dbContext.Models.Remove(m);
                    dbContext.SaveChanges();
                    result = "Ok";
                }
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            return result;
        }

        public IEnumerable<VehicleMakeVM> GetAllVehicleMakes()
        {
            Mapper.CreateMap<VehicleMake, VehicleMakeVM>();
            var makesList = from make in dbContext.Makes select make;
            var makes = new List<VehicleMakeVM>();
            if (makesList.Any())
            {
                foreach (var make in makesList)
                {
                    VehicleMakeVM makeModel = Mapper.Map<VehicleMake, VehicleMakeVM>(make);
                    makes.Add(makeModel);
                }
            }
            return makes;
        }

        public VehicleMakeVM FindMakeById(int id)
        {
            string result = string.Empty;
            try
            {
                Mapper.CreateMap<VehicleMake, VehicleMakeVM>();
                var makeDetails = dbContext.Makes.FirstOrDefault(x => x.Id == id);
                var make = Mapper.Map<VehicleMake, VehicleMakeVM>(makeDetails);
                return make;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string AddMake(VehicleMakeVM item)
        {
            string result = string.Empty;
            if (item == null)
            {
                throw new ArgumentNullException("item");
            }

            try
            {
                Mapper.CreateMap<VehicleMakeVM, VehicleMake>();
                var make = Mapper.Map<VehicleMakeVM, VehicleMake>(item);
                dbContext.Makes.Add(make);
                dbContext.SaveChanges();
                result = "Ok";
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            return result;
        }

        public string EditMake(VehicleMakeVM item)
        {
            string result = string.Empty;
            try
            {
                var make = dbContext.Makes.FirstOrDefault(x => x.Id == item.Id);
                if (make != null)
                {
                    make.Name = item.Name;
                    make.Abrv = item.Abrv;
                    dbContext.SaveChanges();
                    result = "Ok";
                }
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            return result;
        }

        public string DeleteMake(int id)
        {
            string result = string.Empty;
            try
            {
                var make = dbContext.Makes.FirstOrDefault(x => x.Id == id);
                if (make != null)
                {
                    dbContext.Makes.Remove(make);
                    dbContext.SaveChanges();
                    result = "Ok";
                }
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            return result;
        }
    }
}
