import { Component } from '@angular/core';
import { MatchService } from 'src/app/services/match.service';
import { IMatch } from 'src/app/interfaces/IMatch';
import { NATIONAL_TEAMS } from 'src/app/constants/nationalTeams';
import { STADIUMS } from 'src/app/constants/stadiums';
import { catchError } from 'rxjs';
import { IFixtureItem } from 'src/app/interfaces/IFixtureItem';
import { STAGES } from 'src/app/constants/stages';
import { Router } from '@angular/router';

@Component({
  selector: 'app-create-match',
  templateUrl: './create-match.component.html',
  styleUrls: ['./create-match.component.css'],
})

export class CreateMatchComponent {
  
  constructor(private matchService: MatchService, private router: Router) { }
  
  nationalTeams = NATIONAL_TEAMS;
  stadiums = STADIUMS;
  stages = STAGES;
  
  model = { Id: null, LocalNationalTeam: '', VisitorNationalTeam: '', StadiumId: '', Date: '', StageId: '' } as unknown as IMatch;
  model2 = { Date: '', Time: '' };

  getDateTime(): void {
    const date = this.model2.Date;
    const time = this.model2.Time;

    if (date && time) {
      const dateTimeString = `${date}T${time}:00`;
      const isoDateTime = new Date(dateTimeString).toISOString();
      this.model.Date = isoDateTime;
    }
  }

  submitted = false;

  onSubmit() { this.submitted = true; }
  
  /** Emits a new match containing the values registered in the form.
  *
  */

  createMatch(): void {
    this.getDateTime();
    if (this.model.LocalNationalTeam && this.model.VisitorNationalTeam && this.model.StadiumId && this.model.Date) {
      this.matchService.add(this.model.LocalNationalTeam, this.model.VisitorNationalTeam, this.model.Date, this.model.StadiumId, this.model.StageId )
      .subscribe({
        next: (response: IMatch) => {
          console.log(response)
          alert('Partido registrado con éxito!');
          this.router.navigate(['/create-match']);
          this.model = { Id: null, LocalNationalTeam: '', VisitorNationalTeam: '', StadiumId: '', Date: '', StageId: '' } as unknown as IMatch;
          this.submitted = false;
        },
        error: (error) => {
          console.error(error);
          alert('Error al registrar el partido. Por favor, vuelva a intentar.');
        }
      });
    }
  }
  
}
