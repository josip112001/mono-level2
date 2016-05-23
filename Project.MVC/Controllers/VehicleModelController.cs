using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Project.Service.Interface;
using Project.Service.Models;
using Project.Service.Infrastructure;
using PagedList;

namespace Project.MVC.Controllers
{
    public class VehicleModelController : Controller
    {
        static readonly IVehicleService repository = new VehicleService();

        // GET: VehicleModel
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentFilter = sortOrder;
            ViewBag.Id = String.IsNullOrEmpty(sortOrder) ? "Id" : "";
            ViewBag.Name = sortOrder == "Name" ? "Name_desc" : "Name";
            ViewBag.Abrv = sortOrder == "Abrv" ? "Abrv_desc" : "Abrv";
            ViewBag.MakeName = sortOrder == "MakeName" ? "MakeName_desc" : "MakeName";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            IEnumerable<VehicleModelVM> vehicleModelList = repository.GetAllVehicleModels();

            if (!String.IsNullOrEmpty(searchString))
            {
                vehicleModelList = vehicleModelList.Where(s => s.Name.Contains(searchString)
                || s.Abrv.Contains(searchString)
                || s.MakeName.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "Id":
                    vehicleModelList = vehicleModelList.OrderByDescending(s => s.Id);
                    break;
                case "Name":
                    vehicleModelList = vehicleModelList.OrderBy(s => s.Name);
                    break;
                case "Name_desc":
                    vehicleModelList = vehicleModelList.OrderByDescending(s => s.Name);
                    break;
                case "Abrc":
                    vehicleModelList = vehicleModelList.OrderBy(s => s.Abrv);
                    break;
                case "Abrv_desc":
                    vehicleModelList = vehicleModelList.OrderByDescending(s => s.Abrv);
                    break;
                case "MakeName":
                    vehicleModelList = vehicleModelList.OrderBy(s => s.MakeName);
                    break;
                case "MakeName_desc":
                    vehicleModelList = vehicleModelList.OrderByDescending(s => s.MakeName);
                    break;
                default:
                    vehicleModelList = vehicleModelList.OrderBy(s => s.Name);
                    break;
            }

            int pageSize = 3;
            int pageNumber = (page ?? 1);

            return View(vehicleModelList.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult Create()
        {
            IEnumerable<VehicleMakeVM> listVehicleMake = repository.GetAllVehicleMakes();

            SelectList list = new SelectList(listVehicleMake, "Id", "Name");
            ViewBag.MakeList = list;

            return View();
        }

        [HttpPost]
        public ActionResult Create(VehicleModelVM obj)
        {
            if (ModelState.IsValid)
            {
                string str = repository.AddModel(obj);
                if (str == "Ok")
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return View();
                }
            }
            else
            {
                return View();
            }
        }

        [HttpGet]
        public ActionResult Edit(int Id)
        {
            IEnumerable<VehicleMakeVM> listVehicleMake = repository.GetAllVehicleMakes();
            SelectList list = new SelectList(listVehicleMake, "Id", "Name");
            ViewBag.MakeList = list;
            return View(repository.FindModelById(Id));
        }

        [HttpPost]
        public ActionResult Edit(VehicleModelVM obj)
        {
            if (ModelState.IsValid)
            {
                string str = repository.EditModel(obj);
                if (str == "Ok")
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return View();
                }
            }
            else
            {
                return View();
            }
        }

        public ActionResult Delete(int id)
        {
            string str = repository.DeleteModel(id);
            if (str == "Ok")
            {
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }
        }
    }
}
