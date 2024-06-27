import { AfterViewInit, Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import * as $ from 'jquery';
import { NATION_FLAGS } from 'src/app/constants/nationFlags';
import { IPredictionResultItem } from 'src/app/interfaces/IPredictionResultItem';
import { ResultService } from 'src/app/services/result.service';

@Component({
  selector: 'app-results',
  templateUrl: './results.component.html',
  styleUrls: ['./results.component.css']
})
export class ResultsComponent implements OnInit, AfterViewInit {
  results: IPredictionResultItem[] | undefined;
  
  nationFlags= NATION_FLAGS;
  
  studentId: string | undefined;
  
  constructor(private route: ActivatedRoute, private resultService: ResultService) { }
  
  ngAfterViewInit() {
    ($('[data-toggle="tooltip"]') as any).tooltip();
  }

  ngOnInit() {
    this.route.params.subscribe(params => {
      const studentId = params['studentId'];
      this.studentId= studentId;
    });
    this.getPredictionResultItems();
  }
  
  getPredictionResultItems(): void {
    this.resultService.getPredictionResultItems(this.studentId!).subscribe({
      next: (response: IPredictionResultItem[]) => {
        this.results = response;
        console.log(response);
      },
      error: (error) => {
        console.error('Error fetching result items:', error);
      }
    });
  }
  
  getPoints(predictionResultItem: IPredictionResultItem): number {
    return this.resultService.getPoints(
      predictionResultItem.localNationalTeamGoals,
      predictionResultItem.localNationalTeamPredictedGoals,
      predictionResultItem.visitorNationalTeamGoals,
      predictionResultItem.visitorNationalTeamPredictedGoals
    );
  }
}
