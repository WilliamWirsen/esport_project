import { Component, Inject } from '@angular/core';
import * as $ from 'jquery';
import { HttpClient } from '@angular/common/http';


@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})

export class HomeComponent {
  public matches: Matches[];
  public leagues: Leagues[];
  public games: Games[];
  public liveGames: LiveGames[];
  today: number = Date.now();

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    http.get<Matches[]>(baseUrl + 'api/SampleData/GetMatches').subscribe(result => {
      this.matches = result;
      console.log(this.matches);
    }, error => console.error(error));

    http.get<Leagues[]>(baseUrl + 'api/SampleData/GetLeagues').subscribe(result => {
      this.leagues = result;
      console.log(this.leagues);
    }, error => console.error(error));

    http.get<LiveGames[]>(baseUrl + 'api/SampleData/GetLiveMatches').subscribe(result => {
      this.liveGames = result;
      console.log(this.liveGames);
    }, error => console.error(error));

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
      }
    ]
  }
  ngOnInit() {
    $(document).ready(function () {

    });
  }
}

interface LiveGames {
  id: number;
  startDate: string;
  name: string;  
  is_active: boolean;
  match_type: string;
  stream_url: string;
  draw: false;
  forfeit: boolean;
  opponentOne: object;
  opponentTwo: object;
  league: object;
  opponentOneResult: number;
  opponentTwoResult: number;
  season: string;
  numberOfGames: number;
  status: string;
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


