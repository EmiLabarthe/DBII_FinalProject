import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { NATION_FLAGS } from 'src/app/constants/nationFlags';
import { IPredictionItem } from 'src/app/interfaces/IPredictionItem';
import { PredictionService } from 'src/app/services/prediction.service';

@Component({
  selector: 'app-predictions',
  templateUrl: './predictions.component.html',
  styleUrls: ['./predictions.component.css']
})
export class PredictionsComponent implements OnInit {
  
  predictions: IPredictionItem[] | undefined;

  nationFlags= NATION_FLAGS;
  
  studentId: string | undefined;
  
  constructor(private route: ActivatedRoute, private predictionService: PredictionService) { }
  
  ngOnInit() {
    this.route.params.subscribe(params => {
      const studentId = params['studentId'];
      this.studentId= studentId;
    });
    this.getPredictionItems();
  }
  
  getPredictionItems(): void {
    this.predictionService.getPredictionItems(this.studentId!).subscribe({
      next: (response: IPredictionItem[]) => {
        this.predictions = response;
        console.log(response);
      },
      error: (error) => {
        console.error('Error fetching prediction items:', error);
      }
    });
  }

  save(){

  }
}
