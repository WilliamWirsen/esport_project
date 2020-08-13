using EnumsNET;
using esport.Models;
using esport.Models.Enums;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using RestSharp.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http.Headers;

namespace esport.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
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
                        Id = (int)match["opponents"][0]["opponent"]["id"],
                        Acronym = (string)match["opponents"][0]["opponent"]["acronym"],
                        Name = (string)match["opponents"][0]["opponent"]["name"],
                        ImgUrl = (string)match["opponents"][0]["opponent"]["image_url"]
                    };
                    var opponentTwo = new Team
                    {
                        Id = (int)match["opponents"][1]["opponent"]["id"],
                        Acronym = (string)match["opponents"][1]["opponent"]["acronym"],
                        Name = (string)match["opponents"][1]["opponent"]["name"],
                        ImgUrl = (string)match["opponents"][1]["opponent"]["image_url"]
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
                        Id = (int)item["opponents"][0]["opponent"]["id"],
                        Acronym = (string)item["opponents"][0]["opponent"]["acronym"],
                        Name = (string)item["opponents"][0]["opponent"]["name"],
                        ImgUrl = (string)item["opponents"][0]["opponent"]["image_url"]
                    };
                    var opponentTwo = new Team
                    {
                        Id = (int)item["opponents"][1]["opponent"]["id"],
                        Acronym = (string)item["opponents"][1]["opponent"]["acronym"],
                        Name = (string)item["opponents"][1]["opponent"]["name"],
                        ImgUrl = (string)item["opponents"][1]["opponent"]["image_url"]
                    };
                    var series = new Series
                    {
                        Id = (int)item["serie"]["id"],
                        Name = (string)item["serie"]["name"],
                        Prizepool = (string)item["serie"]["prizepool"],
                        Season = (string)item["serie"]["season"],
                        Year = (int)item["serie"]["year"],
                        Description = (string)item["serie"]["description"],
                        FullName = (string)item["serie"]["full_name"],
                        Tier = (string)item["serie"]["tier"],
                        BeginDate = (DateTime)item["serie"]["begin_at"],
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
                        Acronym = (string)item["acronym"],
                        ImgUrl = (string)item["image_url"],
                        Name = (string)item["name"],
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

        [HttpGet("[action]")]
        public Match GetMatch(int id)
        {
            try
            {
                var client = new RestClient($"https://api.pandascore.co/matches/{id}");
                var request = new RestRequest(Method.GET);
                request.AddParameter("Authorization", string.Format("Bearer " + _token),
                ParameterType.HttpHeader);
                IRestResponse response = client.Execute(request);
                System.Diagnostics.Debug.WriteLine(response);
                if (response.IsSuccessful)
                {
                    var content = JsonConvert.DeserializeObject<JToken>(response.Content);
                    var match = new Match();

                    var opponents = content.SelectTokens("opponents[*]").Select(x => x.SelectToken("opponent"))
                    .Select(opponent => new Team()
                    {
                        Id = (int)opponent["id"],
                        Acronym = (string)opponent["acronym"],
                        ImgUrl = (string)opponent["image_url"],
                        Name = (string)opponent["name"],
                        Location = (string)opponent["location"],
                        ModifiedDate = (DateTime)opponent["modified_at"]
                    })
                    .ToList();

                    var games = content.SelectTokens("games[*]").Select(game => new Game()
                    {
                        BeginDate = (DateTime?)game["begin_at"],
                        Complete = (bool)game["complete"],
                        EndDate = (DateTime?)game["end_at"],
                        Finished = (bool)game["finished"],
                        Forfeight = (bool)game["forfeit"],
                        Id = (int)game["id"],
                        MatchId = (int)game["match_id"],
                        Name = (string)game["position"],
                        Status = game["status"].ToObject<MatchStatusType>(),
                        VideoUrl = (string)game["video_url"],
                        Winner = new Winner()
                        {
                            WinnerId = (int?)game["winner"]["id"],
                            WinnerType = game["winner"]["type"].ToObject<WinnerType?>()
                        }

                    }).ToList();

                    match.BeginAt = DateTime.Parse((string)content["begin_at"]);
                    match.Draw = (bool)content["draw"];
                    //match.EndAt = (DateTime)item["end_at"];
                    match.Forfeit = (bool)content["forfeit"];
                    match.Id = (int)content["id"];
                    match.Games = games;
                    match.League = new League()
                    {
                        Id = (int)content["league"]["id"],
                        ImgUrl = (string)content["league"]["image_url"],
                        ModifiedDate = (DateTime)content["league"]["modified_at"],
                        Name = (string)content["league"]["name"],
                        Slug = (string)content["league"]["slug"],
                        Url = (string)content["league"]["url"]
                    };
                    match.Live = new Live()
                    {
                        OpenDate = (DateTime?)content["live"]["opens_at"] ?? DateTime.MinValue,
                        Supported = (bool)content["live"]["supported"],
                        Url = (string)content["live"]["url"]
                    };
                    match.EmbeddedUrl = (string)content["live_embeded_url"];
                    match.MatchType = content["match_type"].ToObject<MatchType>();
                    match.ModifiedDate = (DateTime)content["modified_at"];
                    match.Name = (string)content["name"];
                    match.NumberOfGames = (int)content["number_of_games"];
                    match.OfficialStreamUrl = (string)content["official_stream_url"];
                    match.Opponent1 = opponents[0];
                    match.Opponent2 = opponents[1];
                    match.OriginalScheduleDate = (DateTime)content["original_scheduled_at"];
                    match.Rescheduled = (bool)content["rescheduled"];
                    match.Results = new List<Result>();
                    foreach (var result in content["results"])
                    {
                        match.Results.Add(new Result()
                        {
                            Score = (int)result["score"],
                            TeamId = (int)result["team_id"]
                        });
                    }
                    match.ScheduledDate = (DateTime)content["scheduled_at"];
                    match.Series = new Series()
                    {
                        BeginDate = (DateTime)content["serie"]["begin_at"],
                        Description = (string)content["serie"]["description"],
                        EndDate = (DateTime?)content["serie"]["end_at"] ?? DateTime.MinValue,
                        FullName = (string)content["serie"]["full_name"],
                        Id = (int)content["serie"]["id"],
                        LeagueId = (int)content["serie"]["league_id"],
                        ModifiedDate = (DateTime)content["serie"]["modified_at"],
                        Name = (string)content["serie"]["name"],
                        Season = (string)content["serie"]["season"],
                        Tier = (string)content["serie"]["tier"],
                        Year = (int)content["serie"]["year"],
                        Winner = new Winner()
                        {
                            WinnerId = (int?)content["serie"]["winner_id"],
                            WinnerType = content["serie"]["winner_type"].ToObject<WinnerType?>()
                        }
                    };
                    match.Slug = (string)content["slug"];
                    match.Status = content["status"].ToObject<MatchStatusType>();
                    match.Tournament = new Tournament()
                    {
                        BeginDate = (DateTime)content["tournament"]["begin_at"],
                        Id = (int)content["tournament"]["id"],
                        EndDate = (DateTime?)content["tournament"]["end_at"],
                        IsLiveSupported = (bool)content["tournament"]["live_supported"],
                        LeagueId = (int)content["tournament"]["league_id"],
                        ModifiedDate = (DateTime)content["tournament"]["modified_at"],
                        Name = (string)content["tournament"]["name"],
                        Prizepool = (string)content["tournament"]["prizepool"],
                        SeriesId = (int)content["tournament"]["serie_id"],
                        Slug = (string)content["tournament"]["slug"],
                        Winner = new Winner()
                        {
                            WinnerId = (int?)content["tournament"]["winner_id"],
                            WinnerType = content["tournament"]["winner_type"].ToObject<WinnerType?>()
                        }
                    };
                    return match;

                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                throw new Exception(ex.Message);
            }
            return null;
        }
    }
}
