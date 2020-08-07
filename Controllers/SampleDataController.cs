using EnumsNET;
using esport.Models;
using esport.Models.Enums;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http.Headers;

namespace esport.Controllers
{
    [Route("api/[controller]")]
    public class SampleDataController : Controller
    {
        private static string _token = "56ItzsN1iV0bXtXi6s3lT5sM6ejvfxQctMSxCfybyQl1feUUjZY";


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
                        Id = (int)match["league"]["id"],
                        ImgUrl = (string)match["league"]["image_url"],
                        Name = (string)match["league"]["name"],
                        Slug = (string)match["league"]["slug"],
                        Url = (string)""
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
                        streamUrl = (string)match["live_embed_url"],
                        linkUrl = (string)match["live_url"],
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
            return BadRequest();

        }

        [HttpGet("[action]")]
        public IActionResult GetLeagueMatches()
        {
            var leagues = GetLeagues();
            var matches = GetMatches();
            if (leagues.Any() && matches.Any())
            {
                List<League> leagueList = new List<League>();
                foreach (var league in leagues)
                {
                    leagueList.Add(new League
                    {
                        Id = league.Id,
                        ImgUrl = league.ImgUrl ?? "https://via.placeholder.com/35X21",
                        Name = league.Name,
                        Slug = league.Slug,
                        Url = league.Url,
                        Videogame = league.Videogame,
                        Matches = matches.Where(x => x.League.Id == league.Id).ToList()
                    });
                }
                //foreach (var league in leagueList.GroupBy(x => x.Videogame.Id).Select(group => new { Metric = group.Key, Count = group.Count() }).OrderBy(x => x.Metric)) {
                //    Console.WriteLine("{0} {1}", league.Metric, league.Count);
                //}
                leagueList = leagueList.Where(x => x.Matches.Any()).ToList();
                return Ok(leagueList);
            }
            return BadRequest();
        }
        private List<League> GetLeagues()
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
                        Id = (int)item["id"],
                        ImgUrl = (string)item["image_url"],
                        Name = (string)item["name"],
                        Slug = (string)item["videogame"]["slug"],
                        Url = (string)item["url"],
                        Videogame = new Game
                        {
                            Id = (int)item["videogame"]["id"],
                            Name = (string)item["videogame"]["name"],
                            Slug = (string)item["videogame"]["slug"],
                        }
                    };
                    leagues.Add(league);
                }
                return leagues;

            }
            System.Diagnostics.Debug.WriteLine("Connection not possible");
            return null;

        }

        public List<UpcomingMatches> GetMatches()
        {
            var client = new RestClient($"https://api.pandascore.co//matches/upcoming?token=" + _token + "&filter[detailed_stats]=true&per_page=100&range[begin_at]=" + DateTime.Now + "," + DateTime.Now.AddMonths(1));
            var request = new RestRequest(Method.GET);
            IRestResponse response = client.Execute(request);
            System.Diagnostics.Debug.WriteLine(response);
            if (response.IsSuccessful)
            {
                var content = JsonConvert.DeserializeObject<JToken>(response.Content);
                List<UpcomingMatches> upcomingGames = new List<UpcomingMatches>();

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
                        Id = (int)item["league"]["id"],
                        ImgUrl = (string)item["league"]["image_url"],
                        Name = (string)item["league"]["name"],
                        Slug = (string)item["videogame"]["slug"],
                        Url = (string)item["league"]["url"],
                        Videogame = new Game
                        {
                            Id = (int)item["videogame"]["id"],
                            Name = (string)item["videogame"]["name"],
                            Slug = (string)item["videogame"]["slug"],
                        }
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
                    var upcomingGame = new UpcomingMatches
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

                return upcomingGames;

            }
            System.Diagnostics.Debug.WriteLine("Connection not possible");
            return null;
        }
        [HttpGet("[action]")]
        public List<Team> GetOpponents(int id)
        {
            var client = new RestClient($"https://api.pandascore.co/matches/{id}/opponents");            
            var request = new RestRequest(Method.GET);
            request.AddParameter("Authorization", string.Format("Bearer " + _token),
            ParameterType.HttpHeader);
            IRestResponse response = client.Execute(request);
            System.Diagnostics.Debug.WriteLine(response);
            if (response.IsSuccessful)
            {
                var content = JsonConvert.DeserializeObject<JToken>(response.Content);
                List<Team> opponents = new List<Team>();
                foreach (var item in content["opponents"])
                {
                    var team = new Team()
                    {
                        acronym = (string)item["acronym"],
                        imgUrl = (string)item["image_url"],
                        name = (string)item["name"],
                        CurrentVideogame = (string)item["current_videogame"]["name"],
                        Players = new List<Player>()

                    };
                    List<Player> players = new List<Player>();
                    foreach (var player in item["players"])
                    {
                        players.Add(new Player()
                        {
                            BirthYear = (string)player["birth_year"],
                            Birthday = (DateTime?)player["birthday"],
                            Name = (string)player["name"],
                            HomeTown = (string)player["hometown"],
                            Id = (int)player["id"],
                            ImgUrl = (string)player["image_url"] ?? "/assets/profile-picture.png",
                            FirstName = (string)player["first_name"],
                            LastName = (string)player["last_name"],
                            Nationality = (string)player["nationality"],
                            Role = (string)player["role"],
                            Slug = (string)player["slug"]
                        });
                    }
                    team.Players.AddRange(players);
                    opponents.Add(team);
                }
                return opponents;
            }
            System.Diagnostics.Debug.WriteLine("Connection not possible");
            return null;
        }
    }
}
