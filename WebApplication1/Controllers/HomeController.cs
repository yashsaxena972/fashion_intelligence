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
        
        public ActionResult Index(string products)
        {
            System.Net.ServicePointManager.Expect100Continue = false;


            IWebDriver driver = new ChromeDriver();

            TrendingProductValue[] product = new TrendingProductValue[40];
            for (int i = 0; i < product.Length; i++)
            {
                product[i] = new TrendingProductValue();
            }

            List<String> flipkartList = new List<string>();
            List<String> amazonList = new List<string>();
            List<String> koovsList = new List<string>();

            if (products == "tshirt" || products == null)
            {
                var flipkartUrl = "https://www.flipkart.com/clothing-and-accessories/topwear/tshirt/men-tshirt/pr?sid=clo,ash,ank,edy&otracker=categorytree&otracker=nmenu_sub_Men_0_T-Shirts";
                driver.Navigate().GoToUrl(flipkartUrl);
                var flipkartImages = driver.FindElements(By.XPath("//img[@class='_3togXc']"));
                

                for (int i = 0; i < 10; i++)
                {
                    var links = flipkartImages[i].GetAttribute("src");
                    flipkartList.Add(links);

                }

                var amazonUrl = "https://www.amazon.in/gp/bestsellers/apparel/1968120031?ref_=Oct_s9_apbd_obs_hd_bw_b29C1vT_S&pf_rd_r=5FQ1NJHZWS2BZE31AABD&pf_rd_p=30ecd3d7-225a-5b6e-83ae-823d50b6e95c&pf_rd_s=merchandised-search-10&pf_rd_t=BROWSE&pf_rd_i=1968120031";
                driver.Navigate().GoToUrl(amazonUrl);
                var amazonImages = driver.FindElements(By.XPath("//img"));
                

                for (int i = 5; i < 15; i++)
                {
                    var links = amazonImages[i].GetAttribute("src");
                    amazonList.Add(links);

                }

                var koovsUrl = "https://www.koovs.com/men/t-shirts-and-polo-shirts/";
                driver.Navigate().GoToUrl(koovsUrl);
                var koovsImages = driver.FindElements(By.XPath("//img[@class='prodImg']"));
                

                for (int i = 0; i < 20; i = i + 2)
                {
                    var links = koovsImages[i].GetAttribute("src");
                    koovsList.Add(links);

                }


                int j = 0;
                int flipkartRank = 1;
                foreach (var item in flipkartList)
                {
                    product[j].imageLink = item;
                    product[j].internalRank = flipkartRank;
                    product[j].websiteTraffic = 122;
                    product[j].website = "Flipkart";
                    j++;
                    flipkartRank++;
                }
                int amazonRank = 1;
                foreach (var item in amazonList)
                {
                    product[j].imageLink = item;
                    product[j].internalRank = amazonRank;
                    product[j].websiteTraffic = 111;
                    product[j].website = "Amazon";
                    j++;
                    amazonRank++;
                }
                int koovsRank = 1;
                foreach (var item in koovsList)
                {
                    product[j].imageLink = item;
                    product[j].internalRank = koovsRank;
                    product[j].websiteTraffic = 47143;
                    product[j].website = "Koovs";
                    j++;
                    koovsRank++;
                }
            }
            else if(products == "jeans")
            {
                var flipkartUrl = "https://www.flipkart.com/clothing-and-accessories/bottomwear/jeans/men-jeans/pr?sid=clo%2Cvua%2Ck58%2Ci51&otracker=categorytree&otracker=nmenu_sub_Men_0_Jeans&p%5B%5D=facets.brand%255B%255D%3DWrangler&p%5B%5D=facets.brand%255B%255D%3DLevi%2527s&p%5B%5D=facets.brand%255B%255D%3DLee&p%5B%5D=facets.brand%255B%255D%3DPeter%2BEngland&p%5B%5D=facets.brand%255B%255D%3DU.S.%2BPolo%2BAssn&p%5B%5D=facets.brand%255B%255D%3DUnited%2BColors%2Bof%2BBenetton&p%5B%5D=facets.brand%255B%255D%3DDenizen&p%5B%5D=facets.brand%255B%255D%3DJohn%2BPlayers&p%5B%5D=facets.brand%255B%255D%3DFlying%2BMachine&p%5B%5D=facets.brand%255B%255D%3DPepe%2BJeans&p%5B%5D=facets.brand%255B%255D%3DNumero%2BUno&p%5B%5D=facets.brand%255B%255D%3DRoadster&p%5B%5D=facets.brand%255B%255D%3DJack%2B%2526%2BJones&p%5B%5D=facets.brand%255B%255D%3DEd%2BHardy&p%5B%5D=facets.brand%255B%255D%3DParx&p%5B%5D=facets.brand%255B%255D%3DU.S.%2BPolo%2BAssn.&p%5B%5D=facets.brand%255B%255D%3DWROGN";
                driver.Navigate().GoToUrl(flipkartUrl);
                var flipkartImages = driver.FindElements(By.XPath("//img[@class='_3togXc']"));

                for (int i = 0; i < 10; i++)
                {
                    var links = flipkartImages[i].GetAttribute("src");
                    flipkartList.Add(links);

                }

                var amazonUrl = "https://www.amazon.in/s?bbn=1968076031&rh=n%3A1968076031%2Cp_n_style_browse-bin%3A1975054031&pf_rd_i=1968076031&pf_rd_m=A1VBAL9TL5WCBF&pf_rd_p=94b53eea-c579-4ce5-94b1-50ab1b812dd6&pf_rd_r=E8SDMMJF57RVAP7FAY6W&pf_rd_s=merchandised-search-7&ref=QABillboard_en-IN";
                driver.Navigate().GoToUrl(amazonUrl);
                var amazonImages = driver.FindElements(By.XPath("//img"));

                for (int i = 5; i < 15; i++)
                {
                    var links = amazonImages[i].GetAttribute("src");
                    amazonList.Add(links);

                }

                var koovsUrl = "https://www.koovs.com/men/jeans/";
                driver.Navigate().GoToUrl(koovsUrl);
                var koovsImages = driver.FindElements(By.XPath("//img[@class='prodImg']"));

                for (int i = 0; i < 20; i = i + 2)
                {
                    var links = koovsImages[i].GetAttribute("src");
                    koovsList.Add(links);

                }


                int j = 0;
                int flipkartRank = 1;
                foreach (var item in flipkartList)
                {
                    product[j].imageLink = item;
                    product[j].internalRank = flipkartRank;
                    product[j].websiteTraffic = 122;
                    product[j].website = "Flipkart";
                    j++;
                    flipkartRank++;
                }
                int amazonRank = 1;
                foreach (var item in amazonList)
                {
                    product[j].imageLink = item;
                    product[j].internalRank = amazonRank;
                    product[j].websiteTraffic = 111;
                    product[j].website = "Amazon";
                    j++;
                    amazonRank++;
                }
                int koovsRank = 1;
                foreach (var item in koovsList)
                {
                    product[j].imageLink = item;
                    product[j].internalRank = koovsRank;
                    product[j].websiteTraffic = 47143;
                    product[j].website = "Koovs";
                    j++;
                    koovsRank++;
                }
            }





            UpcomingProductValue[] designs = new UpcomingProductValue[40];
            for (int i = 0; i < designs.Length; i++)
            {
                designs[i] = new UpcomingProductValue();
            }
            int k = 0;

            var instagramUrl = "https://www.instagram.com/stealherstyle/";
            driver.Navigate().GoToUrl(instagramUrl);
            var instaImages = driver.FindElements(By.XPath("//img[@class='FFVAD']"));
            List<String> instaList = new List<string>();

            for (int i = 0; i < 10; i++)
            {
                var links = instaImages[i].GetAttribute("src");
                instaList.Add(links);

            }

            foreach (var item in instaList)
            {
                designs[k].imageLink = item;
                designs[k].numberOfLikes = 1;
                designs[k].numberOfComments = 1;
                k++;
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


            ViewBag.Products = product;
            ViewBag.Designs = designs;
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