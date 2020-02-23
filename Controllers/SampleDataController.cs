using esport.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System.Collections.Generic;
using System.Linq;

namespace esport.Controllers
{
    [Route("api/[controller]")]
    public class SampleDataController : Controller
    {
        private static string _token = "56ItzsN1iV0bXtXi6s3lT5sM6ejvfxQctMSxCfybyQl1feUUjZY";

        [HttpGet("[action]")]
        public IActionResult GetMatches()
        {
            var client = new RestClient($"https://api.pandascore.co//matches/upcoming?token=" + _token);
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
                        StartDate = (string)item["begin_at"],
                        Draw = (bool)item["draw"],
                        MatchType = (string)item["match_type"],
                        Name = (string)item["name"],
                        NumberOfGames = (int)item["number_of_games"],
                        Tournament = (string)item["tournament"]["name"],
                        League = league,
                        OpponentOne = opponentOne,
                        OpponentTwo = opponentTwo,
                        Series = series
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
            var client = new RestClient($"https://api.pandascore.co//leagues?per_page=100&token=" + _token);
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

        [HttpGet("[action]")]
        public IActionResult GetLiveMatches()
        {
            var client = new RestClient($"https://api.pandascore.co//lives?per_page=3&token=" + _token);
            var request = new RestRequest(Method.GET);
            IRestResponse response = client.Execute(request);

            if (response.IsSuccessful)
            {
                var content = JsonConvert.DeserializeObject<JToken>(response.Content);
                List<LiveGame> liveGames = new List<LiveGame>();
                foreach (var item in content)
                {
                    var match = item["match"];
                    var league = new League
                    {
                        ID = (int)match["league"]["id"],
                        imgUrl = (string)match["league"]["image_url"],
                        name = (string)match["league"]["name"],
                        slug = (string)match["league"]["slug"],
                        url = (string)"",
                        videogame = (string)""
                    };

                    var opponentOne = new Team
                    {
                        ID = (int)match["opponents"][0]["opponent"]["id"],
                        acronym = (string)match["opponents"][0]["opponent"]["acronym"],
                        name = (string)match["opponents"][0]["opponent"]["name"],
                        imgUrl = (string)match["opponents"][0]["opponent"]["image_url"]
                    };
                    var opponentTwo = new Team
                    {
                        ID = (int)match["opponents"][1]["opponent"]["id"],
                        acronym = (string)match["opponents"][1]["opponent"]["acronym"],
                        name = (string)match["opponents"][1]["opponent"]["name"],
                        imgUrl = (string)match["opponents"][1]["opponent"]["image_url"]
                    };

                    var live = new LiveGame
                    {
                        id = (int)item["event"]["id"],
                        startDate = (string)match["begin_at"],
                        isActive = (bool)match["live"]["supported"],
                        matchType = (string)match["match_type"],
                        streamUrl = (string)match["stream_url"],
                        draw = (bool)match["draw"],
                        forfeit = (bool)match["forfeit"],
                        opponentOne = opponentOne,
                        opponentTwo = opponentTwo,
                        opponentOneResult = (int)match["results"][0]["score"],
                        opponentTwoResult = (int)match["results"][1]["score"],
                        numberOfGames = (int)match["number_of_games"],
                        season = (string)match["tournament"]["name"],
                        name = (string)match["league"]["name"],
                        league = league,
                        status = (string)match["status"]
                    };

                    liveGames.Add(live);
                }
                return Ok(liveGames);

            }
            System.Diagnostics.Debug.WriteLine("Connection not possible");
            return null;

        }

    }
}
