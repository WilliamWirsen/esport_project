using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace esport.Models
{
    public class LeagueTableModel
    {
        public string LeagueCaption { get; set; }

        public IEnumerable<Match> Matches { get; set; }


        public LeagueTableModel GetMatches()
        {
            var client = new RestClient($"https://api.pandascore.co//ow/tournaments/upcoming?token=56ItzsN1iV0bXtXi6s3lT5sM6ejvfxQctMSxCfybyQl1feUUjZY");
            var request = new RestRequest(Method.GET);
            IRestResponse response = client.Execute(request);
            if (response.IsSuccessful)
            {
                var content = JsonConvert.DeserializeObject<JToken>(response.Content);

                //Get the league caption
                var leagueCaption = content["league"].Value<string>();

                var opponents = content.SelectTokens("opponent[*]")
                    .Select(opponent => new Team
                    {
                        ID = (int)opponent["id"],
                        acronym = (string)opponent["acronym"],
                        imgUrl = (string)opponent["image_url"],
                        name = (string)opponent["name"]
                    })
                    .ToList();

                var match = content.SelectTokens("[*]")
                    .Select(matches => new Match
                    {
                        ID = (int)matches["id"],
                        draw = (bool)matches["draw"],
                        forfeit = (bool)matches["forfeit"],
                        matchType = (string)matches["match_type"],
                        name = (string)matches["name"],
                        numberOfGames = (int)matches["number_of_games"],
                        opponent1 = opponents[0],
                        opponent2 = opponents[1],
                        winnerId = 0
                    })
                    .ToList();

                // Return the model to my caller.
                return new LeagueTableModel
                {
                    LeagueCaption = leagueCaption,
                    Matches = match
                };                
            }

            // TODO: Log error, throw exception or do other stuff for failed request here
            Console.WriteLine(response.Content);
            return null;
        }
    }
    
    
    /*
    public class Standings
    {
        public string TeamName { get; set; }
        public int Position { get; set; }
    }
    */
}
