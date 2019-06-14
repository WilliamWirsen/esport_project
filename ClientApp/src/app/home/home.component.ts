import { Component, Inject } from '@angular/core';
import * as $ from 'jquery';
import { HttpClient } from '@angular/common/http';


@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})

export class HomeComponent {
  public matches: Matches[];

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    http.get<Matches[]>(baseUrl + 'api/SampleData/GetMatches').subscribe(result => {
      this.matches = result;
      console.log(this.matches);
    }, error => console.error(error));
  }
  public ngOnInit() {
    $(document).ready(function () {
      //GetUpcomingGames();
      console.log(this.matches);
    })
  }
}
function GetUpcomingGames() {
  $.ajax({
    url: '/api/SampleData/GetMatches',
    type: "GET",
    contentType: "application/json;charset=utf-8",
    dataType: "json",
    success: function (result) {
      this.matches = result;
      console.log(this.matches);
    },
    error: function (error) {
      console.log(JSON.stringify(error));
    }
  });
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


