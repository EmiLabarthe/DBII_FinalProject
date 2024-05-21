import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-create-match',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './create-match.component.html',
  styleUrl: './create-match.component.css'
})
export class CreateMatchComponent {

  constructor(private matchService: MatchService) { }

  nationalTeams = NATIONAL_TEAMS;

  @Output() matchAdded = new EventEmitter<IMatch>();

  model = { localNationalTeam: 'Nombre Selección Local', visitorNationalTeam: 'Nombre Selección Visitante', city: 'Nombre de Ciudad', date: new Date() } as IMatch;

  submitted = false;
  onSubmit() { this.submitted = true; }

  /** Emits a new match containing the values registered in the form.
   * 
   */
  createMatch(): void {
    if (this.model.localNationalTeam && this.model.visitorNationalTeam && this.model.city && this.model.date) {
      this.matchAdded.emit(this.model);
      this.model = { localNationalTeam: 'Nombre Selección Local', visitorNationalTeam: 'Nombre Selección Visitante', city: 'Nombre de Ciudad', date: new Date() } as IMatch;
      this.submitted = false;
    }
  }

}
