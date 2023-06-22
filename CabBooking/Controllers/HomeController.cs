using CabBooking.DbRepository;
using CabBooking.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CabBooking.Controllers
{
    public class HomeController : Controller
    {
        HomeDb homeDb = new HomeDb();

        public ActionResult Index()
        {
            MainModel model = new MainModel(); 
            model.ProcId = 2;
            model.list = homeDb.GetAllVehicle<MainModel>(model);
            return View(model);
        }

      

    }
      
}