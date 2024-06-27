import { AfterViewInit, Component, OnInit } from '@angular/core';
import * as $ from 'jquery';
import { ActivatedRoute } from '@angular/router';
import { NATION_FLAGS } from 'src/app/constants/nationFlags';
import { IPredictionItem } from 'src/app/interfaces/IPredictionItem';
import { PredictionService } from 'src/app/services/prediction.service';

@Component({
  selector: 'app-predictions',
  templateUrl: './predictions.component.html',
  styleUrls: ['./predictions.component.css',]
})
export class PredictionsComponent implements OnInit, AfterViewInit {
  
  predictions: IPredictionItem[] | undefined;
  originalPredictions: IPredictionItem[] | undefined;
  
  nationFlags = NATION_FLAGS;
  
  studentId: string | undefined;
  
  constructor(private route: ActivatedRoute, private predictionService: PredictionService) { }
  
  ngAfterViewInit() {
    ($('[data-toggle="tooltip"]') as any).tooltip();
  }

  ngOnInit() {
    this.route.params.subscribe(params => {
      const studentId = params['studentId'];
      this.studentId = studentId;
    });
    this.getPredictionItems();
  }
  
  getPredictionItems(): void {
    this.predictionService.getPredictionItems(this.studentId!).subscribe({
      next: (response: IPredictionItem[]) => {
        this.predictions = response;
        this.originalPredictions = JSON.parse(JSON.stringify(response));
        console.log(response);
      },
      error: (error) => {
        console.error('Error fetching prediction items:', error);
      }
    });
  }
  
  hasPredictionChanged(prediction: IPredictionItem, index: number): boolean {
    return JSON.stringify(prediction) !== JSON.stringify(this.originalPredictions![index]);
  }
  
  save(): void {
    const modifiedPredictions = this.predictions!.filter((prediction, index) => this.hasPredictionChanged(prediction, index));
    
    modifiedPredictions.forEach(prediction => {
      if(!prediction.predictionId){
        this.predictionService.add(this.studentId!, prediction.matchId, prediction.localNationalTeamPredictedGoals, prediction.visitorNationalTeamPredictedGoals)
        .subscribe(response => {
          console.log('Prediction added', response);
        });
      } else {
        this.predictionService.update(prediction, this.studentId!)
        .subscribe(response => {
          console.log('Prediction updated', response);
        });
      }
      
    });
    
    this.originalPredictions = JSON.parse(JSON.stringify(this.predictions));
  }
}
