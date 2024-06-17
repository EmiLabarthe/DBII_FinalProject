import { Component } from '@angular/core';
import { IPrediction } from 'src/app/interfaces/IPrediction';
import { IPredictionItem } from 'src/app/interfaces/IPredictionItem';
import { PredictionService } from 'src/app/services/prediction.service';

@Component({
  selector: 'app-predictions',
  templateUrl: './predictions.component.html',
  styleUrls: ['./predictions.component.css']
})
export class PredictionsComponent {
  
  predictions: IPredictionItem[] | undefined;
  
  constructor(private predictionService: PredictionService) { }
  
  ngOnInit() {
    this.getPredictionItems();
  }
  
  getPredictionItems(): void {
    this.predictionService.getPredictionItems()
    .subscribe({
      next: (response: IPredictionItem[]) => {
        this.predictions= response;
        console.log(response);
      }
    });
  }
  
  addGoal(){

  }
  substractGoal(){
    
  }
}
