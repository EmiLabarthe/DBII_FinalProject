import { Component } from '@angular/core';
import { IMatch } from 'src/app/interfaces/IMatch';
import { MatchService } from 'src/app/services/match.service';
import { CommonModule } from '@angular/common';
import { IMatchResult } from 'src/app/interfaces/IMatchResult';
import { MatchResultService } from 'src/app/services/match-result.service';

@Component({
  selector: 'app-upload-result',
  templateUrl: './upload-result.component.html',
  styleUrls: ['./upload-result.component.css']
})
export class UploadResultComponent {

  matches: any[] | undefined;
  teams: string[] = [
    "Argentina",
    "Canadá",
    "Chile",
    "Perú",
    "Ecuador",
    "Jamaica",
    "México",
    "Venezuela",
    "Bolivia",
    "Estados Unidos",
    "Panamá",
    "Uruguay",
    "Brasil",
    "Colombia",
    "Costa Rica",
    "Paraguay"
  ];

  model = { MatchId: '', LocalNationalTeamGoals: 0, VisitorNationalTeamGoals: 0, WinnerId: '' };

  constructor(private matchService: MatchService, private matchResultService: MatchResultService){}

  ngOnInit(){
    this.matchService.getMatches().subscribe(
      (data: any[]) => {
        this.matches = data;
      },
      (error) => {
        console.error('Error fetching matches', error)
      }
    )
  }

  submit(){
    if(this.model.MatchId !== null && this.model.LocalNationalTeamGoals !== null && this.model.VisitorNationalTeamGoals !== null && this.model.WinnerId !== null){
      this.matchResultService.addResult(this.model.MatchId, this.model.LocalNationalTeamGoals, this.model.VisitorNationalTeamGoals, this.model.WinnerId)
      .subscribe(res =>{
        console.log(`resultado agregado correctamente, ${res}`);
        alert('Resultado agregado!');
        this.model = { MatchId: '', LocalNationalTeamGoals: 0, VisitorNationalTeamGoals: 0, WinnerId: '' };
      })
    }
  }

  onSubmit(){}
}
