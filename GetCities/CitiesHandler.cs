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

        public string GetCountriesLinks()
        {
            string result = "Nothing yet";

            string htmlCodeUnparsed;

            WebClient client = new WebClient();
            client.Encoding = System.Text.Encoding.UTF8;
            htmlCodeUnparsed = client.DownloadString("http://www.gismeteo.ua/catalog/");

            string[] htmlCodeWrapDivider = new string[2];

            htmlCodeWrapDivider = htmlCodeUnparsed.Split(
                        new[] { "<div class=\"countries wrap\">" },
                        StringSplitOptions.None);

            htmlCodeWrapDivider[0] = String.Empty;


            string pattern = "<a href=\"(.*?)\">";
            MatchCollection matches = Regex.Matches(htmlCodeWrapDivider[1], pattern);
            Console.WriteLine("Matches found: {0}", matches.Count);


            if (matches.Count > 0)
            {
                string[] htmlCodeCountry = new string[matches.Count];

                for (int i = 0; i < matches.Count; i++)
                {
                    htmlCodeCountry[i] = client.DownloadString("http://www.gismeteo.ua" + matches[i].Groups[1]);
                }

                result = String.Format("Got URLs for {0} countries", matches.Count);
            }
            else
            {
                result = "Failed to get countries' URLs";
            }

            return result;
        }


    }
}