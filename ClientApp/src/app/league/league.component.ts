import { Component } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'league',
  templateUrl: './league.component.html',
  styleUrls: ['./league.component.css']
})

export class LeagueComponent {
  public League: League[] = [];

}
