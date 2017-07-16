using System;
using System.Collections.Generic;
using System.Web;
using System.Net;
using System.Data;
using System.Text.RegularExpressions;

namespace GetCities.Classes
{
    public class CityInfo
    {
        public string cityName;
        public string cityUrl;

        public CityInfo()
        {
            cityName = String.Empty;
            cityUrl = String.Empty;
        }
    }

    public class CitiesHandler
    {
        public List<String> htmlLinksToCityPages;
        public List<CityInfo> completeCities;


        public CitiesHandler()
        {
            htmlLinksToCityPages = new List<String>();
            completeCities = new List<CityInfo>();
        }

        public List<CityInfo> getCities()
        {
            List<string> links = getLinks("https://www.gismeteo.ua/catalog/ukraine/");

            processHTML(links);

            getLinksAndNames(htmlLinksToCityPages);

            return completeCities;
        }

        public void processHTML(List<string> links)
        {
            foreach (string link in links)
            {
                if (getHTML("http://www.gismeteo.ua" + link).Contains("<a href=\"/weather"))
                {
                    htmlLinksToCityPages.Add(link);
                }
                else
                {
                    processHTML(getLinks("http://www.gismeteo.ua" + link));
                }
            }


        }


        public List<String> getLinks(string linkToParse)
        {
            string pattern = "<a href=\"(.*?)\">";
            MatchCollection matches = Regex.Matches(getHTML(linkToParse), pattern);

            List<string> links = new List<string>();
            string tempMatch;

            for (int i = 0; i < matches.Count; i++)
            {
                tempMatch = matches[i].Groups[1].ToString();

                if (tempMatch.Contains("/catalog/") && tempMatch != "/catalog/" && tempMatch != "/map/catalog/")
                {
                    links.Add(tempMatch);
                }
            }


            return links;

        }

        public void getLinksAndNames(List<String> links)
        {
            List <string> cityUrls = new List<String>();
            List<string> cityNames = new List<String>();


            foreach (string link in links)
            {
                cityUrls = getCityUrls("http://www.gismeteo.ua" + link);
                cityNames = getNames("http://www.gismeteo.ua" + link);

                for (int i = 0; i < cityUrls.Count; i++)
                {
                    CityInfo newCity = new CityInfo();

                    newCity.cityUrl = cityUrls[i];
                    newCity.cityName = cityNames[i];

                    completeCities.Add(newCity);
                }
            }

        }

        public List<String> getCityUrls(string linkToParse)
        {
            string pattern = "<a href=\"(.*?)\" >";
            MatchCollection matches = Regex.Matches(getHTML2(linkToParse), pattern);

            List<string> links = new List<string>();
            string tempMatch;


            for (int i = 0; i < matches.Count; i++)
            {
                tempMatch = matches[i].Groups[1].ToString();

                if (tempMatch.Contains("/weather-"))
                {
                    links.Add(tempMatch);
                }
            }


            return links;

        }

        public List<String> getNames(string link)
        {
            List<string> newCityNames = new List<string>();

            string pattern = "<a href=\"/weather.*?>(.*?)</a>";

            MatchCollection matches = Regex.Matches(getHTML3(link), pattern);

            string tempMatch;

            for (int i = 0; i < matches.Count; i++)
            {
                tempMatch = matches[i].Groups[1].ToString();
                newCityNames.Add(tempMatch);
            }

            return newCityNames;
        }


        //UTILS
        public string getHTML(string link)
        {
            string htmlCodeUnparsed;

            WebClient client = new WebClient();
            client.Encoding = System.Text.Encoding.UTF8;
            htmlCodeUnparsed = client.DownloadString(link);

            return htmlCodeUnparsed;
        }

        public string getHTML2(string link)
        {
            string htmlCodeUnparsed;

            WebClient client = new WebClient();
            client.Encoding = System.Text.Encoding.UTF8;
            htmlCodeUnparsed = client.DownloadString(link);

            return htmlCodeUnparsed;
        }

        public string getHTML3(string link)
        {
            string htmlCodeUnparsed;

            WebClient client = new WebClient();
            client.Encoding = System.Text.Encoding.UTF8;
            htmlCodeUnparsed = client.DownloadString(link);

            return htmlCodeUnparsed;
        }

    }
}