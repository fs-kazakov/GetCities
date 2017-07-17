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
        public List<String> partialURLsToPagesWithCities;
        public List<CityInfo> completeCities;


        public CitiesHandler()
        {
            partialURLsToPagesWithCities = new List<String>();
            completeCities = new List<CityInfo>();
        }

        public List<CityInfo> getCities(string url)
        {
            List<string> URLs = getURLs(url);

            checkWeatherURLinHTML(URLs);

            getCitiesURLsAndNames(partialURLsToPagesWithCities);

            return completeCities;
        }

        public void checkWeatherURLinHTML(List<string> URLs)
        {
            foreach (string URL in URLs)
            {
                if (getHTML("http://www.gismeteo.ua" + URL).Contains("<a href=\"/weather"))
                {
                    partialURLsToPagesWithCities.Add(URL);
                }
                else
                {
                    checkWeatherURLinHTML(getURLs("http://www.gismeteo.ua" + URL));
                }
            }


        }


        public List<String> getURLs(string sourceURL)
        {
            string pattern = "<a href=\"(.*?)\">";
            MatchCollection matches = Regex.Matches(getHTML(sourceURL), pattern);

            List<string> URLs = new List<string>();
            string tempMatch;

            for (int i = 0; i < matches.Count; i++)
            {
                tempMatch = matches[i].Groups[1].ToString();

                if (tempMatch.Contains("/catalog/") && tempMatch != "/catalog/" && tempMatch != "/map/catalog/")
                {
                    URLs.Add(tempMatch);
                }
            }


            return URLs;

        }

        public void getCitiesURLsAndNames(List<String> partialURLs)
        {
            List <string> cityUrls = new List<String>();
            List<string> cityNames = new List<String>();


            foreach (string pURL in partialURLs)
            {
                cityUrls = getCityUrls("http://www.gismeteo.ua" + pURL);
                cityNames = getNames("http://www.gismeteo.ua" + pURL);

                for (int i = 0; i < cityUrls.Count; i++)
                {
                    CityInfo newCity = new CityInfo();

                    newCity.cityUrl = cityUrls[i];
                    newCity.cityName = cityNames[i];


                    completeCities.Add(newCity);
                }
            }


        }

        public List<String> getCityUrls(string completeURL)
        {
            string pattern = "<a href=\"(.*?)\" >";
            MatchCollection matches = Regex.Matches(getHTML(completeURL), pattern);

            List<string> partialURLs = new List<string>();
            string tempMatch;


            for (int i = 0; i < matches.Count; i++)
            {
                tempMatch = matches[i].Groups[1].ToString();

                if (tempMatch.Contains("/weather-"))
                {
                    partialURLs.Add(tempMatch);
                }
            }


            return partialURLs;

        }

        public List<String> getNames(string completeURL)
        {
            List<string> newCityNames = new List<string>();

            string pattern = "<a href=\"/weather.*?>(.*?)</a>";

            MatchCollection matches = Regex.Matches(getHTML(completeURL), pattern);

            string tempMatch;

            for (int i = 0; i < matches.Count; i++)
            {
                tempMatch = matches[i].Groups[1].ToString();
                newCityNames.Add(tempMatch);
            }

            return newCityNames;
        }


        //UTILS
        public string getHTML(string completeURL)
        {
            string htmlCodeUnparsed;

            WebClient client = new WebClient();
            client.Encoding = System.Text.Encoding.UTF8;
            htmlCodeUnparsed = client.DownloadString(completeURL);

            return htmlCodeUnparsed;
        }


    }
}