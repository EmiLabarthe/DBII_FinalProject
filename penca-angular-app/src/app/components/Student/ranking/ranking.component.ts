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
  studentId: string | null = null;

  constructor(private route: ActivatedRoute, private rankingService: RankingService){}

  

  ngOnInit(): void{
    this.route.paramMap.subscribe(params => {
      this.studentId = params.get('studentId');
    });
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
