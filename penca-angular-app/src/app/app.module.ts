import { NgModule, LOCALE_ID  } from '@angular/core';
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
import { SelectChampionComponent } from './components/Student/select-champion/select-champion.component';
import { ResultsComponent } from './components/Student/results/results.component';
import { UploadResultComponent } from './components/Admin/upload-result/upload-result.component';
import { AdminMenuComponent } from './components/Admin/admin-menu/admin-menu.component';
import { UploadChampionComponent } from './components/Admin/upload-champion/upload-champion.component';


import { registerLocaleData } from '@angular/common';
import localeEs from '@angular/common/locales/es';

registerLocaleData(localeEs);

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
    SelectChampionComponent,
    ResultsComponent,
    UploadResultComponent,
    AdminMenuComponent,
    UploadChampionComponent,
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
  providers: [{ provide: LOCALE_ID, useValue: 'es' }],
  bootstrap: [AppComponent]
})
export class AppModule { }
