import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
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

  teams= [
    'Uruguay',
    'Argentina',
    'Brasil',
    'Canadá',
    'Chile',
    'Perú',
    'Ecuador',
    'Jamaica',
    'México',
    'Venezuela',
    'Bolivia',
    'Estados Unidos',
    'Panamá',
    'Colombia',
    'Costa Rica',
    'Paraguay'
  ];

  model = { ChampionId: '', ViceChampionId: '' } as IStudentTournamentPrediction;

  async upload() {
    if(this.studentId !== null){
      this.tournamentService.add(this.studentId, this.model.ChampionId, this.model.ViceChampionId)
      .subscribe({
        next: (response: IStudentTournamentPrediction) => {
          console.log(response);
          this.router.navigate([`/fixture/${this.studentId}`]);
          alert('Predicción subida con éxito');
        }
      });
    }else{
      console.error('Student ID is null')
    }
    
  }
}
