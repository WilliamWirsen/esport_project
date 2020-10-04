import { Component, OnInit, Inject } from '@angular/core';
import { HttpEventType, HttpClient, HttpRequest, HttpHeaders } from '@angular/common/http';

@Component({
  selector: 'app-scheduled-matches',
  templateUrl: './scheduled-matches.component.html',
  styleUrls: ['./scheduled-matches.component.css'],
})
export class ScheduledMatchesComponent implements OnInit {
  public matches: Matches[] = [];
  public leagues: League[] = [];
  today: number = Date.now();
  public showLoader: boolean;
  public filterUpcomingGames: string;
  totalMatches = 0;
  private http: HttpClient;
  private baseUrl: string;

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.showLoader = true;
    this.http = http;
    this.baseUrl = baseUrl;
    this.GetLeagueMatches(null);
    
  }
  ngOnInit(): void {
    this.filterUpcomingGames = "one_month";

    var filterInterval = this.GetCookie("filterUpcomingGames");
    console.log(filterInterval);
    if (filterInterval == null)
      this.SetCookie("filterUpcomingGames", this.filterUpcomingGames, 30);
    else
      this.filterUpcomingGames = filterInterval;
  }

  GetLeagueMatches(toDate) {
    var url = "api/SampleData/GetLeagueMatches";
    if (toDate != null)
      url += "?toDate=" + toDate;


    this.http.get<League[]>(this.baseUrl + url, {
      reportProgress: true, observe: "events",
      headers: new HttpHeaders({ 'Content-Type': 'application/json' })
    }).subscribe(event => {
      if (event.type === HttpEventType.DownloadProgress) {
        console.log("download progress: " + event.loaded + " of " + event.total);
      }
      if (event.type === HttpEventType.Response) {
        console.log("download complete");
        this.leagues = event.body;
        console.log("--- Scheduled matches ---");
        console.log(this.leagues);
        console.log(this.leagues = this.GroupBy(this.leagues, 'videogameName'));
        console.log("--- End Scheduled matches ---");
        for (var i = 0; i < Object.keys(this.leagues).length; i++)
          this.CountMatches(this.leagues);
        this.showLoader = false;
      }
      
    }, error => console.error(error));
  };
  
  FilterUpcomingGames($event) {
    console.log("--- start filter ---")
    this.SetCookie("filterUpcomingGames", $event, 30);
    
    var currentDate = new Date();
    switch (this.GetCookie("filterUpcomingGames")) {
      case "one_month":
        this.GetLeagueMatches(null);
        break;
      case "one_week":
        var newDate = new Date(currentDate.setDate(currentDate.getDate() + 7));
        var formattedDate = newDate.toLocaleDateString('sv-SE', {
          day: '2-digit',
          month: '2-digit',
          year: 'numeric'
        }); 
        this.GetLeagueMatches(formattedDate);
        break;
      case "one_day":
        var newDate = new Date(currentDate.setDate(currentDate.getDate() + 1));
        var formattedDate = newDate.toLocaleDateString('sv-SE', {
          day: '2-digit',
          month: '2-digit',
          year: 'numeric'
        });
        this.GetLeagueMatches(formattedDate);
        break;
      default:
        this.GetLeagueMatches(null);
        break;

    }
    console.log("--- end filter ---")
  }

  ToggleSelect(id) {
    var element = document.getElementById(id + "-container");
    element.classList.toggle("is-hidden");
    element.classList.toggle("mb-1em");
    console.log(element);
  }

  GroupBy(xs, key) {
    return xs.reduce(function (rv, x) {
      (rv[x[key]] = rv[x[key]] || []).push(x);
      return rv;
    }, {});
  };

  CountMatches(array) {
    for (var i in array) {
      var counter = 0;
      for (var j in array[i]) {
        if (array[i][j].matches !== undefined)
          counter += Object.keys(array[i][j].matches).length;
      }
      array[i]["totalMatches"] = counter;
    }
  };

  SetCookie(name, value, days) {
    var expires = "";
    if (days) {
      var date = new Date();
      date.setTime(date.getTime() + (days * 24 * 60 * 60 * 1000));
      expires = "; expires=" + date.toUTCString();
    }
    document.cookie = name + "=" + (value || "") + expires + "; path=/";
  }
  GetCookie(name) {
    var nameEQ = name + "=";
    var ca = document.cookie.split(';');
    for (var i = 0; i < ca.length; i++) {
      var c = ca[i];
      while (c.charAt(0) == ' ') c = c.substring(1, c.length);
      if (c.indexOf(nameEQ) == 0) return c.substring(nameEQ.length, c.length);
    }
    return null;
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

