using CabBooking.DbRepository;
using CabBooking.Models;
using CabBooking.Utilities;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web.Hosting;
using System.Web.Mvc;

namespace CabBooking.Controllers
{
    public class HomeController : Controller
    {
        HomeDb homeDb = new HomeDb();
        SessionManager sm = new SessionManager();

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

        [HttpGet]
        public ActionResult FastYatra()
        {
            NewBooking model = new NewBooking();
            ViewBag.VehiclesList = homeDb.GetVehicles();
            return View(model);
        }

        [HttpPost]
        public ActionResult FastYatra(NewBooking model)
        {
            ResultSet obj = new ResultSet();
            if (!String.IsNullOrEmpty(model.startDest) && !String.IsNullOrEmpty(model.endDest))
            {
                ViewBag.VehiclesList = homeDb.GetVehicles();
                obj = homeDb.NewAddBooking<ResultSet>(model);
                if (obj.flag == 1)
                {
                    CreateResponse("FastYatra", "Home", obj.msg, ResponseType.Success);
                }
            }
            else
            {
                CreateResponse("", "", "All fields required !", ResponseType.Success);
            }
            return View(model);
        }

        public ActionResult Index()
        {
            MainModel model = new MainModel();
            model.ProcId = 2;
            model.list = homeDb.GetAllVehicle<MainModel>(model);
            ViewBag.VehiclesList = homeDb.GetVehicles();
            return View(model);
        }

        [HttpPost]
        public ActionResult Index(MainModel model)
        {
            //string Price = model.TotalPrices.Replace("₹", "");
            //model.TotalPrices = Price;
            ResultSet obj = new ResultSet();
            ViewBag.VehiclesList = homeDb.GetVehicles();
            obj = homeDb.AddBooking<ResultSet>(model);
            if (obj.flag == 1)
            {
                model.ProcId = 2;
                sendEmail("Support@fastyatra.com", model.Name, model.MobileNo);
                model.list = homeDb.GetAllVehicle<MainModel>(model);
                if (!String.IsNullOrEmpty(obj.msg))
                {
                    TempData["response"] = "success";
                }
                else
                {
                    TempData["response"] = "error";
                }
                //CreateResponse("Index", "Home", obj.msg, ResponseType.Success);
                TempData["msg"] = obj.msg;
            }
            return View(model);
        }

        //public ActionResult GetCoordinates()
        //{
        //    Location model = new Location();
        //    GeoCoordinateWatcher watcher = new GeoCoordinateWatcher();

        //    if (watcher.TryStart(false, TimeSpan.FromSeconds(10)))
        //    {
        //        GeoCoordinate coordinate = watcher.Position.Location;

        //        if (coordinate.IsUnknown)
        //        {
        //            // Location data is not available
        //        }
        //        else
        //        {
        //            double latitude = coordinate.Latitude;
        //            double longitude = coordinate.Longitude;

        //            model.Latitude = coordinate.Latitude;
        //            model.Longitude = coordinate.Longitude;

        //            // Do something with the latitude and longitude
        //            ViewBag.Latitude = latitude;
        //            ViewBag.Longitude = longitude;
        //        }
        //    }
        //    else
        //    {
        //        // Location services are not available or permission is denied
        //    }

        //    return null;
        //}

        //public async Task<ActionResult> GetCoordinates(string address)
        //{
        //    string apiKey = "hXk4E43uNtZtrvB74g3WufgtFeYmjCUA";
        //    string apiUrl = $"http://www.mapquestapi.com/geocoding/v1/address?key={apiKey}&location={Uri.EscapeDataString(address)}";

        //    try
        //    {
        //        using (HttpClient client = new HttpClient())
        //        {
        //            HttpResponseMessage response = await client.GetAsync(apiUrl);

        //            if (response.IsSuccessStatusCode)
        //            {
        //                string content = await response.Content.ReadAsStringAsync();

        //                var result = JsonConvert.DeserializeObject<NominatimResult[]>(content);

        //                if (result.Length > 0)
        //                {
        //                    double latitude = double.Parse(result[0].Lat);
        //                    double longitude = double.Parse(result[0].Lon);

        //                    // Do something with the latitude and longitude
        //                    ViewBag.Latitude = latitude;
        //                    ViewBag.Longitude = longitude;
        //                }
        //                else
        //                {
        //                    // Address not found
        //                    ViewBag.Error = "Address not found";
        //                }
        //            }
        //            else
        //            {
        //                // Handle API error
        //                string errorMessage = $"API request failed with status code: {response.StatusCode}";
        //                ViewBag.Error = errorMessage;
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        // Handle exception
        //        ViewBag.Error = "An error occurred: " + ex.Message;
        //    }

        //    return View();
        //}

        public JsonResult GetCoordinates(string address)
        {
            LatLng obj = new LatLng();
            string apiKey = "hXk4E43uNtZtrvB74g3WufgtFeYmjCUA";
            string apiUrl = $"http://www.mapquestapi.com/geocoding/v1/address?key={apiKey}&location={Uri.EscapeDataString(address)}";

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(apiUrl);
            request.Method = "GET";

            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            {
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    using (Stream stream = response.GetResponseStream())
                    {
                        StreamReader reader = new StreamReader(stream);
                        string content = reader.ReadToEnd();

                        var result = JsonConvert.DeserializeObject<Root>(content);

                        if (result.results.Count > 0)
                        {
                            double latitude = result.results[0].locations[0].displayLatLng.lat;
                            double longitude = result.results[0].locations[0].displayLatLng.lng;

                            // Do something with the latitude and longitude
                            obj.lat = latitude;
                            obj.lng = longitude;
                        }
                        else
                        {
                            // Address not found
                            ViewBag.Error = "Address not found";
                        }
                    }
                }
                else
                {
                    // Handle API error
                    ViewBag.Error = "Failed to retrieve coordinates";
                }
            }

            return Json(obj, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetAddressSuggestions(string term)
        {
            string apiKey = "hXk4E43uNtZtrvB74g3WufgtFeYmjCUA";
            string apiUrl = $"http://www.mapquestapi.com/geocoding/v1/address?key={apiKey}&location={term}";

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(apiUrl);
            request.Method = "GET";

            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            {
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    using (Stream stream = response.GetResponseStream())
                    {
                        StreamReader reader = new StreamReader(stream);
                        string content = reader.ReadToEnd();

                        var result = JsonConvert.DeserializeObject<Root>(content);

                        if (result.results.Count > 0)
                        {
                            var locations = result.results[0].locations;
                            List<string> addressSuggestions = locations.Select(l => l.Street + ", " + l.adminArea5 + ", " + l.adminArea3 + " " + l.postalCode).ToList();

                            return Json(addressSuggestions, JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            // Address not found
                            ViewBag.Error = "Address not found";
                        }
                    }
                }
                else
                {
                    // Handle API error
                    ViewBag.Error = "Failed to retrieve coordinates";
                }
            }

            return Json("", JsonRequestBehavior.AllowGet);
        }

        public JsonResult getAddress(string term)
        {
            string apiKey = "16c6b62bcedf492e923e0cfa39d58db9";
            string apiUrl = $"https://api.geoapify.com/v1/geocode/autocomplete?text={term}&apiKey={apiKey}";
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(apiUrl);
                request.Method = "GET";

                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        using (Stream stream = response.GetResponseStream())
                        {
                            StreamReader reader = new StreamReader(stream);
                            string content = reader.ReadToEnd();

                            var result = JsonConvert.DeserializeObject<RootV2>(content);

                            if (result.features.Count > 0)
                            {

                                var locations = result.features;

                                var addressSuggestions = (from N in locations
                                                          select new { N.properties.formatted, N.properties.lat, N.properties.lon });

                                //List<string> addressSuggestions = locations.Select(l => l.properties.name+","+l.properties.village + ", " + l.properties.city + ", " + l.properties.county + " " + l.properties.state_district+","+l.properties.state+","+l.properties.country).ToList();

                                return Json(addressSuggestions, JsonRequestBehavior.AllowGet);
                            }
                            else
                            {
                                // Address not found
                                ViewBag.Error = "Address not found";
                            }
                        }
                    }
                    else
                    {
                        // Handle API error
                        ViewBag.Error = "Failed to retrieve coordinates";
                    }
                }
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
            }

            return Json("", JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetCalculateDistance(LatLng source, LatLng destination)
        {
            DistanceCalculator distanceCalculator = new DistanceCalculator();
            var distance = distanceCalculator.CalculateDistance(source, destination);
            return Json(distance.ToString("#.##"), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(account model)
        {
            if (!String.IsNullOrEmpty(model.LoginId) && !String.IsNullOrEmpty(model.password))
            {
                var res = homeDb.CheckLoginType<account>(model);
                if (res.id > 0)
                {
                    if (res.flag == 1)
                    {
                        CreateResponse("Index", "home", res.msg, ResponseType.Error);
                    }
                    else
                    {
                        sm.userId = res.id;
                        sm.roleName = res.rolename;
                        return RedirectToAction("Dashboard", "Admin");
                    }
                }
                else
                {
                    CreateResponse("", "", "Invalid login and password ! Please contact to administrator", ResponseType.Error);
                }
            }
            return View(model);
        }

        public ActionResult Logout()
        {
            Session.Abandon();
            Session.RemoveAll();
            Session.Clear();
            return RedirectToAction("Index");
        }

        public void sendEmail(string email, string UserId, string Pass)
        {
            string MessageBody = getMessageBody(UserId, Pass);
            string subject = "New Booking";
            Mail.SendEmailMessag(email, subject, MessageBody);
            //return Json(otp, JsonRequestBehavior.AllowGet);
        }

        public String getMessageBody(string UserId, string Pass)
        {
            StreamReader rd = new StreamReader(HostingEnvironment.MapPath(@"~/EmailTemplates/EmailRemplates.html"));
            string msgBody = rd.ReadToEnd();
            msgBody = msgBody.Replace("[User]", "Kapil Tripathi");
            msgBody = msgBody.Replace("[UserId]", UserId);
            msgBody = msgBody.Replace("[Password]", Pass);
            return msgBody;
        }

        public ActionResult AboutUs()
        {
            return View();
        }

        public ActionResult AboutCompany()
        {
            return View();
        }

        [HttpGet]
        public ActionResult BookCab()
        {
            NewBooking model = new NewBooking();
            ViewBag.VehiclesList = homeDb.GetVehicles();
            return View(model);
        }

        [HttpPost]
        public ActionResult BookCab(NewBooking model)
        {
            ResultSet obj = new ResultSet();
            if (!String.IsNullOrEmpty(model.startDest) && !String.IsNullOrEmpty(model.endDest))
            {
                ViewBag.VehiclesList = homeDb.GetVehicles();
                obj = homeDb.NewAddBooking<ResultSet>(model);
                if (obj.flag == 1)
                {
                    CreateResponse("BookCab", "Home", obj.msg, ResponseType.Success);
                }
            }
            else
            {
                CreateResponse("", "", "All fields required !", ResponseType.Success);
            }
            return View(model);
        }

        [HttpGet]
        public ActionResult ContactUs()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ContactUs(ContactUs model)
        {
            ResultSet obj = new ResultSet();
            obj = homeDb.ContactUs<ResultSet>(model);
            if (obj.flag == 1)
            {
                CreateResponse("ContactUs", "Home", obj.msg, ResponseType.Success);
            }
            return View(model);
        }

    }

}