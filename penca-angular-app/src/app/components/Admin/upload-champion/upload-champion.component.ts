import { Component } from '@angular/core';
import { MatchResultService } from 'src/app/services/match-result.service';
import { MatchService } from 'src/app/services/match.service';
import { TournamentResultService } from 'src/app/services/tournament-result.service';

@Component({
  selector: 'app-upload-champion',
  templateUrl: './upload-champion.component.html',
  styleUrls: ['./upload-champion.component.css']
})
export class UploadChampionComponent {

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

  model = { ChampionId: '', ViceChampionId: '' };

  constructor(private tournamentResultService: TournamentResultService){}

  ngOnInit(){
  }

  submit(){
    if(this.model.ChampionId !== null && this.model.ViceChampionId !== null){
      this.tournamentResultService.addResult(this.model.ChampionId, this.model.ViceChampionId)
      .subscribe(res =>{
        console.log(`resultado agregado correctamente, ${res}`);
        alert('Resultado agregado!');
        this.model = { ChampionId: '', ViceChampionId: '' };
      })
    }
  }

  onSubmit(){}
}
