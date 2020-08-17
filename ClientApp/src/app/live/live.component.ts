import { Component, OnInit, Input, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { DomSanitizer } from '@angular/platform-browser';
import { ActivatedRoute } from '@angular/router';
import { environment } from '../../environments/environment';

@Component({
  selector: 'app-live',
  templateUrl: './live.component.html',
  styleUrls: ['./live.component.css']
})
export class LiveComponent implements OnInit {
  public liveGames: any = [];
  private url: string;

  public isEmpty(obj: object): boolean {
    return Object.keys(obj).length === 0;
  }

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string, private sanitizer: DomSanitizer, private route: ActivatedRoute) {
    const url = environment.production ? "esport.azurewebsites.net" : "localhost";
    this.url = url;

    http.get<LiveGames[]>(baseUrl + 'api/SampleData/GetLiveMatches').subscribe(result => {
      this.liveGames = result;
      console.log("--- Live games ---")
      console.log(this.liveGames);
      console.log("--- end live games ---")
      console.log(this.liveGames.length);
      this.liveGames = groupBy(this.liveGames, 'status');

      console.log(this.liveGames);
    }, error => console.error(error));

    var groupBy = function (xs, key) {
      return xs.reduce(function (rv, x) {
        (rv[x[key]] = rv[x[key]] || []).push(x);
        return rv;
      }, {});
    };
  }

  transform(url) {
    if (url.includes("twitch"))
      url = url + "&parent=" + this.url + "&muted=true";
    return this.sanitizer.bypassSecurityTrustResourceUrl(url);
  }


  ngOnInit(): void {
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
  not_started: any[];
  running: any[];
  finished: any[];
}
