import { Component, OnDestroy, OnInit, Inject } from '@angular/core';
import { HttpClient, HttpParams, HttpHeaders } from '@angular/common/http';
import { ActivatedRoute } from '@angular/router';
import { groupBy } from 'rxjs/operators';

@Component({
  selector: 'match',
  templateUrl: './match.component.html',  
  styleUrls: ['./match.component.css']
})

export class MatchComponent implements OnInit, OnDestroy {
  id: number;

  private sub: any;
  private _http: HttpClient;
  private _baseUrl: string;

  public teams: Team[] = [];
  public today: number = new Date().getFullYear();

  opponent1 = [];
  opponent2 = [];

  public isLoaded: boolean;

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string, private route: ActivatedRoute) {
    this._http = http;
    this._baseUrl = baseUrl;
    this.isLoaded = false;
    console.log(this.isLoaded);

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
        console.log(this.teams[0]);
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
