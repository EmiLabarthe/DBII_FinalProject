import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import {LoginComponent} from "./components/login/login.component";
import {MenuComponent} from "./components/Student/menu/menu.component";
import {RegisterComponent} from "./components/Student/register/register.component";
import {RankingComponent} from "./components/Student/ranking/ranking.component";
import {FixtureComponent} from "./components/Student/fixture/fixture.component";
import {CreateMatchComponent} from "./components/Admin/create-match/create-match.component";
import { PredictionsComponent } from './components/Student/predictions/predictions.component';

const routes: Routes = [
  { path: '', redirectTo: 'login', pathMatch: 'full' },

  { path: 'login', component: LoginComponent },
  { path: 'register', component: RegisterComponent },
  { path: 'menu/:id', component: MenuComponent },
  { path: 'ranking', component: RankingComponent },
  { path: 'fixture', component: FixtureComponent },
  { path: 'create-match', component: CreateMatchComponent },
  { path: 'predictions/:studentId', component: PredictionsComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
