using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HtmlAgilityPack;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using WebApplication1.Models;

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
        
        
        public ActionResult Index(string products)
        {
            System.Net.ServicePointManager.Expect100Continue = false;


            IWebDriver driver = new ChromeDriver();
            var flipkartUrl = "https://www.flipkart.com/clothing-and-accessories/topwear/tshirt/men-tshirt/pr?sid=clo,ash,ank,edy&otracker=categorytree&otracker=nmenu_sub_Men_0_T-Shirts";
            driver.Navigate().GoToUrl(flipkartUrl);
            var flipkartImages = driver.FindElements(By.XPath("//img[@class='_3togXc']"));
            List<String> flipkartList = new List<string>();

            for (int i = 0; i < 10; i++)
            {
                var links = flipkartImages[i].GetAttribute("src");
                flipkartList.Add(links);

            }

            var amazonUrl = "https://www.amazon.in/gp/bestsellers/apparel/1968120031?ref_=Oct_s9_apbd_obs_hd_bw_b29C1vT_S&pf_rd_r=5FQ1NJHZWS2BZE31AABD&pf_rd_p=30ecd3d7-225a-5b6e-83ae-823d50b6e95c&pf_rd_s=merchandised-search-10&pf_rd_t=BROWSE&pf_rd_i=1968120031";
            driver.Navigate().GoToUrl(amazonUrl);
            var amazonImages = driver.FindElements(By.XPath("//img"));
            List<String> amazonList = new List<string>();

            for (int i = 5; i < 15; i++)
            {
                var links = amazonImages[i].GetAttribute("src");
                amazonList.Add(links);

            }

            var koovsUrl = "https://www.koovs.com/men/t-shirts-and-polo-shirts/";
            driver.Navigate().GoToUrl(koovsUrl);
            var koovsImages = driver.FindElements(By.XPath("//img[@class='prodImg']"));
            List<String> koovsList = new List<string>();

            for (int i = 0; i < 20; i=i+2)
            {
                var links = koovsImages[i].GetAttribute("src");
                koovsList.Add(links);

            }

            
            TrendingProductValue[] product = new TrendingProductValue[30]; 
            for(int i = 0; i < product.Length; i++)
            {
                product[i] = new TrendingProductValue();
            }

            int j = 0;
            int flipkartRank = 1;
            foreach (var item in flipkartList)
            {
                product[j].imageLink = item;
                product[j].internalRank = flipkartRank;
                product[j].websiteTraffic = 122;
                j++;
                flipkartRank++;
            }
            int amazonRank = 1;
            foreach (var item in amazonList)
            {
                product[j].imageLink = item;
                product[j].internalRank = amazonRank;
                product[j].websiteTraffic = 111;
                j++;
                amazonRank++;
            }
            int koovsRank = 1;
            foreach (var item in koovsList)
            {
                product[j].imageLink = item;
                product[j].internalRank = koovsRank;
                product[j].websiteTraffic = 47143;
                j++;
                koovsRank++;
            }

            /*
            var similarWebFlipkartUrl = "https://www.similarweb.com/website/flipkart.com/?competitors=amazon.in";
            driver.Navigate().GoToUrl(similarWebFlipkartUrl);
            var flipkartTraffic= driver.FindElements(By.XPath("//span[@class='websiteRanks-value']"));
            List<String> trafficList = new List<string>();

            for (int i = 0; i < 2; i++)
            {
                var data = flipkartTraffic[i].Text;
                trafficList.Add(data);
            }
            */


            ViewBag.Flipkart = flipkartList;
            ViewBag.Amazon = amazonList;
            //ViewBag.Traffic = trafficList;
            ViewBag.Koovs = koovsList;
            ViewBag.Products = product;
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