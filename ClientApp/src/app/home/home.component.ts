import { Component, Input } from '@angular/core';
import { LiveComponent } from '../live/live.component';
import { ScheduledMatchesComponent } from '../scheduled-matches/scheduled-matches.component';

@Component({
  selector: 'app-home',
  templateUrl: "./home.component.html",
  styleUrls: ['./home.component.css'],
})

export class HomeComponent {
  @Input() live: LiveComponent;
  @Input() scheduledMatches: ScheduledMatchesComponent;

  constructor() { }

}

