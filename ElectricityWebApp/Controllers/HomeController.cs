using ElectricityWebApp.Models;
using Microsoft.AspNetCore.Mvc;
using ElectricityWebApp.DataAccess;
using System.Diagnostics;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net;
using System.IO;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace ElectricityWebApp.Controllers
{
    public class HomeController : Controller
    {
        HttpClient httpClient;

        static string BASE_URL = "https://api.eia.gov/series";
        static string API_KEY = "dBbaSkv56vK5WhwXVR6MtfklLtg0D6ejivzUcdxQ"; //Add your API key here inside ""

        // Obtaining the API key is easy. The same key should be usable across the entire
        // data.gov developer network, i.e. all data sources on data.gov.
        // https://www.nps.gov/subjects/developer/get-started.htm

        public readonly  ApplicationDbContext dbContext;

        public HomeController(ApplicationDbContext context)
        {
            dbContext = context;
        }
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult AboutUs()
        {
            return View();
        }

        public ActionResult Graph()
        {
            return View();
        }

        
        public ActionResult GetChartData()
        {
            var json = "";
            StringBuilder sb = new StringBuilder();
            List<object> chartData = new List<object>();

            string NATIONAL_PARK_API_PATH = BASE_URL + "/?series_id=ELEC.GEN.ALL-AK-99.A&api_key=" + API_KEY + "&out=json";
            System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
            using (var response = WebRequest.Create(NATIONAL_PARK_API_PATH).GetResponse()) //Saving the json response in database    
            {
                var dataStream = response.GetResponseStream();
                var reader = new StreamReader(dataStream);
                json = reader.ReadToEnd();
                dynamic instagramDataList = JsonConvert.DeserializeObject(json);

                
                List<int> labels = new List<int>();
                foreach (var row in instagramDataList.series)
                {
                    foreach (var data in row.data)
                    {

                        labels.Add(Convert.ToInt32(data[0]));
                    }
                }
                chartData.Add(labels);
                List<int> series1 = new List<int>();
                foreach (var row in instagramDataList.series)
                {
                    foreach (var data in row.data)
                    {

                        series1.Add(Convert.ToInt32(data[1]));
                    }
                }
                chartData.Add(series1);


            }
            return new JsonResult(chartData);

        }
        public ActionResult SaveRecordInDb()  //This is used to get fetch records from API  and save data in database.    
        {
           
            try
            {
                string NATIONAL_PARK_API_PATH = BASE_URL + "/?series_id=ELEC.GEN.ALL-AK-99.A;ELEC.GEN.ALL-AK-98.A;ELEC.GEN.ALL-AK-97.A&api_key=" + API_KEY + "&out=json";
                System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;   
            using (var response = WebRequest.Create(NATIONAL_PARK_API_PATH).GetResponse()) //Saving the json response in database    
            {    
                var dataStream = response.GetResponseStream();
        var reader = new StreamReader(dataStream);
        var json = reader.ReadToEnd();
        dynamic instagramDataList = JsonConvert.DeserializeObject(json);    
                foreach (var data in instagramDataList.series)    
                {    
                            var SeriesData = new Series();
                            SeriesData.series_id = data.series_id;
                            SeriesData.name = data.name;
                            SeriesData.units = data.units;
                            SeriesData.f = data.f;        
                            SeriesData.description= data.description;
                            SeriesData.source = data.source;
                            SeriesData.iso3166 = data.iso3166;
                            SeriesData.latlon = data.latlon;
                            SeriesData.latlon2 = data.latlon2;
                            SeriesData.geography = data.geography;
                            SeriesData.geography2 = data.geography2;
                            SeriesData.lastHistoricalPeriod= data.lastHistoricalPeriod;
                            SeriesData.start = data.start;
                            SeriesData.end = data.end;
                            SeriesData.updated = data.updated;
                        dbContext.series.Add(SeriesData);
                        dbContext.SaveChanges();
                        
                  }
                     reader.Close();
                    if (dataStream != null)
                    {

                        dataStream.Close();
                    }
            }
                return RedirectToAction("Index");
            }    
        catch (Exception ex)
{
    ViewBag.ErrorMessage = "Some exception occured" + ex;
    return View();
}    
    }    
  
    }
}




