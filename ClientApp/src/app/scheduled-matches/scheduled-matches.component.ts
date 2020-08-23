import { Component, OnInit, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-scheduled-matches',
  templateUrl: './scheduled-matches.component.html',
  styleUrls: ['./scheduled-matches.component.css']
})
export class ScheduledMatchesComponent implements OnInit {
  public matches: Matches[] = [];
  public leagues: League[] = [];
  today: number = Date.now();
  public showLoader: boolean;

  totalMatches = 0;

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.showLoader = true;

    http.get<League[]>(baseUrl + 'api/SampleData/GetLeagueMatches').subscribe(result => {
      this.leagues = result;
      console.log("--- Scheduled matches ---");
      console.log(this.leagues);
      console.log(this.leagues = groupBy(this.leagues, 'videogameName'));
      console.log("--- End Scheduled matches ---");
      for (var i = 0; i < Object.keys(this.leagues).length; i++)
        countMatches(this.leagues);
      this.showLoader = false;
    }, error => console.error(error));

    var countMatches = function (array) {
      for (var i in array) {
        var counter = 0;
        for (var j in array[i]) {
          if (array[i][j].matches !== undefined)
            counter += Object.keys(array[i][j].matches).length;
        }
        array[i]["totalMatches"] = counter;
      }
    }

    var groupBy = function (xs, key) {
      return xs.reduce(function (rv, x) {
        (rv[x[key]] = rv[x[key]] || []).push(x);
        return rv;
      }, {});
    };
  }
  ngOnInit(): void {
  }
}

interface League {
  id: number;
  imgUrl: string;
  name: string;
  slug: string;
  videogame: object;
  matches: object[];
  modified_date: Date;
  videogameName: string;
  totalMatches: number;
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

