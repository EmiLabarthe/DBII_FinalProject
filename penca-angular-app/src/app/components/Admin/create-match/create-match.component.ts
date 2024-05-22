import { Component } from '@angular/core';
import { MatchService } from 'src/app/services/match.service';
import { IMatch } from 'src/app/interfaces/IMatch';
import { NATIONAL_TEAMS } from 'src/app/constants/nationalTeams';
import { STADIUMS } from 'src/app/constants/stadiums';
import { catchError } from 'rxjs';

@Component({
  selector: 'app-create-match',
  templateUrl: './create-match.component.html',
  styleUrls: ['./create-match.component.css'],
})
export class CreateMatchComponent {

  constructor(private matchService: MatchService) { }

  nationalTeams = NATIONAL_TEAMS;
  stadiums= STADIUMS;

  model = { LocalNationalTeam: 'Nombre Selección Local',
    VisitorNationalTeam: 'Nombre Selección Visitante', StadiumId: 0, Date: new Date() } as IMatch;

  submitted = false;
  onSubmit() { this.submitted = true; }

  /** Emits a new match containing the values registered in the form.
  *
  */
  createMatch(): void {
    if (this.model.LocalNationalTeam && this.model.VisitorNationalTeam && this.model.StadiumId && this.model.Date) {
      this.matchService.add(this.model.LocalNationalTeam, this.model.VisitorNationalTeam, this.model.Date, this.model.StadiumId)
      .pipe(
        catchError((error) => {
          console.error(error);
          alert('Error al registrar el partido. Por favor, vuelva a intentar.');
          throw error;
        })
      ).subscribe({
        next: (response: IMatch) => {
          console.log(response)
          alert('Partido registrado con éxito!');
          this.model = { LocalNationalTeam: 'Nombre Selección Local', VisitorNationalTeam: 'Nombre Selección Visitante', StadiumId: 0, Date: new Date() } as IMatch;
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
