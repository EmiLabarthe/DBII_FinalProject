import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { NATIONAL_TEAMS } from 'src/app/constants/nationalTeams';
import { IStudentTournamentPrediction } from 'src/app/interfaces/IStudentTournamentPrediction';
import { StudentTournamentPredictionService } from 'src/app/services/student-tournament-prediction.service';

@Component({
  selector: 'app-select-champion',
  templateUrl: './select-champion.component.html',
  styleUrls: ['./select-champion.component.css']
})
export class SelectChampionComponent {
  
  constructor(private route: ActivatedRoute, private tournamentService : StudentTournamentPredictionService,
    private router: Router) { }
    
    studentId: string | null = null;
    
    ngOnInit(): void {
      this.route.paramMap.subscribe(params => {
        this.studentId = params.get('studentId');
      });
    }
    
    nationalTeams: string[] = NATIONAL_TEAMS.map(nationalTeam => nationalTeam.CountryName);
    
    model = { ChampionId: '', ViceChampionId: '' } as IStudentTournamentPrediction;
    
    async upload() {
      if(this.studentId !== null){
        this.tournamentService.add(this.studentId, this.model.ChampionId, this.model.ViceChampionId)
        .subscribe({
          next: (response: IStudentTournamentPrediction) => {
            console.log(response);
            this.model.ChampionId= '';
            this.model.ViceChampionId= '';
            this.router.navigate([`/predictions/${this.studentId}`]);
            alert('Prediction registered successfully!');
          }
        });
      }else{
        console.error('Student ID is null')
      }
      
    }
  }
  