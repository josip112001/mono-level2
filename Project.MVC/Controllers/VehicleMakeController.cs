using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Project.Service;
using Project.Service.Infrastructure;
using PagedList;
using PagedList.Mvc;
using Project.Service.Interface;
using Project.Service.Models;

namespace Project.MVC.Controllers
{
    public class VehicleMakeController : Controller
    {
        // GET: VehicleMake

        static readonly IVehicleService repository = new VehicleService();


        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {

            ViewBag.CurrentSort = sortOrder;
            ViewBag.Id = String.IsNullOrEmpty(sortOrder) ? "Id" : "";
            ViewBag.Name = sortOrder == "Name" ? "name_des" : "Name";
            ViewBag.Abrv = sortOrder == "Abrv" ? "abrv_des" : "Abrv";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            IEnumerable<VehicleMakeVM> vehicleMakeList = repository.GetAllVehicleMakes();

            if (!String.IsNullOrEmpty(searchString))
            {
                vehicleMakeList = vehicleMakeList.Where(s => s.Name.Contains(searchString)
                                       || s.Abrv.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "Id":
                    vehicleMakeList = vehicleMakeList.OrderByDescending(s => s.Id);
                    break;
                case "Name":
                    vehicleMakeList = vehicleMakeList.OrderBy(s => s.Name);
                    break;
                case "name_des":
                    vehicleMakeList = vehicleMakeList.OrderByDescending(s => s.Name);
                    break;
                case "Abrv":
                    vehicleMakeList = vehicleMakeList.OrderBy(s => s.Abrv);
                    break;
                case "abrv_des":
                    vehicleMakeList = vehicleMakeList.OrderByDescending(s => s.Abrv);
                    break;
                default:
                    vehicleMakeList = vehicleMakeList.OrderBy(s => s.Name);
                    break;
            }

            int pageSize = 3;
            int pageNumber = (page ?? 1);

            return View(vehicleMakeList.ToPagedList(pageNumber,pageSize));
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(VehicleMakeVM obj)
        {
            if (ModelState.IsValid)
            {
                string str = repository.AddMake(obj);
                if (str == "Ok")
                    return RedirectToAction("Index");
                return View(); // open error page
            }

            else
            {
                return View();
            }
        }

        [HttpGet]
        public ActionResult Edit(int Id)
        {
            return View(repository.FindMakeById((Id)));
        }

        [HttpPost]
        public ActionResult Edit(VehicleMakeVM obj)
        {
            if (ModelState.IsValid)
            {
                string str = repository.EditMake(obj);
                if (str == "Ok")
                    return RedirectToAction("Index");
                return View(); // open error page
            }

            else
            {
                return View();
            }
        }


        public ActionResult Delete(int id)
        {
            string str = repository.DeleteMake(id);
            if (str == "Ok")
                return RedirectToAction("Index");
            return View(); // open error page

        }
    }
}