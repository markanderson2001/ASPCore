using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace TheWorld.Services
{
    public class GeoCoordsService
    {
        private IConfigurationRoot _config;
        private ILogger<GeoCoordsService> _logger;

        public  GeoCoordsService(ILogger<GeoCoordsService> logger,
            IConfigurationRoot config) //at minimum we need a ilogger for geocordservice, also add configuration as a dependency to this class
            {
                _logger = logger;
                _config = config;
            }

        //New method with name of where we want to get coords for
        public async Task<GeoCoordsResult> GetCoordsAsync(string name)//use async and use a task to return a new type of structure -and generate in new file
                                                        //move to Services
        {
            //crete an instance of geocoord result
            var result = new GeoCoordsResult() //put some default vals
            {
                //defaults
                Success = false,
                Message = "Failed to get coordinates"
            };
            //Get a service address of an URL that will get our coordinates for us, to do this we need
            //.. a service that will convert a name of a place, or address into a set of Coords
            //..one such service is Bing Maps - https://www.bingmapsportal.com/

            var apiKey =_config["Keys:BingKey"];//we want to store this apiKey in configuration (Config.jason)
            var encodedName = WebUtility.UrlEncode(name);
            var url = $"http://dev.virtualearth.net/REST/v1/Locations?q={encodedName}&key={apiKey}";//construct actual url -with two paramets - anme and apikey

            var client = new HttpClient();

            var json = await client.GetStringAsync(url);


// Read out the results
// Fragile, might need to change if the Bing API changes
            var results = JObject.Parse(json);
            var resources = results["resourceSets"][0]["resources"];
            if (!results["resourceSets"][0]["resources"].HasValues)
            {
                result.Message = $"Could not find '{name}' as a location";
            }
            else
            {
                var confidence = (string)resources[0]["confidence"]; //test
                if (confidence != "High")
                {
                    result.Message = $"Could not find a confident match for '{name}' as a location";
                }
                else
                {
                    var coords = resources[0]["geocodePoints"][0]["coordinates"];
                    //cast to double as indexer is untyped
                    result.Latitude = (double)coords[0];
                    result.Longitude = (double)coords[1];
                    result.Success = true;
                    result.Message = "Suiccess";
                }
            }
            return result;            
        }
    }
}
