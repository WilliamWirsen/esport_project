import { Component, OnDestroy, OnInit, Inject } from '@angular/core';
import { HttpClient, HttpParams, HttpHeaders } from '@angular/common/http';
import { ActivatedRoute } from '@angular/router';
import { groupBy } from 'rxjs/operators';

@Component({
  selector: 'match',
  templateUrl: './match.component.html',
  styleUrls: ['./match.component.css', '../app.component.css']
})

export class MatchComponent implements OnInit, OnDestroy {
  id: number;

  private sub: any;
  private _http: HttpClient;
  private _baseUrl: string;

  public teams: Team[] = [];
  public today: number = new Date().getFullYear();
  public hidden: boolean;
  public match: object;
  public numbers: number[] = [];
  public loadingTeamsSpinner: boolean;
  opponent1 = [];
  opponent2 = [];

  public isLoaded: boolean;

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string, private route: ActivatedRoute) {
    this._http = http;
    this._baseUrl = baseUrl;
    this.isLoaded = false;
    console.log(this.isLoaded);
    this.hidden = false;
    this.numbers = Array(5).fill(0).map((x, i) => i)
    this.loadingTeamsSpinner = true;
  }



  ngOnInit() {
    this.sub = this.route.params.subscribe(params => {
      this.id = +params['id']; // (+) converts string 'id' to a number
      console.log(this.id);

      let headers = new HttpHeaders();
      headers.append('Content-Type', 'application/json');
      headers.append('matchId', this.id.toString());
      let para = new HttpParams().set("id", this.id.toString());
      this._http.get<Team[]>(this._baseUrl + "api/SampleData/GetOpponents", { headers: headers, params: para }).subscribe(result => {
        this.teams = result;
        console.log(this.teams);
        this.loadingTeamsSpinner = false;

      });
      this._http.get<Match>(this._baseUrl + "api/SampleData/GetMatch", { headers: headers, params: para }).subscribe(result => {
        this.match = result;
        console.log(this.match);
      })
      this.isLoaded = true;
      console.log(this.isLoaded);
    });
  }
  ngOnDestroy() {
    this.sub.unsubscribe();
  }
}

interface Team {
  ID: number
  acronym: string
  imgUrl: string
  name: string
  Players: Player[]
  CurrentVideogame: string
}

interface Player {
  BirthYear: string,
  Birthday: string
  FirstName: string
  LastName: string
  Name: string
  Nationality: string
  Role: string
  Slug: string
  HomeTown: string
  Id: number
  ImgUrl: string
}
interface Match {
  Id: number
  Draw: boolean
  Forfeit: boolean
  Name: string
  NumberOfGames: number
  Opponent1: Team
  Opponent2: Team
  WinnerId: number
  League: League
  BeginAt: Date
  EndAt: Date
  Games: Game[]
  Live: Live
  EmbeddedUrl: string
  MatchType: string
  ModifiedDate: Date
  OfficialStreamUrl: string
  Rescheduled: boolean
  Result: Result[]
  ScheduledDate: Date
  Series: Series
  Status: string
  Tournament: Tournament
  Videogame: Videogame
  OriginalScheduleDate: Date
  Slug: string
}
interface League {

}

interface Live {

}
interface Game {
  Id: number
  Name: string
  Position: string
  MatchId: number
  Slug: string
  BeginDate: Date
  Complete: boolean
  EndDate: Date
  Finished: boolean
  Forfeight: boolean
  Status: string
  VideoUrl: string
  Winner: Winner
}
interface Winner {

}
interface Result {

}
interface Series {

}
interface Tournament {

}
interface Videogame {

}

