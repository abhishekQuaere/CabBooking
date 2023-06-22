using CabBooking.DbRepository;
using CabBooking.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace CabBooking.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin

        Common com = new Common();
        AdminDb adminDb = new AdminDb();


        #region CreateResponse
        /// <summary>
        /// Creates a successfull response with redirection and default MessageStream -> MessageStream.RecordUpdatedSuccessfully .
        /// </summary>
        /// <param name="Action"></param>
        /// <param name="Controller"></param>
        //private void CreateResponse(string Action, string Controller)
        //{
        //    ViewBag.ResponseURL = Url.Action(Action, Controller);
        //    ViewBag.ResponseMessage = MessageStream.RecordUpdatedSuccessfully;
        //    ViewBag.ResponseType = ResponseType.Success;
        //}

        /// <summary>
        /// Creates a successfull response with redirection and message.
        /// </summary>
        /// <param name="Action"></param>
        /// <param name="Controller"></param>
        /// <param name="Message"></param>
        private void CreateResponse(string Action, string Controller, string Message)
        {
            ViewBag.ResponseURL = Url.Action(Action, Controller);
            ViewBag.ResponseMessage = Message;
            ViewBag.ResponseType = ResponseType.Success;
        }

        /// <summary>
        /// Creates a response with customized parameters. Keep action and controller empty / blank if redirection is not required.
        /// </summary>
        /// <param name="Action"></param>
        /// <param name="Controller"></param>
        /// <param name="Message"></param>
        /// <param name="ResponseType_success_warning_error_OR_info"></param>
        private void CreateResponse(string Action, string Controller, string Message, string ResponseType_success_warning_error_OR_info, bool UseTempData = false, string ResponseTitle = "")
        {
            if (!string.IsNullOrEmpty(Action))
            {
                ViewBag.ResponseURL = Url.Action(Action, Controller);
                if (UseTempData)
                    TempData["ResponseURL"] = Url.Action(Action, Controller);
            }

            ViewBag.ResponseMessage = Message;
            ViewBag.ResponseType = ResponseType_success_warning_error_OR_info;
            if (!string.IsNullOrEmpty(ResponseTitle)) ViewBag.ResponseTitle = ResponseTitle;

            if (UseTempData)
            {
                TempData["ResponseMessage"] = Message;
                TempData["ResponseType"] = ResponseType_success_warning_error_OR_info;
            }
        }

        private void FlushCreateResponse()
        {
            TempData.Remove("ResponseURL");
            TempData.Remove("ResponseMessage");
            TempData.Remove("ResponseType");
        }
        #endregion

        public ActionResult Dashboard()
        {
            return View();
        }


        public ActionResult ActiveTrips()
        {
            return View();
        }
        public ActionResult CompleteTrips()
        {

            return View();
        }



        public ActionResult AddDriver()
        {

            return View();
        }

        public ActionResult AllDriver()
        {
            return View();
        }


        [HttpGet]
        public ActionResult AddVehicle(int? id)
        {
            AddVehicle model = new AddVehicle();
            if (Convert.ToInt32(id) != 0)
            {
                model.Id = Convert.ToInt32(id);
                model.ProcId = 1;
                model.list = adminDb.GetAllVehicle<AddVehicle>(model);
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult AddVehicle(AddVehicle model)
        {
            ResultSet obj = new ResultSet();
            obj = adminDb.AddUpdateVehicle<ResultSet>(model);
            if (obj.flag == 1)
            {
                CreateResponse("AllVehicle", "Admin", obj.msg, ResponseType.Success);
            }
            else if (obj.flag == 2)
            {
                CreateResponse("AllVehicle", "Admin", obj.msg, ResponseType.Success);
            }
            else
            {
                CreateResponse("AddVehicle", "Admin", "Opps! Somthing went wrong", ResponseType.Error);
            }
            return View(model);
        }

        public ActionResult AddImages(int id)
        {
            AddVehicle model = new AddVehicle();
            model.Id = id;
            return View(model);
        }

        [HttpPost] // for multiupload use '[]'
        public ActionResult UploadProductImages(int id, HttpPostedFileBase[] productpictures)
        {
            string Dirpath = "~/Content/VehicleImages/";
            string path = "";
            bool res = false;
            string msg = "";
            VehicleImage model = new VehicleImage();

            foreach (var pp in productpictures)
            {
                //image/jpeg
                string filename = pp.FileName;
                if (!Directory.Exists(Server.MapPath(Dirpath)))
                {
                    Directory.CreateDirectory(Server.MapPath(Dirpath));
                }

                string ext = Path.GetExtension(pp.FileName);
                var status = com.ValidateImagePDF_FileExtWithSize(pp, 2048);
                if (status == "Valid")
                {

                    filename = DateTime.Now.ToString("yyyyMMddHHmmssffff") + "_" + filename;
                    string completepath = Path.Combine(Server.MapPath(Dirpath), filename);
                    if (System.IO.File.Exists(completepath))
                    {
                        System.IO.File.Delete(completepath);
                    }

                    pp.SaveAs(completepath);
                    path = Dirpath + filename;
                    res = true;
                    model.VehicleId = id;
                    model.VehiclePhoto = path;
                    adminDb.AddVehicleImageById<VehicleImage>(model);
                }
                else
                {
                    msg = status;
                }


                //string ext = pp.ContentType.Split('/')[1];
                //AddVehicle productPicture = new AddVehicle();
                //string filename = $"f_{productPicture.Id}.{ext}";  //f_1d1d1d-ddd.png
                //string fullFilePath = uploadFullPath + "/" + filename;
                //pp.SaveAs(fullFilePath);


                //productPicture.FileName = filename;
                //productPicture.Id = id;

                //db.ProductPictures.Add(productPicture);
                //db.SaveChanges();
            }
            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

        public ActionResult GetProductImages(int id)
        {
            VehicleImage model = new VehicleImage();
            model.VehicleId = id;
            List<VehicleImage> productPictures = adminDb.GetVehicleImageById<VehicleImage>(model);
            return PartialView("_ProductPictures", productPictures);
        }
        public ActionResult RemoveProductPicture(int id)
        {
            VehicleImage model = new VehicleImage();
            model.Id = id;
            adminDb.AddVehicleImageById<VehicleImage>(model);
            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

        public ActionResult AllVehicle()
        {
            AddVehicle model = new AddVehicle();
            model.ProcId = 2;
            model.list = adminDb.GetAllVehicle<AddVehicle>(model);
            return View(model);

        }
        public ActionResult AddFare()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddFare(Fares model)
        {
            ResultSet obj = new ResultSet();
            obj = adminDb.AddUpdateFare<ResultSet>(model);
            if (obj.flag == 1)
            {
                CreateResponse("AllFare", "Admin", obj.msg, ResponseType.Success);
            }
            else if (obj.flag == 2)
            {
                CreateResponse("AllFare", "Admin", obj.msg, ResponseType.Success);
            }
            else
            {
                CreateResponse("AddFare", "Admin", "Opps! Somthing went wrong", ResponseType.Error);
            }
            return View(model);
        }


        public ActionResult AllFare()
        {
            Fares model = new Fares();
            model.list = adminDb.GetAllFare<Fares>(model);
            return View(model);
        }
        public ActionResult FareFailList()
        {
            return View();
        }

        public JsonResult UploadFile(HttpPostedFileBase File)
        {
            string Dirpath = "~/Content/writereaddata/ResearchGuidance/";
            string path = "";
            string filename = File.FileName;
            bool res = false;
            string msg = "";
            if (!Directory.Exists(Server.MapPath(Dirpath)))
            {
                Directory.CreateDirectory(Server.MapPath(Dirpath));
            }
            string ext = Path.GetExtension(File.FileName);
            var status = com.ValidateImagePDF_FileExtWithSize(File, 2048);
            if (status == "Valid")
            {

                filename = DateTime.Now.ToString("yyyyMMddHHmmssffff") + "_" + filename;
                string completepath = Path.Combine(Server.MapPath(Dirpath), filename);
                if (System.IO.File.Exists(completepath))
                {
                    System.IO.File.Delete(completepath);
                }

                File.SaveAs(completepath);
                path = Dirpath + filename;
                res = true;
            }
            else
            {
                msg = status;
            }
            return Json(new { result = res, fpath = path, mesg = msg });
        }

    }
}