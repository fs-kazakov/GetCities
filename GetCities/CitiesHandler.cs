using System;
using System.Collections.Generic;
using System.Web;
using System.Net;
using System.Data;
using System.Text.RegularExpressions;

namespace GetCities.Classes
{
    public class CitiesHandler
    {
        public CitiesHandler()
        {


        }


        public string getCities()
        {
            string result = getCountriesHtml(GetCountriesLinks())[0];

            return result;
        }

        public List<string> GetCountriesLinks()
        {
            string htmlCode = getHTML("http://www.gismeteo.ua/catalog/");

            string pattern = "<a href=\"(.*?)\">";
            MatchCollection matches = Regex.Matches(htmlCode, pattern);

            List<string> countryLinks = new List<string>();
            string tempMatch;

            for (int i = 0; i < matches.Count; i++)
            {
                tempMatch = matches[i].Groups[1].ToString();

                if (tempMatch.Contains("/catalog/") && tempMatch != "/catalog/" && tempMatch != "/map/catalog/")
                {
                    countryLinks.Add(tempMatch);
                }
            }

            return countryLinks;
        }


        public List<String> getCountriesHtml(List<string> links)
        {
            List<String> countryHTMLs = new List<String>();

            for (int i = 0; i < 5; i++)
            {
                countryHTMLs.Add(
                    getHTML("http://www.gismeteo.ua" + links[i]));
            }

            return countryHTMLs;
        }


        //UTILS
        public string getHTML (string link)
        {
            string htmlCodeUnparsed;

            WebClient client = new WebClient();
            client.Encoding = System.Text.Encoding.UTF8;
            htmlCodeUnparsed = client.DownloadString(link);

            return htmlCodeUnparsed;
        }

    }
}