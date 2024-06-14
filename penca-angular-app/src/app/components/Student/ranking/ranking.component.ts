import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { IRankingItem } from 'src/app/interfaces/IRankingItem';
import { RankingService } from 'src/app/services/ranking.service';

@Component({
  selector: 'app-ranking',
  templateUrl: './ranking.component.html',
  styleUrls: ['./ranking.component.css']
})
export class RankingComponent {

  ranking:  IRankingItem[] | undefined;

  constructor(private route: ActivatedRoute, private rankingService: RankingService){}

  ngOnInit(): void{
    this.rankingService.getRanking().subscribe(
      (data: IRankingItem[]) => {
        this.ranking = data;
      },
      (error) => {
        console.error('Error fetching ranking', error)
      }
    )
  }

}
