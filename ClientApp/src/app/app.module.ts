import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from "@angular/router";
import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { CounterComponent } from './counter/counter.component';
import { FetchDataComponent } from './fetch-data/fetch-data.component';
import { NavbarComponent } from './navbar/navbar.component';
import { LeaguesComponent } from './leagues/leagues.component'; 
import { LeagueComponent } from './league/league.component'; 
import { MatchComponent } from './match/match.component';
import { TestComponent } from './test/test.component';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    CounterComponent,
    FetchDataComponent,
    NavbarComponent,
    LeaguesComponent,
    LeagueComponent,
    MatchComponent,
    TestComponent,
    HomeComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' },
      { path: 'counter', component: CounterComponent },
      { path: 'fetch-data', component: FetchDataComponent },
      { path: 'leagues', component: LeaguesComponent },
      { path: 'match/:id', component: MatchComponent}
    ])
  ],
  providers: [],
})
export class AppModule { }
