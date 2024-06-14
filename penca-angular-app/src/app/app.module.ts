import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { AppRoutingModule } from './app-routing.module';
import { RouterModule } from '@angular/router';
import { HttpClientModule } from '@angular/common/http';

import { AppComponent } from './app.component';
import { CreateMatchComponent } from './components/Admin/create-match/create-match.component';
import { LoginComponent } from './components/login/login.component';
import { MenuComponent } from './components/Student/menu/menu.component';
import { MatchesComponent } from './components/Student/matches/matches.component';
import { RankingComponent } from './components/Student/ranking/ranking.component';
import { RegisterComponent } from './components/Student/register/register.component';
import { FixtureComponent } from './components/Student/fixture/fixture.component';
import { PredictionsComponent } from './components/Student/predictions/predictions.component';
@NgModule({
  declarations: [
    AppComponent,
    MatchesComponent,
    CreateMatchComponent,
    MenuComponent,
    LoginComponent,
    RankingComponent,
    RegisterComponent,
    FixtureComponent,
    PredictionsComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    RouterModule,
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    HttpClientModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
