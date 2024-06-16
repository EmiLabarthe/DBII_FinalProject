import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-select-champion',
  templateUrl: './select-champion.component.html',
  styleUrls: ['./select-champion.component.css']
})
export class SelectChampionComponent {

  constructor(private route: ActivatedRoute) { }

  studentId: string | null = null;

  ngOnInit(): void {
    this.route.paramMap.subscribe(params => {
      this.studentId = params.get('studentId');
    });
  }
}
