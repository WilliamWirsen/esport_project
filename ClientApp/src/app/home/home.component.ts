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

  }
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


