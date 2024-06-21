import { Component } from '@angular/core';
import { IMatch } from 'src/app/interfaces/IMatch';
import { MatchService } from 'src/app/services/match.service';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-upload-result',
  templateUrl: './upload-result.component.html',
  styleUrls: ['./upload-result.component.css']
})
export class UploadResultComponent {

  matches: any[] | undefined;

  constructor(private matchService: MatchService){}



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
}
