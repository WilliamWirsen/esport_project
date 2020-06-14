import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'leagues',
  templateUrl: './leagues.component.html',
  styleUrls: ['./leagues.component.css']
})

export class LeagueComponent {
  public leagues: Leagues[];

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
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
