import { Component, Inject, OnInit, Input } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { DomSanitizer } from '@angular/platform-browser';
import { ActivatedRoute } from '@angular/router';
import { LiveComponent } from '../live/live.component';

@Component({
  selector: 'app-home',
  templateUrl: "./home.component.html",
  styleUrls: ['./home.component.css'],
})

export class HomeComponent implements OnInit {
  @Input() live: LiveComponent;

  public matches: Matches[] = [];
  public leagues: Leagues[] = [];
  public games: Games[] = [];
  today: number = Date.now();
  public showLoader: boolean;

  overwatch = [];
  rocketLeague = [];
  cod = [];
  dota2 = [];
  csgo = [];
  siege = [];
  lol = [];

  overwatchCounter = 0;
  rocketLeagueCounter = 0;
  codCounter = 0;
  dotaCounter = 0;
  csgoCounter = 0;
  siegeCounter = 0;
  lolCounter = 0;



  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string, private sanitizer: DomSanitizer, private route: ActivatedRoute) {
    this.showLoader = true;
    //http.get<Matches[]>(baseUrl + 'api/SampleData/GetMatches').subscribe(result => {
    //  this.matches = result;
    //  console.log(this.matches);
    //}, error => console.error(error));    
    http.get<Leagues[]>(baseUrl + 'api/SampleData/GetLeagueMatches').subscribe(result => {
      this.leagues = result;
      console.log("--- Leagues ---");
      console.log(this.leagues);
      console.log("--- End Leagues ---");
      this.overwatch = this.leagues.filter(item => item.slug == "ow");
      this.rocketLeague = this.leagues.filter(item => item.slug == "rl");
      this.cod = this.leagues.filter(item => item.slug == "cod-mw");
      this.dota2 = this.leagues.filter(item => item.slug == "dota-2");
      this.csgo = this.leagues.filter(item => item.slug == "cs-go");
      this.siege = this.leagues.filter(item => item.slug == "r6-siege");
      this.lol = this.leagues.filter(item => item.slug == "league-of-legends");

      this.overwatchCounter = countMatches(this.overwatch, this.overwatchCounter);
      this.rocketLeagueCounter = countMatches(this.rocketLeague, this.rocketLeagueCounter);
      this.csgoCounter = countMatches(this.csgo, this.csgoCounter);
      this.codCounter = countMatches(this.cod, this.codCounter);
      this.dotaCounter = countMatches(this.dota2, this.dotaCounter);
      this.siegeCounter = countMatches(this.siege, this.siegeCounter);
      this.lolCounter = countMatches(this.lol, this.lolCounter);

      this.showLoader = false;
    }, error => console.error(error));

    var countMatches = function (array, counter) {
      for (var i in array) {
        counter += Object.keys(array[i].matches).length;
      }
      return counter;
    }

    this.games = [
      {
        id: "overwatch",
        name: "Overwatch",
        slug: "Overwatch",
        img: "../../assets/overwatch_icon.png"
      },
      {
        id: "league-of-legends",
        name: "League of Legends",
        slug: "LoL",
        img: "../../assets/league-of-legends.png"
      },
      {
        id: "dota-2",
        name: "Dota 2",
        slug: "Dota 2",
        img: "../../assets/dota2.png"
      },
      {
        id: "CS_GO",
        name: "Counter Strike: Global Offensive",
        slug: "CS:GO",
        img: "../../assets/csgo-icon-6.png"
      },
      {
        id: "Rocket_League",
        name: "Rocket League",
        slug: "rl",
        img: "../../assets/rl.png"
      },
      {
        id: "r6-siege",
        name: "Rainbow Six Siege",
        slug: "r6-siege",
        img: "../../assets/r6-siege.png"
      }
    ]

  }
  ngOnInit(): void {
  }
}



interface Games {
  id: string;
  name: string;
  slug: string;
  img: string;
}
interface Leagues {
  id: number;
  imgUrl: string;
  name: string;
  slug: string;
  videogame: string;
  matches: object;
}

interface Matches {
  id: number;
  startDate: string;
  draw: boolean;
  matchType: string;
  name: string;
  numberOfGames: number;
  tournament: string;
  league: object;
  opponentOne: object;
  opponentTwo: object;
}


