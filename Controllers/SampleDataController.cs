using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RestSharp;
using esport.Models;
using Newtonsoft.Json.Linq;

namespace esport.Controllers
{
    [Route("api/[controller]")]
    public class SampleDataController : Controller
    {
        static string token = "56ItzsN1iV0bXtXi6s3lT5sM6ejvfxQctMSxCfybyQl1feUUjZY";

        private static string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        [HttpGet("[action]")]
        public IEnumerable<WeatherForecast> WeatherForecasts()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                DateFormatted = DateTime.Now.AddDays(index).ToString("d"),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            });
        }

        [HttpGet("[action]")]
        public IActionResult GetMatches()
        {
            var client = new RestClient($"https://api.pandascore.co//matches/upcoming?token=" + token);
            var request = new RestRequest(Method.GET);
            IRestResponse response = client.Execute(request);
            System.Diagnostics.Debug.WriteLine(response);
            if (response.IsSuccessful)
            {
                var content = JsonConvert.DeserializeObject<JToken>(response.Content);
                List<UpcomingGame> upcomingGames = new List<UpcomingGame>();

                foreach (var item in content)
                {
                    if (!item["opponents"].HasValues)
                    {
                        System.Diagnostics.Debug.WriteLine("Array is empty");
                        continue;
                    }
                    else if (item["opponents"].Count<object>() <= 1)
                        continue;
                       
                    //System.Diagnostics.Debug.WriteLine(item["opponents"].Count<object>());
                    var league = new League
                    {
                        ID = (int)item["league"]["id"],
                        imgUrl = (string)item["league"]["image_url"],
                        name = (string)item["league"]["name"],
                        slug = (string)item["videogame"]["slug"],
                        url = (string)item["league"]["url"],
                        videogame = (string)item["videogame"]["name"]
                    };
                    
                    var opponentOne = new Team
                    {
                        ID = (int)item["opponents"][0]["opponent"]["id"],
                        acronym = (string)item["opponents"][0]["opponent"]["acronym"],
                        name = (string)item["opponents"][0]["opponent"]["name"],
                        imgUrl = (string)item["opponents"][0]["opponent"]["image_url"]
                    };
                    var opponentTwo = new Team
                    {
                        ID = (int)item["opponents"][1]["opponent"]["id"],
                        acronym = (string)item["opponents"][1]["opponent"]["acronym"],
                        name = (string)item["opponents"][1]["opponent"]["name"],
                        imgUrl = (string)item["opponents"][1]["opponent"]["image_url"]
                    };
                    var series = new Series
                    {
                        id = (int)item["serie"]["id"],
                        name = (string)item["serie"]["name"],
                        prizepool = (string)item["serie"]["prizepool"],
                        season = (string)item["serie"]["season"],
                        year = (int)item["serie"]["year"],
                        status = (string)item["serie"]["status"]
                    };
                    var upcomingGame = new UpcomingGame
                    {
                        ID = (int)item["id"],
                        startDate = (string)item["begin_at"],
                        draw = (bool)item["draw"],
                        matchType = (string)item["match_type"],
                        name = (string)item["name"],
                        numberOfGames = (int)item["number_of_games"],
                        tournament = (string)item["tournament"]["name"],
                        league = league,
                        opponentOne = opponentOne,
                        opponentTwo = opponentTwo,
                        series = series
                    };
                    upcomingGames.Add(upcomingGame);
                    System.Diagnostics.Debug.WriteLine(upcomingGame);
                }

                return Ok(upcomingGames);

            }
            System.Diagnostics.Debug.WriteLine("Connection not possible");
            return null;

        }

        [HttpGet("[action]")]
        public IActionResult GetLeagues()
        {
            var client = new RestClient($"https://api.pandascore.co//leagues?per_page=100&token=" + token);
            var request = new RestRequest(Method.GET);
            IRestResponse response = client.Execute(request);

            if (response.IsSuccessful)
            {
                var content = JsonConvert.DeserializeObject<JToken>(response.Content);
                List<League> leagues = new List<League>();
                foreach (var item in content)
                {
                    var league = new League
                    {
                        ID = (int)item["id"],
                        imgUrl = (string)item["image_url"],
                        name = (string)item["name"],
                        slug = (string)item["videogame"]["slug"],
                        url = (string)item["url"],
                        videogame = (string)item["videogame"]["name"]
                    };
                    leagues.Add(league);
                }
                return Ok(leagues);

            }
            System.Diagnostics.Debug.WriteLine("Connection not possible");
            return null;

        }

        public class UpcomingGame
        {
            public int ID { get; set; }
            public string startDate { get; set; }
            public bool draw { get; set; }
            public string matchType { get; set; }
            public string name { get; set; }
            public int numberOfGames { get; set; }
            public string tournament { get; set; }
            public League league { get; set; }
            public Team opponentOne { get; set; }
            public Team opponentTwo { get; set; }
            public Series series { get; set; }
        }

        public class WeatherForecast
        {
            public string DateFormatted { get; set; }
            public int TemperatureC { get; set; }
            public string Summary { get; set; }

            public int TemperatureF
            {
                get
                {
                    return 32 + (int)(TemperatureC / 0.5556);
                }
            }
        }
    }
}
