using CabBooking.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;

namespace CabBooking.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class WebApiController : ApiController
    {
        HttpClient client;

        public WebApiController()
        {
            client = new HttpClient();
            //client.BaseAddress = baseAddress;
        }

        [HttpGet]
        public async Task<HttpResponseMessage> getAddress(string term)
        {
            try
            {
                string apiKey = "16c6b62bcedf492e923e0cfa39d58db9";
                var httpRequest = HttpContext.Current.Request;

                Uri baseAddress = new Uri("https://api.geoapify.com/v1/geocode/autocomplete?text="+term+"&apiKey="+apiKey+"");
                client.BaseAddress = baseAddress;

                HttpResponseMessage response = await client.GetAsync(client.BaseAddress);
                if (!response.IsSuccessStatusCode)
                    throw new Exception("Third party API request failed");


                if (response.IsSuccessStatusCode)
                {
                    string responseString = response.Content.ReadAsStringAsync().Result;

                    Newtonsoft.Json.Linq.JObject json = Newtonsoft.Json.Linq.JObject.Parse(responseString);
                    var product = Newtonsoft.Json.JsonConvert.DeserializeObject<RootV2>(responseString);
                    var j = JsonSerializer.Serialize(product);
                }
                return response;
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.OK, ex.Message);
            }
        }

    }
}
