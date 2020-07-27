using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HtmlAgilityPack;

namespace WebApplication1.Controllers
{

    class MyWebClient : WebClient
    {
        protected override WebRequest GetWebRequest(Uri address)
        {
            HttpWebRequest request = base.GetWebRequest(address) as HttpWebRequest;
            request.Proxy.Credentials = System.Net.CredentialCache.DefaultCredentials;
            request.AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip;
            return request;
        }
    }

    public class HomeController : Controller
    {
        
        /*
        public String Index(string id)
        {
            return "id = " + id;
        }
        */
        
        
        public ActionResult Index()
        {
            System.Net.ServicePointManager.Expect100Continue = false;

            var data = new MyWebClient().DownloadString("https://www.flipkart.com/clothing-and-accessories/topwear/tshirt/men-tshirt/pr?sid=clo,ash,ank,edy&otracker=categorytree&otracker=nmenu_sub_Men_0_T-Shirts");
            var doc = new HtmlDocument();
            doc.LoadHtml(data);

            List<String> Output = new List<string>();
            var HeaderNames = doc.DocumentNode.SelectNodes("//a[@class='_2mylT6']").ToList();
            foreach (var item in HeaderNames)
            {
                Output.Add(item.InnerText.ToString());
            }

            ViewBag.Products = Output;
            return View();
        }
        

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}