import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import {LoginComponent} from "./components/login/login.component";
import {RegisterComponent} from "./components/Student/register/register.component";
import {RankingComponent} from "./components/Student/ranking/ranking.component";
import {FixtureComponent} from "./components/Student/fixture/fixture.component";
import {CreateMatchComponent} from "./components/Admin/create-match/create-match.component";
import { PredictionsComponent } from './components/Student/predictions/predictions.component';
import { SelectChampionComponent } from './components/Student/select-champion/select-champion.component';
import { ResultsComponent } from './components/Student/results/results.component';
import { UploadResultComponent } from './components/Admin/upload-result/upload-result.component';
import { MenuComponent } from './components/Student/menu/menu.component';
import { UploadChampionComponent } from './components/Admin/upload-champion/upload-champion.component';

const routes: Routes = [
  { path: '', redirectTo: 'login', pathMatch: 'full' },

  { path: 'login', component: LoginComponent },
  { path: 'register', component: RegisterComponent },
  { path: 'ranking/:studentId', component: RankingComponent },
  { path: 'fixture/:studentId', component: FixtureComponent },
  { path: 'results/:studentId', component: ResultsComponent },
  { path: 'create-match', component: CreateMatchComponent },
  { path: ':studentId/predictions', component: PredictionsComponent },
  { path: 'select-champion/:studentId', component: SelectChampionComponent },
  { path: 'menu/:studentId', component: MenuComponent },
  { path: 'upload-result', component: UploadResultComponent },
  { path: 'predictions/:studentId', component: PredictionsComponent },
  { path: 'select-champion/:studentId', component: SelectChampionComponent},
  { path: 'upload-champion', component: UploadChampionComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
